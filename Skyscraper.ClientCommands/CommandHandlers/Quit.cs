using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Quit")]
    public class Quit : Command, ICommandHandler
    {
        public Quit(Command command) : base(command) { }

        public void Execute(IConnectionManager connection)
        {
            connection.Disconnect(this.Network, this.Body);
        }
    }
}