using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Irc
{
    public class MessageHandlerFactory
    {
        private static TypeDictionary<string, IIrcMessageHandler> messageHandlers;

        static MessageHandlerFactory()
        {
            MessageHandlerFactory.messageHandlers = new TypeDictionary<string, IIrcMessageHandler>(
                TypeHelpers
                .ClassesForInterfaceInAssembly<IIrcMessageHandler>()
                .SelectMany(t => (Attribute.GetCustomAttribute(t, typeof(HandlerAttribute)) as HandlerAttribute).words.Select(cw => new { CommandWord = cw, Type = t }))
                .ToDictionary(c => c.CommandWord.ToUpperInvariant(), c => c.Type)
            );
        }

        public static ICollection<string> AvailableCommands
        {
            get { return MessageHandlerFactory.messageHandlers.Keys; }
        }

        public static IIrcMessageState Resolve(IRawMessage rawMessage)
        {
            IIrcMessage ircMessage = new IrcMessage(rawMessage);
            string commandWord = ircMessage.CommandWord.ToUpperInvariant();

            return new IrcMessageState(MessageHandlerFactory.messageHandlers[commandWord], ircMessage);
        }
    }
}
