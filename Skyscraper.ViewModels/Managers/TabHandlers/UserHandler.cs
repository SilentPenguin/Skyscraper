﻿using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Managers.TabHandlers
{
    class UserHandler : ITabHandler
    {
        public IEnumerable<ITabResult> GetTabResults(IClient client, ITabQuery query)
        {
            IEnumerable<ITabResult> results = new Collection<ITabResult>();
            if (client != null && client.Users != null)
            {
                results = results.Concat(this.GetLogResults(client, query));
                results = results.Concat(this.GetVisibleUsersResults(client, query));
                results = results.Concat(this.GetAllUsersResults(client, query));
            }
            return results.Distinct();
        }

        private IEnumerable<ITabResult> GetLogResults(IClient client, ITabQuery query)
        {
            var nickname = query.Keyword.ToLowerInvariant();

            return client.Log
                .Where(entry => entry.IsUserVisible && entry is IUserEvent)
                .Select(entry => entry as IUserEvent)
                .Where(entry => entry.Network.LocalUser.Nickname != entry.User.Nickname && entry.User.Nickname.ToLowerInvariant().StartsWith(nickname))
                .OrderBy(match => match.ReceivedAt)
                .Select(match => 
                    {
                        var text = query.ReplaceKeyword(match.User.Nickname);
                        return new TabResult
                        {
                            Text = text,
                            Channel = match.Source as IChannel,
                            CursorIndex = query.GetCursorIndexAtEndOfKeyword(text)
                        };
                    }
                );
        }

        private IEnumerable<ITabResult> GetVisibleUsersResults(IClient client, ITabQuery query)
        {
            var nickname = query.Keyword.ToLowerInvariant();

            return client.Users
                .Where(user => user.IsUserVisible && user.Nickname.ToLowerInvariant().StartsWith(nickname) && user.Nickname != user.Network.LocalUser.Nickname )
                .Select(match => 
                    {
                        var text = query.ReplaceKeyword(match.Nickname);
                        return new TabResult
                        {
                            Text = text,
                            Channel = match.Channels.FirstOrDefault(), //TODO change this to the last channel the user spoke in
                            CursorIndex = query.GetCursorIndexAtEndOfKeyword(text)
                        };
                    }
                );
        }

        private IEnumerable<ITabResult> GetAllUsersResults(IClient client, ITabQuery query)
        {
            var nickname = query.Keyword.ToLowerInvariant();

            return client.Users
                .Where(user => user.Nickname.ToLowerInvariant().StartsWith(nickname) && user.Nickname != user.Network.LocalUser.Nickname)
                .Select(match =>
                    {
                        var text = query.ReplaceKeyword(match.Nickname);
                        return new TabResult
                        {
                            Text = text,
                            Channel = match.Channels.FirstOrDefault(), //TODO change this to the last channel the user spoke in
                            CursorIndex = query.GetCursorIndexAtEndOfKeyword(text)
                        };
                    }
                );
        }
    }
}
