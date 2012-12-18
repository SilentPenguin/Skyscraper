﻿using System;

namespace Skyscraper.Data
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

        public UserEvent(IUser user)
        {
            this.User = user;
            this.NicknameContinuity = this.User.Nickname;
        }
    }
}