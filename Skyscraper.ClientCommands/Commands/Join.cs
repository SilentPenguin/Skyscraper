using Skyscraper.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands.Commands
{
    [CommandType(CommandType.Join)]
    class Join : Command, ICommand
    {
        public Join(Command command) : base(command) { }

        public void Execute(IConnectionManager connection)
        {
            connection.Join(this.Network, this.Body);
        }
    }
}
