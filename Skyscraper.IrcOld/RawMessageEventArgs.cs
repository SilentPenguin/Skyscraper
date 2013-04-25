using System;
using Skyscraper.Irc;
using Skyscraper.Models;

namespace Skyscraper.Irc.Events
{
    public class RawMessageEventArgs : EventArgs
    {
        public RawMessage RawMessage { get; private set; }

        public RawMessageEventArgs(RawMessage rawMessage)
        {
            this.RawMessage = rawMessage;
        }
    }
}