using System;
using Skyscraper.Irc;
using Skyscraper.Models;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Server")]
    public class Server : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command) 
        {
            string uri = command.Arguments[0];
            string protocol = "irc://";

            if (!uri.StartsWith(protocol))
            {
                uri = protocol + uri;
            }

            Uri networkUrl = new Uri(uri);

            if (networkUrl.Port < 0)
            {
                uri += ":6667";
            }

            networkUrl = new Uri(uri);

            INetwork network = new Network
            {
                Url = networkUrl,
            };

            connection.Connect(network);
        }
    }
}