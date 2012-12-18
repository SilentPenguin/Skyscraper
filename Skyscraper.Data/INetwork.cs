using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Skyscraper.Data
{
    public interface INetwork : INotifyPropertyChanged
    {
        String Name { get; }
        Uri Url { get; }
        ObservableCollection<IChannel> Channels { get; set; }
        Boolean IsConnected { get; set; }
    }
}