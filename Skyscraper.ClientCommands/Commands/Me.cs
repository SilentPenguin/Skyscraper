using Skyscraper.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands.Commands
{
    [CommandType(CommandType.Me)]
    class Me : Command, ICommand
    {
        public Me(Command command) : base(command) { }

        public void Execute(IConnectionManager connection)
        {

        }
    }
}
