using Skyscraper.ClientCommands;
using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Behaviours.TabHandlers
{
    class CommandHandler : ITabHandler
    {
        private ICollection<String> availableCommands { get; set; }

        public CommandHandler()
        {
            this.availableCommands = CommandFactory.AvailableCommands;
        }

        
        public IEnumerable<ITabResult> GetTabResults(IClient client, ITabQuery query)
        {
            string firstWord = query.Text.Split(' ').First();
            if (!string.IsNullOrWhiteSpace(firstWord) && firstWord.StartsWith("/") && firstWord.Count() > 1 &&
               (firstWord.Count() >= query.CursorLocation))
            {
                string commandText = firstWord.Substring(1).ToUpperInvariant();
                IEnumerable<string> matchedCommands = this.availableCommands.Where(command => command.ToUpperInvariant().StartsWith(commandText));
                return matchedCommands.Select(match =>
                    {
                        var text = query.ReplaceKeyword(match.ToLowerInvariant());
                        return new TabResult
                        {
                            Text = text,
                            CursorIndex = query.GetCursorIndexAtEndOfKeyword(text),
                        };
                    }
                );
            }
            return new Collection<ITabResult>();
        }
    }
}
