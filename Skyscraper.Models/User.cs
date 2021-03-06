﻿using Skyscraper.Utilities;
using System.Collections.ObjectModel;

namespace Skyscraper.Models
{
    public class User : NotifyPropertyChangedBase, IUser, ILogSource
    {
        public User()
        {
            this.Channels = new ObservableCollection<IChannel>();
        }

        public User(IUser user)
            : this()
        {
            nickname = user.Nickname;
            hostname = user.Hostname;
            realname = user.Realname;
            modes = user.Modes;
            isAway = user.IsAway;
        }

        private INetwork network;
        public INetwork Network
        {
            get { return this.network; }
            set { this.SetProperty(ref this.network, value); }
        }

        private ObservableCollection<IChannel> channels;
        public ObservableCollection<IChannel> Channels
        {
            get
            {
                return this.channels;
            }
            set
            {
                this.SetProperty(ref this.channels, value);
            }
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

        public string Name
        {
            get
            {
                return this.nickname;
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

        private bool isUserVisible;
        public bool IsUserVisible
        {
            get
            {
                return this.isUserVisible;
            }
            set
            {
                this.SetProperty(ref this.isUserVisible, value);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User user = (User)obj;

            return this.Nickname == user.Nickname;
        }

        public override int GetHashCode()
        {
            return this.Nickname.GetHashCode();
        }
    }
}