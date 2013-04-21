using System;
using System.Collections.ObjectModel;

namespace Skyscraper.Models
{
    public interface IUser
    {

        INetwork Network { get; set; }
        ObservableCollection<IChannel> Channels { get; set; }

        String Nickname { get; set; }
        String Hostname { get; set; }
        String Realname { get; set; }
        String Modes { get; set; }
        bool IsAway { get; set; }

        bool IsUserVisible { get; }
    }
}