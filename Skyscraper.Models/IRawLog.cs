using System.Collections.ObjectModel;

namespace Skyscraper.Models
{
    public interface IRawLog
    {
        ObservableCollection<RawMessage> Log { get; set; }
    }
}