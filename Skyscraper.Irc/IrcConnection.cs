using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Skyscraper.Tcp;
using Skyscraper.Tcp.Events;

namespace Skyscraper.Irc
{
    class IrcConnection : TcpEventClient, IIrcConnection
    {
        public IrcConnection(IIrcConnectionSettings settings)
            : base(IrcConnection.addressFamily)
        {
            base.Encoding = settings.Encoding;
            base.Address = settings.Host;

            base.Connected += connection_Connected;
            base.Disconnected += connection_Disconnected;
            base.Sent += connection_Sent;
            base.Recieved += connection_Recieved;
        }

        public ~IrcConnection()
        {
            if (base.IsConnected)
            {
                base.Disconnect();
                this.connection_Disconnected(this, new EventArgs());
            }

            base.Connected -= connection_Connected;
            base.Disconnected -= connection_Disconnected;
            base.Sent -= connection_Sent;
            base.Recieved -= connection_Recieved;
        }

        private const AddressFamily addressFamily = AddressFamily.InterNetwork;


        private IIrcConnectionSettings settings { get; set; }
        
        public void Connect()
        {
            base.Connect();
        }

        void connection_Connected(object sender, EventArgs e)
        {
            IIrcUserSettings user = this.settings.User;
            this.SendNickname(user.Nickname);
            this.SendUsername(user.Username, user.Realname);

            if (!string.IsNullOrEmpty(user.Password))
            {
                this.SendPassword(user.Password);
            }
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
            IRawMessage message = new RawMessage
            {
                Text = rawMessage,
                Direction = RawMessageDirection.Sent
            };
            base.Send(message);
        }

        #region IrcProtocol
        public void SendNickname(string nickname)
        {
            this.SendRawMessage("NICK {0}", nickname);
        }

        public void SendUsername(string username, string realname)
        {
            this.SendRawMessage("USER {0} hostname servername :{1}", username, realname);
        }

        public void SendPassword(string password)
        {
            this.SendRawMessage("PASS {0}", password);
        }

        public void SendMessage(string message, params string[] targets)
        {
            string targetstring = string.Join(",", targets);
            this.SendRawMessage("PRIVMSG {0} :{1}", message, targetstring);
        }

        public void JoinChannel(string channel)
        {
            this.SendRawMessage("JOIN {0}", channel);
        }

        public void PartChannel(string channel, string reason)
        {
            this.SendRawMessage("PART {0} :{1}", channel, reason);
        }

        #endregion
    }
}
