using Skyscraper.Irc;

namespace Skyscraper.ClientCommands.CommandHandlers
{
    [TextCommandHandler("Nick")]
    public class Nick : ICommandHandler
    {
        public void Execute(IConnectionManager connection, ICommand command)
        {
            string newNickname = command.Arguments[0];

            if (command.Network != null)
            {
                connection.SetNickname(command.Network, newNickname);
            }

            if (command.User != null)
            {
                command.User.Nickname = newNickname;
            }
        }
    }
}