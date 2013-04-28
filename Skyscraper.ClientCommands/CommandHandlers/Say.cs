using Skyscraper.Irc;
using Skyscraper.Utilities;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [Handler("Say")]
    public class Say : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.Send(command.Channel, command.Body);
        }
    }
}