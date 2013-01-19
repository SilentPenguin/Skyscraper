using System;
namespace Skyscraper.Irc
{
    public interface IConnectionManager
    {
        Skyscraper.Models.INetwork Connect(Skyscraper.Models.INetwork network);
        void Disconnect(Skyscraper.Models.INetwork connection, string message = null);
        void Join(Skyscraper.Models.INetwork connection, string channelName);
        void Send(Skyscraper.Models.IChannel channel, string message);
    }
}
