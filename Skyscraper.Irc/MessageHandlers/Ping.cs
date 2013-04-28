using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyscraper.Utilities;
using Skyscraper.Models;

namespace Skyscraper.Irc.MessageHandlers
{
    [Handler("PING")]
    class Ping : IIrcMessageHandler
    {
        public void Execute(IIrcMessage message, IIrcConnection connection, IClient client)
        {
            connection.SendRawMessage("PONG :{0}", message.Arguments[0]);
        }
    }
}
