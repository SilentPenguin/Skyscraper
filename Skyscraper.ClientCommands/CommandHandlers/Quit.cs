using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [CommandHandler("Quit")]
    public class Quit : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.Disconnect(command.Network, command.Body);
        }
    }
}