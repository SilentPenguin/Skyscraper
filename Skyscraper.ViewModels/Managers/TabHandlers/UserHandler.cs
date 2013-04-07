using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Behaviours.TabHandlers
{
    class UserHandler : ITabHandler
    {
        public IEnumerable<ITabResult> GetTabResults(IClient client, ITabQuery query)
        {
            ICollection<ITabResult> results = new Collection<ITabResult>();
            if (client != null && client.Users != null)
            {
                results.Concat(this.GetLogResults(client, query));
                results.Concat(this.GetVisibleUsersResults(client, query));
                results.Concat(this.GetAllUsersResults(client, query));
            }
            return results.Distinct();
        }

        private IEnumerable<ITabResult> GetLogResults(IClient client, ITabQuery query)
        {
            return client.Log
                .Where(entry => entry.IsUserVisible && entry is IUserEvent)
                .Select(match => match as IUserEvent)
                .OrderBy(match => match.ReceivedAt)
                .Select(match => 
                    new TabResult 
                    {
                        Text = query.ReplaceKeyword(match.User.Nickname),
                        Channel = match.Source as IChannel,
                    });
        }

        private IEnumerable<ITabResult> GetVisibleUsersResults(IClient client, ITabQuery query)
        {
            return client.Users
                .Where(user => user.IsUserVisible)
                .Select(match =>
                    new TabResult
                    {
                        Text = query.ReplaceKeyword(match.Nickname),
                        Channel = match.Channels.FirstOrDefault(),
                    });
        }

        private IEnumerable<ITabResult> GetAllUsersResults(IClient client, ITabQuery query)
        {
            return client.Users
                .Select(match =>
                    new TabResult
                    {
                        Text = query.ReplaceKeyword(match.Nickname),
                        Channel = match.Channels.FirstOrDefault(),
                    });
        }
    }
}
