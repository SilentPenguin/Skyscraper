using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public class MessageState : IMessageState
    {
        public MessageState(IMessageHandler handler, IMessage message)
        {
            this.Handler = handler;
            this.IrcMessage = message;
        }

        private IMessageHandler handler;
        public IMessageHandler Handler
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

        private IMessage ircMessage;
        public IMessage IrcMessage
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
