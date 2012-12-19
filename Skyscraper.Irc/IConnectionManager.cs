using System;
namespace Skyscraper.Irc
{
    interface IConnectionManager
    {
        Skyscraper.Data.INetwork Connect(Skyscraper.Data.INetwork network);
        void Disconnect(Skyscraper.Data.INetwork connection, string message = null);
        void Join(Skyscraper.Data.INetwork connection, string channelName);
        void Send(Skyscraper.Data.IChannel channel, string message);
    }
}
