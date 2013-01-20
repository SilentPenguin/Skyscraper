using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Me")]
    public class Me : Command, ICommandHandler
    {
        public Me(Command command) : base(command) { }

        public void Execute(IConnectionManager connection) { }
    }
}