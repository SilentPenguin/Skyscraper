using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IMessageState
    {
        IMessageHandler Handler { get; }
        IMessage IrcMessage { get; }
    }
}
