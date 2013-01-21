using Skyscraper.Irc;
using Skyscraper.Models;
using System;
using System.Linq;

namespace Skyscraper.ClientCommands
{
    public class Command : ICommand
    {
        public Command(Command command) : this(command.Text)
        {
            this.network = command.Network;
            this.channel = command.Channel;
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

        private INetwork network;
        private IChannel channel;
        public INetwork Network { get { return network; } set { network = value; } }
        public IChannel Channel { get { return channel; } set { channel = value; } }

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
    }
}