using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public class Client : NotifyPropertyChangedBase, IClient
    {
        public ObservableCollection<INetwork> networks;
        public ObservableCollection<INetwork> Networks
        {
            get
            {
                return this.networks;
            }
            set
            {
                this.SetProperty(ref this.networks, value);
            }
        }

        public ObservableCollection<IChannel> channels;
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

        public ObservableCollection<IUser> users;
        public ObservableCollection<IUser> Users
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

        private bool alert;
        public bool Alert
        {
            get
            {
                return this.alert;
            }
            set
            {
                this.SetProperty(ref this.alert, value);
            }
        }

        private bool windowActive;
        public bool WindowActive
        {
            get
            {
                return this.windowActive;
            }
            set
            {
                this.SetProperty(ref this.windowActive, value);
                Alert &= !value;
            }
        }

        public Client()
        {
            this.Channels = new ObservableCollection<IChannel>();
            this.Networks = new ObservableCollection<INetwork>();
            this.Log = new ObservableCollection<ILogEntry>();
            this.Users = new ObservableCollection<IUser>();
        }
    }
}
