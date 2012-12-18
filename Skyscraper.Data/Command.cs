using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyscraper.Utilities;

namespace Skyscraper.Data
{
    public interface ICommand
    {
        String Text { get; }
        String Body { get; }
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

            if (!text.StartsWith("/"))
            {
                text = "/say " + text.TrimStart();
            }

            this.Text = text;
        }

        public String Text { get; private set; }

        public String Body
        {
            get
            {
                return this.Text.Remove(0, this.Text.IndexOf(' ') + 1);
            }
        }

        private String[] commandBreakdown;
        private String[] CommandBreakdown
        {
            get
            {
                if (this.commandBreakdown == null)
                {
                    this.commandBreakdown = this.Text.TrimStart('/').Split(' ').Where(s => !String.IsNullOrWhiteSpace(s)).ToArray();
                }
                return commandBreakdown;
            }
        }

        public CommandType Type
        {
            get
            {
                CommandType result = CommandType.Unrecognised;
                Enum.TryParse<CommandType>(CommandBreakdown[0], ignoreCase:true, result: out result);
                return result;
            }
        }

        public String[] Arguments { get { return CommandBreakdown.Skip(1).ToArray(); } } 
    }

    public enum CommandType
    {
        Unrecognised,
        Server,
        Quit,
        Say,
        Me,
    }
}
