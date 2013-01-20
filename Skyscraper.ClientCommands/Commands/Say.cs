using Skyscraper.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Skyscraper.ClientCommands.Commands
{
    [CommandType("Say")]
    class Say : Command, ICommand
    {
        public Say(Command command) : base(command) { }

        public void Execute(IConnectionManager connection)
        {
            connection.Send(this.Channel, this.Body);
        }
    }
}
