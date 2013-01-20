using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc.Events
{
    public class NetworkEventArgs : ObjectEventArgs<INetwork>
    {
        public NetworkEventArgs(INetwork network) : base(network) { }
        public INetwork Network { get { return this.Object; } set { this.Object = value; } }
    }
}
