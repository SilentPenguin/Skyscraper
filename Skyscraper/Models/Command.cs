using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface ICommand
    {
        String Command { get; }
    }

    public class Command : ICommand
    {
        public Command(String Command)
        {
            if (String.IsNullOrEmpty(Command))
            {
                throw new ArgumentNullException("Command");
            }
            this.Command = Command;
        }

        public String Command { get; private set; }
        private String[] commandBreakdown;
        private String[] CommandBreakdown
        {
            get
            {
                if (this.commandBreakdown == null)
                {
                    this.commandBreakdown = this.Command.TrimStart('/').Split(' ');
                }
                return commandBreakdown;
            }
        }

        public CommandType Type
        {
            get
            {
                CommandType result;
                if (!this.IsCommand)
                {
                    result = CommandType.Say;
                }
                else
                {
                    Enum.TryParse<CommandType>(CommandBreakdown[0], out result);
                }

                return result;
            }
        }

        public Boolean IsCommand {
            get
            {
                return this.Command.StartsWith("/");
            }
        }

        public String[] Arguments { get { return CommandBreakdown.Skip(1).ToArray(); } } 
    }

    public enum CommandType
    {
        Connect,
        Disconnect,
        Say,
        Me,
    }
}
