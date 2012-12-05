using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface IMessage : IUserEvent
    {
        String MessageBody { get; }
    }

    class Message : UserEvent, IMessage
    {
        public Message(IUser User, String Message) : base(User)
        {
            this.User = User;
            this.NicknameContinuity = User.Nickname;
            this.MessageBody = Message;
        }

        public String MessageBody { get; set; }

        public override string ToString()
        {
            return MessageBody;
        }
    }
}
