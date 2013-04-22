using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public class ChannelUser : NotifyPropertyChangedBase, IChannelUser
    {
        private IUser user;
        public IUser User
        {
            get { return this.user; }
            set { this.SetProperty(ref this.user, value); }
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

        public INetwork Network
        {
            get
            {
                return this.User.Network;
            }
            set
            {
                this.User.Network = value;
            }
        }

        public ObservableCollection<IChannel> Channels
        {
            get
            {
                return this.User.Channels;
            }
            set
            {
                this.User.Channels = value;
            }
        }

        public string Nickname
        {
            get
            {
                return this.User.Nickname;
            }
            set
            {
                this.User.Nickname = value;
            }
        }

        public string Hostname
        {
            get
            {
                return this.User.Hostname;
            }
            set
            {
                this.User.Hostname = value;
            }
        }

        public string Realname
        {
            get
            {
                return this.User.Realname;
            }
            set
            {
                this.User.Realname = value;
            }
        }

        public bool IsAway
        {
            get
            {
                return this.User.IsAway;
            }
            set
            {
                this.User.IsAway = value;
            }
        }

        public bool IsUserVisible
        {
            get { return this.User.IsUserVisible; }
        }
    }
}
