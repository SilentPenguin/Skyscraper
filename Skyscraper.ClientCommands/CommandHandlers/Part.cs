using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [CommandHandler("Part")]
    public class Part : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command) 
        {
            connection.Part(command.Network, command.Body);
        }
    }
}