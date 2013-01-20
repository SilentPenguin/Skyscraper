using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Say")]
    public class Say : Command, ICommandHandler
    {
        public Say(Command command) : base(command) { }

        public void Execute(IConnectionManager connection)
        {
            connection.Send(this.Channel, this.Body);
        }
    }
}