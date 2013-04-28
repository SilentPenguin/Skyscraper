using Skyscraper.Irc;
using Skyscraper.Utilities;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [Handler("Quit")]
    public class Quit : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.Disconnect(command.Network, command.Body);
        }
    }
}