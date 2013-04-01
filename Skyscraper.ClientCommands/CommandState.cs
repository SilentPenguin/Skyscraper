using Skyscraper.Irc;

namespace Skyscraper.ClientCommands
{
    public class CommandState : ICommandState
    {
        public ICommandHandler Handler { get; set; }
        public ICommand Command { get; set; }

        public CommandState(ICommandHandler handler, ICommand command)
        {
            this.Handler = handler;
            this.Command = command;
        }

        public void Execute(IConnectionManager connection)
        {
            this.Handler.Execute(connection, this.Command);
        }
    }
}