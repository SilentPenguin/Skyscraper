using System;
using Skyscraper.Models;

namespace Skyscraper.Irc
{
    public class JoinedChannelEventArgs : EventArgs
    {
        public IChannel Channel { get; set; }

        public JoinedChannelEventArgs(IChannel channel)
        {
            this.Channel = channel;
        }
    }
}