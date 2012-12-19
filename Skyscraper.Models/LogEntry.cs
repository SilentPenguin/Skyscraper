using System;
using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public interface ILogEntry
    {
        DateTime ReceivedAt { get; }
    }

    public class LogEntry : NotifyPropertyChangedBase, ILogEntry
    {
        private DateTime receivedAt;
        public DateTime ReceivedAt
        {
            get
            {
                return this.receivedAt;
            }
            set
            {
                this.SetProperty(ref this.receivedAt, value);
            }
        }

        public LogEntry()
        {
            this.ReceivedAt = DateTime.Now;
        }
    }
}