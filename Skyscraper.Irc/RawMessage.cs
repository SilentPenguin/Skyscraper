using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    class RawMessage : IRawMessage
    {
        public RawMessageDirection Direction { get; set; }
        public string Text { get; set; }
    }
}
