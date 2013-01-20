using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Part")]
    public class Part : Command, ICommandHandler
    {
        public Part(Command command) : base(command) { }

        public void Execute(IConnectionManager connection) { }
    }
}