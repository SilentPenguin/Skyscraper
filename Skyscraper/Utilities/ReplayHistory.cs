using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Utilities
{
    interface IReplayHistory
    {
        void Add(String Command);
        String GetPreviousCommand();
        String GetNextCommand();
    }

    class ReplayHistory : IReplayHistory
    {
        private IList<String> CommandHistory { get; set; }
        private Int32 HistoryLocation { get; set; }
        private const Int32 MaximumSize = 200;

        public ReplayHistory()
        {
            this.CommandHistory = new List<String>();
        }

        public void Add(String command){
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
            if (this.HistoryLocation + 1 <= this.CommandHistory.Count())
            {
                ++this.HistoryLocation;
            }
            return this.GetCommandAtLocation(this.HistoryLocation);
        }
    }
}
