using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface IUserEvent : ILogEntry
    {
        IUser User { get; }
        String NicknameContinuity { get; }
    }

    public class UserEvent : LogEntry, IUserEvent
    {
        public UserEvent(IUser User)
        {
            this.User = User;
            this.NicknameContinuity = User.Nickname;
        }

        public IUser User { get; set; }
        /* 
         * this is not the same as User.Nickname, which is always the current user's nickname,
         * Instead this is the value of User.Nickname when the message was recieved.
         * This is for logs, so that when someone changes their nickname, the log still reads their original name
         * Making it possible to follow them when reading the log fresh.
         */
        public String NicknameContinuity { get; set; }
    }
}
