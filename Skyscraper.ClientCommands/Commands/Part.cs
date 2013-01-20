using Skyscraper.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands.Commands
{
    [CommandType("Part")]
    class Part : Command, ICommand
    {
        public Part(Command command) : base(command) { }

        public void Execute(IConnectionManager connection)
        {

        }
    }
}
