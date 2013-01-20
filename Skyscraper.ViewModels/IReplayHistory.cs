using System;
using Skyscraper.Models;
using Skyscraper.ClientCommands;

namespace Skyscraper.Utilities
{
    public interface IReplayHistory
    {
        Boolean IsReplaying { get; }
        void Add(ICommandHandler Command);
        void Add(String Command);
        String GetPreviousCommand();
        String GetNextCommand();
    }
}