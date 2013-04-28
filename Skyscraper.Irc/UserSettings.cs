using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    class UserSettings :IUserSettings
    {
        private string nickname;
        public string Nickname
        {
            get
            {
                return this.nickname;
            }
            set
            {
                this.nickname = value;
            }
        }

        private string realname;
        public string Realname
        {
            get {
                return this.Realname;
            }
            set
            {
                this.Realname = value;
            }
        }

        private string username;
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }
    }
}
