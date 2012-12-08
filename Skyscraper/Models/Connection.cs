using System.Collections.ObjectModel;
using Skyscraper.Utilities;
using System;
using System.ComponentModel;

namespace Skyscraper.Models
{
    public interface IConnection : INotifyPropertyChanged
    {
        ObservableCollection<IChannel> Channels { get; set; }
        Boolean IsConnected { get; set; }
    }

    public class Connection : NotifityPropertyChangedBase, IConnection
    {
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

        private bool isConnected;
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
            set
            {
                this.SetProperty(ref this.isConnected, value);
            }
        }

        public Connection()
        {
            this.Channels = new ObservableCollection<IChannel>();
            this.IsConnected = false;
        }
    }
}