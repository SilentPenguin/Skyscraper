using Skyscraper.ClientCommands;
using Skyscraper.Models;
using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Behaviours
{
    class TabComplete : ITabComplete
    {
        private IClient client { get; set; }
        private TypeList<ITabHandler> tabHandlers { get; set; }

        public TabComplete(IClient client)
        {
            this.client = client;
            this.tabHandlers = new TypeList<ITabHandler>(
                TypeHelpers.ClassesForInterfaceInAssembly<ITabHandler>().ToList()
            );
        }

        public ITabResult GetTabResults(ITabQuery query)
        {
            IEnumerable<ITabResult> results = new Collection<ITabResult>();
            foreach(ITabHandler handler in this.tabHandlers){
                IEnumerable<ITabResult> handlerMatches = handler.GetTabResults(this.client, query);
                if (handlerMatches != null && handlerMatches.Count() > 0)
                {
                    results.Concat(handlerMatches);
                }
            }
            return results.FirstOrDefault();
        }
    }
}
