using System;
using System.Collections.Generic;
using System.Linq;
using Skyscraper.Models;
using Skyscraper.Utilities;

namespace Skyscraper.ClientCommands
{
    public class CommandFactory
    {
        private static Dictionary<string, Type> commandTypes;
        private static Dictionary<string, ICommandHandler> commands;

        static CommandFactory()
        {
            CommandFactory.commandTypes = TypeHelper
                .ClassesForInterfaceInAssemby<ICommandHandler>()
                .SelectMany(t => (Attribute.GetCustomAttribute(t, typeof(TextCommandHandlerAttribute)) as TextCommandHandlerAttribute).CommandWords.Select(cw => new { CommandWord = cw, Type = t }))
                .ToDictionary(c => c.CommandWord.ToUpperInvariant(), c => c.Type);

            CommandFactory.commands = new Dictionary<string, ICommandHandler>();
        }

        public static void Resolve(INetwork network, IChannel channel, String commandString, out ICommand command, out ICommandHandler handler) 
        {
            command = new Command(commandString) { Network = network, Channel = channel };
            handler = null;
            string commandWord = command.CommandWord.ToUpperInvariant();

            lock (CommandFactory.commands)
            {
                if(CommandFactory.commands.ContainsKey(commandWord))
                {
                    handler = CommandFactory.commands[commandWord];
                    return;
                }
                else if(CommandFactory.commandTypes.ContainsKey(commandWord))
                {
                    Type commandType = CommandFactory.commandTypes[command.CommandWord.ToUpperInvariant()];
                    ICommandHandler commandHandler = Activator.CreateInstance(commandType) as ICommandHandler;
                    CommandFactory.commands.Add(commandWord, commandHandler);

                    handler = commandHandler;
                    return;
                }                
            }
        }
    }
}