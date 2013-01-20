using Skyscraper.Irc;
using Skyscraper.Models;
using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands
{
    public class CommandFactory
    {
        private static Dictionary<String, Type> DefaultCommands;

        static CommandFactory()
        {
            CommandFactory.DefaultCommands = TypeHelper
                          .ClassesForInterface<ICommand>()
                          .ToDictionary(type => (Attribute.GetCustomAttribute(type, typeof(CommandTypeAttribute)) as CommandTypeAttribute).Value);
        }

        public static ICommand Resolve(INetwork network, IChannel channel, String commandString) {
            Command command = new Command(commandString) { Network = network, Channel = channel };
            Type commandType = CommandFactory.DefaultCommands[command.Type];
            return Activator.CreateInstance(commandType, command) as ICommand;
        }
    }
}
