using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Me")]
    public class Me : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command) { }
    }
}