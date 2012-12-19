using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public class Network : NotifyPropertyChangedBase, INetwork
    {
        private Regex urlRegularExpression = new Regex("");

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

        private String name { get; set; }
        public String Name
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.name))
                {
                    //if a custom name gets defined somewhere along the line, return that instead
                    return this.name;
                }

                if (!this.Url.HostNameType.Equals(UriHostNameType.Dns) || this.Url.IsLoopback)
                {
                    return string.Empty;
                }

                return this.Url.Host.Split('.').Last();
            }
        }
        
        public Uri Url { get; set; }

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

        public Network()
        {
            this.isConnected = false;
            this.channels = new ObservableCollection<IChannel>();
        }

        public override string ToString()
        {
            return this.Url.ToString();
        }
    }
}