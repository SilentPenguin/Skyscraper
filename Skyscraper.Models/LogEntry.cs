using System;
using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public interface ILogEntry
    {
        INetwork Network { get; }
        ILogSource Source { get; }
        DateTime ReceivedAt { get; }
    }

    public class LogEntry : NotifyPropertyChangedBase, ILogEntry
    {
        private INetwork network;
        public INetwork Network
        {
            get
            {
                return this.network;
            }
            set
            {
                this.SetProperty(ref this.network, value);
            }
        }

        private ILogSource source;
        public ILogSource Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.SetProperty(ref this.source, value);
            }
        }

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

        public LogEntry(INetwork network, ILogSource source)
        {
            this.Network = network;
            this.Source = source;
            this.ReceivedAt = DateTime.Now;
        }
    }
}