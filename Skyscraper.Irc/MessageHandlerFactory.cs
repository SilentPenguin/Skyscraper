using Skyscraper.Tcp;
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
        private static TypeDictionary<string, IMessageHandler> messageHandlers;

        static MessageHandlerFactory()
        {
            MessageHandlerFactory.messageHandlers = new TypeDictionary<string, IMessageHandler>(
                TypeHelpers
                .ClassesForInterfaceInAssembly<IMessageHandler>()
                .SelectMany(t => (Attribute.GetCustomAttribute(t, typeof(HandlerAttribute)) as HandlerAttribute).words.Select(cw => new { CommandWord = cw, Type = t }))
                .ToDictionary(c => c.CommandWord.ToUpperInvariant(), c => c.Type)
            );
        }

        public static ICollection<string> AvailableCommands
        {
            get { return MessageHandlerFactory.messageHandlers.Keys; }
        }

        public static IMessageState Resolve(IRawMessage rawMessage)
        {
            IMessage ircMessage = new ProtocolMessage(rawMessage);
            string commandWord = ircMessage.CommandWord.ToUpperInvariant();

            return new MessageState(MessageHandlerFactory.messageHandlers[commandWord], ircMessage);
        }
    }
}
