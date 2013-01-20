using System;
using System.Collections.Generic;
using System.Linq;
using Skyscraper.Models;
using Skyscraper.Utilities;

namespace Skyscraper.ClientCommands
{
    public class CommandFactory
    {
        private static Dictionary<string, Type> CommandTypes;
        private static Dictionary<string, ICommandHandler> Commands;

        static CommandFactory()
        {
            CommandFactory.CommandTypes = TypeHelper
                .ClassesForInterfaceInAssemby<ICommandHandler>()
                .SelectMany(t => (Attribute.GetCustomAttribute(t, typeof(TextCommandHandlerAttribute)) as TextCommandHandlerAttribute).CommandWords.Select(cw => new { CommandWord = cw, Type = t }))
                .ToDictionary(c => c.CommandWord.ToUpperInvariant(), c => c.Type);

            CommandFactory.Commands = new Dictionary<string, ICommandHandler>();
        }

        public static ICommandHandler Resolve(INetwork network, IChannel channel, String commandString) 
        {
            Command command = new Command(commandString) { Network = network, Channel = channel };
            string commandWord = command.CommandWord.ToUpperInvariant();

            lock (CommandFactory.Commands)
            {
                if(CommandFactory.Commands.ContainsKey(commandWord))
                {
                    return CommandFactory.Commands[commandWord];
                }
                else if(CommandFactory.CommandTypes.ContainsKey(commandWord))
                {
                    Type commandType = CommandFactory.CommandTypes[command.CommandWord.ToUpperInvariant()];
                    ICommandHandler commandHandler = Activator.CreateInstance(commandType, command) as ICommandHandler;
                    CommandFactory.Commands.Add(commandWord, commandHandler);

                    return commandHandler;
                }                
            }

            return null;
        }
    }
}