﻿using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Utilities
{
    interface IReplayHistory
    {
        Boolean IsReplaying { get; }
        void Add(ICommand Command);
        void Add(String Command);
        String GetPreviousCommand();
        String GetNextCommand();
    }

    class ReplayHistory : IReplayHistory
    {
        private IList<String> CommandHistory { get; set; }
        private Int32 HistoryLocation { get; set; }
        private const Int32 MaximumSize = 200;
        private const Boolean BringResubmittedCommandsToTheFront = true;

        public ReplayHistory()
        {
            this.CommandHistory = new List<String>();
        }

        public Boolean IsReplaying
        {
            get
            {
                return this.HistoryLocation.Between(0, this.CommandHistory.Count() - 1);
            }
        }
        public void Add(ICommand command)
        {
            this.Add(command.Command);
        }

        public void Add(String command){
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

        public String GetPreviousCommand() {
            if (--this.HistoryLocation < 0)
            {
                this.HistoryLocation = this.CommandHistory.Count(); //returns empty on the loop around.
            }
            return this.GetCommandAtLocation(this.HistoryLocation);
        }

        public String GetNextCommand() {
            ++this.HistoryLocation;
            if (this.HistoryLocation > this.CommandHistory.Count())
            {
                this.HistoryLocation = 0;
            }
            return this.GetCommandAtLocation(this.HistoryLocation);
        }
    }
}
