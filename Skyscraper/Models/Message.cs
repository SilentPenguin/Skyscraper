using System;

namespace Skyscraper.Models
{
    public interface IMessage : IUserEvent
    {
        String MessageBody { get; }
    }

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

        public Message(IUser user, String message) : base(user)
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