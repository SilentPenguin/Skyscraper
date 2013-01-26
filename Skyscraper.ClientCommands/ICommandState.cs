using System;
namespace Skyscraper.ClientCommands
{
    public interface ICommandState
    {
        ICommand Command { get; set; }
        ICommandHandler Handler { get; set; }
        void Execute(Skyscraper.Irc.IConnectionManager connection);
    }
}
