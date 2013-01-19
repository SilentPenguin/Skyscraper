using Skyscraper.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands
{
    [CommandType(CommandType.Quit)]
    class Quit : Command, ICommand
    {
        public Quit(Command command) : base(command) { }

        public void Execute(IConnectionManager connection)
        {
            connection.Disconnect(this.Network, this.Body);
        }
    }
}
