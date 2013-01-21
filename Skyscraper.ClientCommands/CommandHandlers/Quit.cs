using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Quit")]
    public class Quit : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.Disconnect(command.Network, command.Body);
        }
    }
}