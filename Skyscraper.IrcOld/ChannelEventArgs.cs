using System;
using Skyscraper.Models;

namespace Skyscraper.Irc
{
    public class ChannelEventArgs : ObjectEventArgs<IChannel>
    {
        public IChannel Channel
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

        public ChannelEventArgs(IChannel channel) : base(channel) { }
    }
}