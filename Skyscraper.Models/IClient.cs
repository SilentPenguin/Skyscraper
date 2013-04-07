using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface IClient : ILog
    {
        ObservableCollection<INetwork> Networks { get; set; }
        ObservableCollection<IChannel> Channels { get; set; }
        ObservableCollection<IUser> Users { get; set; }
    }
}
