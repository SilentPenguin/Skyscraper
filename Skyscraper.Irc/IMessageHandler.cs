using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IMessageHandler
    {
        void Execute(IMessage message, IConnection connection);
    }
}
