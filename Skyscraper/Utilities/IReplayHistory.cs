using System;
using Skyscraper.Data;

namespace Skyscraper.Utilities
{
    public interface IReplayHistory
    {
        Boolean IsReplaying { get; }
        void Add(ICommand Command);
        void Add(String Command);
        String GetPreviousCommand();
        String GetNextCommand();
    }
}