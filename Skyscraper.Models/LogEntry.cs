using System;
using Skyscraper.Utilities;

namespace Skyscraper.Models
{
    public class LogEntry : NotifyPropertyChangedBase, ILogEntry
    {
        public LogEntry(INetwork network, ILogSource source)
        {
            this.Network = network;
            this.Source = source;
            this.ReceivedAt = DateTime.Now;
        }

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

        private bool isUserVisible;
        public bool IsUserVisible
        {
            get
            {
                return this.isUserVisible;
            }
            set
            {
                this.SetProperty(ref this.isUserVisible, value);
            }
        }
    }
}