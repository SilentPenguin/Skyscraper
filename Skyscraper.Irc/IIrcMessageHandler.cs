using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IIrcMessageHandler
    {
        void Execute(IIrcMessage message, IIrcConnection connection, IClient client);
    }
}
