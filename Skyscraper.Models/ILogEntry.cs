using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Models
{
    public interface ILogEntry
    {
        INetwork Network { get; }
        ILogSource Source { get; }
        DateTime ReceivedAt { get; }

        bool IsUserVisible { get; }
    }
}
