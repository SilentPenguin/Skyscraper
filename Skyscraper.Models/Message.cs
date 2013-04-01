using System;

namespace Skyscraper.Models
{
    public class Message : UserEvent, IMessage
    {
        private String messageBody;
        public String MessageBody
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

        public Message(INetwork network, ILogSource source, IUser user, string message)
            : base(network, source, user)
        {
            this.User = user;
            this.NicknameContinuity = user.Nickname;
            this.MessageBody = message;
        }

        public override string ToString()
        {
            return this.MessageBody;
        }
    }
}