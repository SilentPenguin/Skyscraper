using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Irc.MessageHandlers
{
    [Handler("PRIVMSG")]
    class PrivateMessage : IMessageHandler
    {
        public void Execute(IMessage message, IConnection connection)
        {
            
        }
    }
}
