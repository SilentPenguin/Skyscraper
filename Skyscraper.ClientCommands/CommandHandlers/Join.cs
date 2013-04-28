using Skyscraper.Irc;
using Skyscraper.Utilities;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [Handler("Join")]
    public class Join : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.Join(command.Network, command.Body);
        }
    }
}