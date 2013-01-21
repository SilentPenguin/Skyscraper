using Skyscraper.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands
{
    public interface ICommandHandler
    {
        void Execute(IConnectionManager connection, ICommand command);
    }
}
