using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Models
{
    public interface ILogEntry
    {
        DateTime ReceivedAt { get; }
    }

    public class LogEntry : ILogEntry
    {
        public LogEntry()
        {
            ReceivedAt = DateTime.Now;
        }

        public DateTime ReceivedAt { get; set; }
    }
}
