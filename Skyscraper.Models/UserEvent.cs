using System;

namespace Skyscraper.Models
{
    public class UserEvent : LogEntry, IUserEvent
    {
        private IUser user;
        public IUser User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.SetProperty(ref this.user, value);
            }
        }

        private String nicknameContinuity;
        public String NicknameContinuity
        {
            get
            {
                return this.nicknameContinuity;
            }
            set
            {
                this.SetProperty(ref this.nicknameContinuity, value);
            }
        }

        public UserEvent(INetwork network, ILogSource source, IUser user) 
            : base(network, source)
        {
            this.User = user;
            this.NicknameContinuity = this.User.Nickname;
        }
    }
}