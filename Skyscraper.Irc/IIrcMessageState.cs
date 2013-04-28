using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IIrcMessageState
    {
        IIrcMessageHandler Handler { get; }
        IIrcMessage IrcMessage { get; }
    }
}
