using Skyscraper.Models;
using System;

namespace Skyscraper.Irc
{
    public interface IConnectionManager
    {
        INetwork Connect(INetwork network, IUser userInformation);
        void Disconnect(INetwork connection, string message = null);
        void Join(INetwork connection, string channelName);
        void Send(IChannel channel, IUser user, string message);
    }
}
