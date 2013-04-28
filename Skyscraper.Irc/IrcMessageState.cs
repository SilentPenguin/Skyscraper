using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public class IrcMessageState : IIrcMessageState
    {
        public IrcMessageState(IIrcMessageHandler handler, IIrcMessage message)
        {
            this.Handler = handler;
            this.IrcMessage = message;
        }

        private IIrcMessageHandler handler;
        public IIrcMessageHandler Handler
        {
            get
            {
                return this.handler;
            }
            private set
            {
                this.handler = value;
            }
        }

        private IIrcMessage ircMessage;
        public IIrcMessage IrcMessage
        {
            get
            {
                return this.ircMessage;
            }
            private set
            {
                this.ircMessage = value;
            }
        }
    }
}
