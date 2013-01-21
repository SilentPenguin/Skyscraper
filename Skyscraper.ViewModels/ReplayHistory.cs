using System;
using System.Collections.Generic;
using System.Linq;
using Skyscraper.Models;
using Skyscraper.ClientCommands;

namespace Skyscraper.Utilities
{
    public class ReplayHistory : IReplayHistory
    {
        private IList<String> CommandHistory { get; set; }
        private Int32 HistoryLocation { get; set; }
        private const Int32 MaximumSize = 200;
        private const Boolean BringResubmittedCommandsToTheFront = true;

        public Boolean IsReplaying
        {
            get
            {
                return this.HistoryLocation.Between(0, this.CommandHistory.Count() - 1);
            }
        }

        public ReplayHistory()
        {
            this.CommandHistory = new List<String>();
        }

        public void Add(ICommand command)
        {
            this.Add(command.Text);
        }

        public void Add(String command)
        {
            if (String.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException("command");
            }

            if (this.CommandHistory.Contains(command) && BringResubmittedCommandsToTheFront)
            {
                this.CommandHistory.Remove(command);
            }

            this.CommandHistory.Add(command);

            if (this.CommandHistory.Count() > MaximumSize)
            {
                this.CommandHistory.RemoveAt(0);
            }

            this.HistoryLocation = this.CommandHistory.Count();
        }

        private String GetCommandAtLocation(Int32 index)
        {
            return this.CommandHistory.WithinBounds(index) ? this.CommandHistory[index] : String.Empty;
        }

        public String GetPreviousCommand() 
        {
            if (--this.HistoryLocation < 0)
            {
                this.HistoryLocation = this.CommandHistory.Count(); //returns empty on the loop around.
            }

            return this.GetCommandAtLocation(this.HistoryLocation);
        }

        public String GetNextCommand() 
        {
            ++this.HistoryLocation;

            if (this.HistoryLocation > this.CommandHistory.Count())
            {
                this.HistoryLocation = 0;
            }

            return this.GetCommandAtLocation(this.HistoryLocation);
        }
    }
}