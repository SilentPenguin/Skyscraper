using Skyscraper.Models;

namespace Skyscraper.Irc
{
    public class NetworkEventArgs : ObjectEventArgs<INetwork>
    {
        public INetwork Network
        {
            get
            {
                return this.Object;
            }
            set
            {
                this.Object = value;
            }
        }

        public NetworkEventArgs(INetwork network) : base(network) { }
    }
}