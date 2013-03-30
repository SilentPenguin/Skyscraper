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
            string newNickname = command.Arguments[0];
            if (command.Network != null)
            {
                connection.SetNickname(command.Network, newNickname);
            }

            if (command.User != null)
            {
                command.User.Nickname = newNickname;
            }
        }
    }
}
