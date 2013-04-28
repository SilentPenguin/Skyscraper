using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IIrcConnectionSettings
    {
        Uri Host { get; }
        Encoding Encoding { get; }
        IrcUserSettings User { get; }
    }
}
