using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Behaviours.TabHandlers
{
    class ChannelHandler : ITabHandler
    {
        public IEnumerable<ITabResult> GetTabResults(Models.IClient client, ITabQuery query)
        {
             return client.Channels
                 .Where( channel => channel.Name.StartsWith(query.Keyword))
                 .Select(match => 
                     new TabResult 
                     {
                         Text = query.ReplaceKeyword(match.Name),
                         Channel = match,
                     });
        }
    }
}
