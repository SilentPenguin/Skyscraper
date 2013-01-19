using Skyscraper.Irc;
using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands.Commands
{
    [CommandType(CommandType.Server)]
    class Server : Command, ICommand
    {
        public Server(Command command) : base(command) { }

        public void Execute(IConnectionManager connection) {
            String uri = this.Arguments[0];
            String protocol = "irc://";
            if (!uri.StartsWith(protocol))
            {
                uri = protocol + uri;
            }
            Uri networkUrl = new Uri(uri);
            if (networkUrl.Port < 0)
            {
                Int16 port = 6667;
                uri += ":" + port;
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
