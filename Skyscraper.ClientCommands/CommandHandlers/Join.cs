using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Join")]
    public class Join : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.Join(command.Network, command.Body);
        }
    }
}