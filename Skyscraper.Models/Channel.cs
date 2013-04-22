using System.Collections.ObjectModel;
using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public class Channel : NotifyPropertyChangedBase, IChannel
    {
        private INetwork network;
        public INetwork Network
        {
            get
            {
                return this.network;
            }
            set
            {
                this.SetProperty(ref this.network, value);
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.SetProperty(ref this.name, value);
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

        private string topic;
        public string Topic
        {
            get
            {
                return this.topic;
            }
            set
            {
                this.SetProperty(ref this.topic, value);
            }
        }

        private ObservableCollection<IChannelUser> users;
        public ObservableCollection<IChannelUser> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.SetProperty(ref this.users, value);
            }
        }

        private ObservableCollection<ILogEntry> log;
        public ObservableCollection<ILogEntry> Log
        {
            get
            {
                return this.log;
            }
            set
            {
                this.SetProperty(ref this.log, value);
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

        public Channel()
        {
            this.Users = new ObservableCollection<IChannelUser>();
            this.Log = new ObservableCollection<ILogEntry>();
        }
    }
}