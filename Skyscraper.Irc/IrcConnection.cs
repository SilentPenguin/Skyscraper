using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Skyscraper.Irc.Events;
using Skyscraper.Models;

namespace Skyscraper.Irc
{
    class IrcConnection : TcpEventClient, IIrcConnection
    {
        public IrcConnection(IIrcConnectionSettings settings)
            : base(IrcConnection.addressFamily)
        {
            this.Encoding = Encoding.UTF8;
        }

        private const AddressFamily addressFamily = AddressFamily.InterNetwork;

        private Encoding Encoding { get; set; }

        private IIrcConnectionSettings settings { get; set; }
        
        public void Connect()
        {
            base.Connected += connection_Connected;
            base.Disconnected += connection_Disconnected;
            base.Sent += connection_Sent;
            base.Recieved += connection_Recieved;
            base.Connect();
        }

        void connection_Connected(object sender, EventArgs e)
        {

        }

        void connection_Disconnected(object sender, EventArgs e)
        {

        }

        void connection_Sent(object sender, RawMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
        void connection_Recieved(object sender, RawMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SendRawMessage(string rawMessage, params object[] formatArgs)
        {
            rawMessage = String.Format(rawMessage, formatArgs);
            IRawMessage message = new RawMessage { MessageText = rawMessage, Direction = Direction.Sent };
            base.Send(message);
        }

    }
}
