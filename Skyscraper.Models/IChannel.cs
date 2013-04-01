using System;
using System.Collections.ObjectModel;

namespace Skyscraper.Models
{
    public interface IChannel : ILog, ILogSource
    {
        String Name { get; }
        String Modes { get; set; }
        String Topic { get; set; }
        ObservableCollection<IUser> Users { get; set; }
    }
}