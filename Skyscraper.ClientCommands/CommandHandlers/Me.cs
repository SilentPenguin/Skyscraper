using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [CommandHandler("Me")]
    public class Me : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.SendAction(command.Channel, command.Body);
        }
    }
}