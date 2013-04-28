using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IConnectionSettings
    {
        Uri Host { get; }
        Encoding Encoding { get; }
        UserSettings User { get; }
    }
}
