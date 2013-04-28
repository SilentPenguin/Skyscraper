using System;
using Skyscraper.Irc;
using Skyscraper.Models;
using Skyscraper.Utilities;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [Handler("Server")]
    public class Server : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command) 
        {
            INetwork network;

            if (command.Network == null)
            {
                //if the network infomation is not set explicitly
                //parse it out, when calling the command as 
                //a client command /server then this will be the
                //case.
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

                network = new Network
                {
                    Url = new Uri(uri),
                    LocalUser = new User(command.User),
                };
            }
            else
            {
                network = command.Network;
            }

            connection.Connect(network, command.User);
        }
    }
}