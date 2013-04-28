using Skyscraper.Irc.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    class ITcpEventClient
    {
        Encoding Encoding { get; }
        Uri Address { get; }
        bool IsConnected { get; }

        void Connect();
        void Disconnect();
        void Send(IRawMessage message);

        public event EventHandler<RawMessageEventArgs> Sent;
        public event EventHandler<RawMessageEventArgs> Recieved;
        public event EventHandler<EventArgs> Connected;
        public event EventHandler<EventArgs> Disconnected;
    }
}
