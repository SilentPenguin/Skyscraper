using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Managers.TabHandlers
{
    class ChannelHandler : ITabHandler
    {
        public IEnumerable<ITabResult> GetTabResults(Models.IClient client, ITabQuery query)
        {
            string keyword = query.Keyword.ToLowerInvariant();

            return client.Channels
                .Where(channel => channel.Name.ToLowerInvariant().StartsWith(keyword))
                .Select(match => 
                    {
                        var text = query.ReplaceKeyword(match.Name);

                        return new TabResult
                        {
                            Text = text,
                            SelectedText = !text.Contains(' ') ? new Range { LowerBound = 0, UpperBound = text.Length } : null,
                            Channel = match,
                            CursorIndex = query.GetCursorIndexAtEndOfKeyword(text),
                        };
                    }
                );
        }
    }
}
