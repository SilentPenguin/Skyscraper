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
    class Ping : IMessageHandler
    {
        public void Execute(IMessage message, IConnection connection, IClient client)
        {
            connection.SendRawMessage("PONG :{0}", message.Arguments[0]);
        }
    }
}
