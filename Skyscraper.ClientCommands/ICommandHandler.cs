using System;
using Skyscraper.Irc;
using Skyscraper.Models;

namespace Skyscraper.ClientCommands
{
    public interface ICommandHandler
    {
        INetwork Network { get; }
        IChannel Channel { get; }
        string Text { get; } //the original text typed by the user
        string Body { get; } //
        string CommandWord { get; } //the word that activates the command handler.
        string[] Arguments { get; } // each individual argument typed

        void Execute(IConnectionManager connection);
    }
}