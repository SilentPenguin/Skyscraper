using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Join")]
    public class Join : Command, ICommandHandler
    {
        public Join(Command command) : base(command) { }

        public void Execute(IConnectionManager connection)
        {
            connection.Join(this.Network, this.Body);
        }
    }
}