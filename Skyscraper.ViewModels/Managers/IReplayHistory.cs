using System;
using Skyscraper.Models;
using Skyscraper.ClientCommands;

namespace Skyscraper.ViewModels.Behaviours
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