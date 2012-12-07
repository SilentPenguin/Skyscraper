using System;
using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public interface IUser
    {
        String Nickname { get; set; }
        String Hostname { get; set; }
        String Modes { get; set; }
        bool IsAway { get; set; }
    }

    public class User : NotifityPropertyChangedBase, IUser
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