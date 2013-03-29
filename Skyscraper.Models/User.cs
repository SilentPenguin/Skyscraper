using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public class User : NotifyPropertyChangedBase, IUser
    {
        public User() { }
        public User(IUser user)
        {
            nickname = user.Nickname;
            hostname = user.Hostname;
            realname = user.Realname;
            modes = user.Modes;
            isAway = user.IsAway;
        }

        private string nickname;
        public string Nickname
        {
            get
            {
                return this.nickname;
            }
            set
            {
                this.SetProperty(ref this.nickname, value);
            }
        }

        private string hostname;
        public string Hostname
        {
            get
            {
                return this.hostname;
            }
            set
            {
                this.SetProperty(ref this.hostname, value);
            }
        }

        private string realname;
        public string Realname
        {
            get
            {
                return this.realname;
            }
            set
            {
                this.SetProperty(ref this.realname, value);
            }
        }

        private string modes;
        public string Modes
        {
            get
            {
                return this.modes;
            }
            set
            {
                this.SetProperty(ref this.modes, value);
            }
        }

        private bool isAway;
        public bool IsAway
        {
            get
            {
                return this.isAway;
            }
            set
            {
                this.SetProperty(ref this.isAway, value);
            }
        }
    }
}