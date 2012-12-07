using System.Collections.ObjectModel;

namespace Skyscraper.Models
{
    public class Connection
    {
        public ObservableCollection<Channel> Channels { get; set; }

        public Connection()
        {
            this.Channels = new ObservableCollection<Channel>();
        }
    }
}