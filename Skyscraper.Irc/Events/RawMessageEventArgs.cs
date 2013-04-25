﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc.Events
{
    public class RawMessageEventArgs : EventArgs
    {
        public IAsyncResult Result { get; set; }
        public IRawMessage Message { get; set; }
    }
}
