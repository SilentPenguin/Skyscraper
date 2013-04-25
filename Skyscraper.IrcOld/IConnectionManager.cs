using Skyscraper.Models;

namespace Skyscraper.Irc
{
    public interface IConnectionManager
    {
        INetwork Connect(INetwork network, IUser userInformation);
        void Disconnect(INetwork connection, string message);
        void Join(INetwork connection, string channelName);
        void Part(INetwork connection, string channelName);
        void Send(IChannel channel, string message);
        void SendAction(IChannel channel, string action);
        void SetNickname(INetwork connection, string newNickname);
    }
}