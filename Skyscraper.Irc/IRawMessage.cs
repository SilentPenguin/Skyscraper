using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IRawMessage
    {
        Direction Direction { get; }
        string Text { get; }
    }
}
