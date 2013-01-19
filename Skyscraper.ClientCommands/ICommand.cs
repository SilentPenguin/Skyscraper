using Skyscraper.Irc;
using Skyscraper.Models;
using System;

namespace Skyscraper.ClientCommands
{
    public interface ICommand
    {
        INetwork Network { get; }
        IChannel Channel { get; }
        String Text { get; } //the original text typed by the user
        String Body { get; } //
        CommandType Type { get; } //the type of command
        String[] Arguments { get; } // each individual argument typed

        void Execute(IConnectionManager connection);
    }
}