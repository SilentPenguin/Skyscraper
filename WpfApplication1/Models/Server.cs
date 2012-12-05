using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Skyscraper.Models
{


    public interface INetwork
    {
        String Name { get; }
        Uri Url { get; }
        IList<IChannel> Channels { get; set; }
    }

    class Network : INetwork
    {
        private Regex urlRegularExpression = new Regex("");
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
        public IList<IChannel> Channels { get; set; }
        public override string ToString()
        {
            return Url.ToString();
        }
    }
}
