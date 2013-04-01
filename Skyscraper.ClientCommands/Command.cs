using System;
using System.Linq;
using Skyscraper.Models;

namespace Skyscraper.ClientCommands
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

        

        public String CommandWord
        {
            get
            {
                return this.CommandBreakdown[0].ToUpper();
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

        public INetwork Network { get; set; }
        public IChannel Channel { get; set; }
        public IUser User { get; set; }

        public String Text { get; private set; }

        public Command(Command command)
            : this(command.Text)
        {
            this.Network = command.Network;
            this.Channel = command.Channel;
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