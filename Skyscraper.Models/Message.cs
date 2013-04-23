using System;

namespace Skyscraper.Models
{
    public class Message : UserEvent, IMessage
    {
        private string messageBody;
        public string MessageBody
        {
            get
            {
                return this.messageBody;
            }
            set
            {
                this.SetProperty(ref this.messageBody, value);
            }
        }

        private bool highlight;
        public bool Highlight
        {
            get
            {
                return this.highlight;
            }
            set
            {
                this.SetProperty(ref this.highlight, value);
            }
        }
        

        public Message(INetwork network, ILogSource source, IUser user, string message, bool highlight)
            : base(network, source, user)
        {
            this.User = user;
            this.NicknameContinuity = user.Nickname;
            this.MessageBody = message;
            this.Highlight = highlight;
        }

        public override string ToString()
        {
            return this.MessageBody;
        }
    }
}