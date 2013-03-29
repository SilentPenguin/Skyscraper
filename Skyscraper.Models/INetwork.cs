using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Skyscraper.Models
{
    public interface INetwork : INotifyPropertyChanged
    {
        String Name { get; }
        Uri Url { get; }
        IUser LocalUser { get; }
        ObservableCollection<IChannel> Channels { get; set; }
        Boolean IsConnected { get; set; }
    }
}