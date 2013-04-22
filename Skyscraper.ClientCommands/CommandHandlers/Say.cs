using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [CommandHandler("Say")]
    public class Say : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            connection.Send(command.Channel, command.Body);
        }
    }
}