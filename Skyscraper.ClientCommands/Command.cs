using System;
using System.Linq;

namespace Skyscraper.Models
{
    public class Command : ICommand
    {
        public String[] Arguments
        {
            get
            {
                return CommandBreakdown.Skip(1).ToArray();
            }
        } 

        public String Body
        {
            get
            {
                return this.Text.Remove(0, this.Text.IndexOf(' ') + 1);
            }
        }

        public String Text { get; private set; }

        public CommandType Type
        {
            get
            {
                CommandType result = CommandType.Unrecognised;
                Enum.TryParse<CommandType>(CommandBreakdown[0], ignoreCase:true, result: out result);
                return result;
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
    }
}