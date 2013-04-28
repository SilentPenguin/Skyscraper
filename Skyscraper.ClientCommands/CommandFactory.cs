using System;
using System.Linq;
using Skyscraper.Models;
using Skyscraper.Utilities;
using System.Collections.Generic;

namespace Skyscraper.ClientCommands
{
    public class CommandFactory
    {
        private static TypeDictionary<string, ICommandHandler> commands;

        static CommandFactory()
        {
            CommandFactory.commands = new TypeDictionary<string, ICommandHandler>(
                TypeHelpers
                .ClassesForInterfaceInAssembly<ICommandHandler>()
                .SelectMany(t => (Attribute.GetCustomAttribute(t, typeof(HandlerAttribute)) as HandlerAttribute).words.Select(cw => new { CommandWord = cw, Type = t }))
                .ToDictionary(c => c.CommandWord.ToUpperInvariant(), c => c.Type)
            );
        }

        public static ICollection<string> AvailableCommands
        {
            get { return CommandFactory.commands.Keys; }
        }

        public static ICommandState Resolve(INetwork network, IChannel channel, IUser user, String commandString) 
        {
            ICommand command = new Command(commandString)
            {
                Network = network,
                Channel = channel,
                User = user,
            };
            string commandWord = command.CommandWord.ToUpperInvariant();

            return new CommandState(CommandFactory.commands[commandWord], command);
        }
    }
}