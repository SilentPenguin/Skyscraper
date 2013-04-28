using Skyscraper.Irc;
using Skyscraper.Utilities;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [Handler("Me")]
    public class Me : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.SendAction(command.Channel, command.Body);
        }
    }
}