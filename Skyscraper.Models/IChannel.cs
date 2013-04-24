using System;
using System.Collections.ObjectModel;

namespace Skyscraper.Models
{
    public interface IChannel : ILog, ILogSource
    {
        INetwork Network { get; }

        String Name { get; }
        String Modes { get; set; }
        String Topic { get; set; }
        ObservableCollection<IChannelUser> Users { get; set; }

        bool IsActiveChannel { get; set; }

        bool IsUserVisible { get; }
    }
}