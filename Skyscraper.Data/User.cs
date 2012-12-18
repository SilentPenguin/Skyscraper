using Skyscraper.Utilities;

namespace Skyscraper.Data
{
    public class User : NotifyPropertyChangedBase, IUser
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