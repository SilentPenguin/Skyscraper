using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Skyscraper.Models;

namespace Skyscraper.Irc
{
    class IrcConnection : IIrcConnection
    {
        public IrcConnection(IIrcConnectionSettings settings)
        {
            this.Encoding = Encoding.UTF8;
            this.connection = new TcpEventClient(IrcConnection.addressFamily);
        }

        private const int bufferLength = 1024;
        private const AddressFamily addressFamily = AddressFamily.InterNetwork;

        private Encoding Encoding { get; set; }

        private TcpEventClient connection { get; set; }

        private IIrcConnectionSettings settings { get; set; }

        public void Connect()
        {
            if (!this.connection.Connected)
            {
                this.connection.Connect(this.settings.Host.Host, this.settings.Host.Port);
            }
        }

        private void ConnectionCompleted(IAsyncResult result)
        {
            
        }

        public void SendRawMessage(string rawMessage, params object[] formatArgs)
        {
            rawMessage = String.Format(rawMessage, formatArgs);
            byte[] encoding = this.Encoding.GetBytes(rawMessage);
            this.connection.WriteAsync(encoding, 0, encoding.Length);
        }

        private void RawMessageReceived(IAsyncResult result)
        {

        }

        public event EventHandler<RawMessageEventArgs> RawMessageSent;
        protected internal virtual void OnRawMessageSent(RawMessageEventArgs e)
        {
            if (this.RawMessageSent != null)
            {
                this.RawMessageSent(this, e);
            }
        }
    }
}
