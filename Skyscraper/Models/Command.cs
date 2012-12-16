using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface ICommand
    {
        String Text { get; }
        CommandType Type { get; }
        String[] Arguments { get; }
    }

    public class Command : ICommand
    {
        public Command(String text)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }
            this.Text = text;
        }

        public String Text { get; private set; }
        private String[] commandBreakdown;
        private String[] CommandBreakdown
        {
            get
            {
                if (this.commandBreakdown == null)
                {
                    this.commandBreakdown = this.Text.TrimStart('/').Split(' ');
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
                return this.Text.StartsWith("/");
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
