using Skyscraper.Irc;

namespace Skyscraper.ClientCommands
{
    public interface ICommandHandler
    {
        void Execute(IConnectionManager connection, ICommand command);
    }
}