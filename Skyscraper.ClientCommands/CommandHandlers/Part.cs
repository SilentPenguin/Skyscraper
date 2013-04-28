using Skyscraper.Irc;
using Skyscraper.Utilities;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [Handler("Part")]
    public class Part : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command) 
        {
            connection.Part(command.Network, command.Body);
        }
    }
}