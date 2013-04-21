using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Managers
{
    public interface ITabHandler
    {
        IEnumerable<ITabResult> GetTabResults(IClient client, ITabQuery query);
    }
}
