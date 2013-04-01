using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface ILog
    {
        ObservableCollection<ILogEntry> Log { get; set; }
    }
}
