using System;
using Skyscraper.Models;

namespace Skyscraper.Irc.Events
{
    public class ChannelEventArgs : ObjectEventArgs<IChannel>
    {
        public ChannelEventArgs(IChannel channel) : base(channel) { }
        public IChannel Channel { get { return this.Object; } set { this.Object = value; } }
    }
}