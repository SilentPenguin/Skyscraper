using System;
using System.Collections.ObjectModel;

namespace Skyscraper.Models
{
    public interface IChannel
    {
        String Name { get; }
        String Modes { get; set; }
        String Topic { get; set; }
        ObservableCollection<IUser> Users { get; set; }
        ObservableCollection<ILogEntry> Log { get; set; }
    }
}