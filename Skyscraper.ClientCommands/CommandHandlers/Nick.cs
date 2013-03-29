using Skyscraper.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Nick")]
    class Nick : ICommandHandler
    {
        void ICommandHandler.Execute(IConnectionManager connection, ICommand command)
        {
            if (command.Network != null)
            {

            }

            if (command.User != null)
            {
                command.User.Nickname = command.Arguments[0];
            }
        }
    }
}
