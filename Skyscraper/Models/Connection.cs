using System.Collections.ObjectModel;
using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public class Connection : NotifityPropertyChangedBase
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