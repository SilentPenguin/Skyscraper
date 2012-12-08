using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Skyscraper.Models
{


    public interface INetwork : INotifyPropertyChanged
    {
        String Name { get; }
        Uri Url { get; }
        ObservableCollection<IChannel> Channels { get; set; }
        Boolean IsConnected { get; set; }
    }

    class Network : NotifyPropertyChangedBase, INetwork
    {
        public Network()
        {
            isConnected = false;
            channels = new ObservableCollection<IChannel>();
        }

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
        private String _Name { get; set; }
        public String Name
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this._Name))
                {
                    //if a custom name gets defined somewhere along the line, return that instead
                    return this._Name;
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

        public override string ToString()
        {
            return Url.ToString();
        }
    }
}
