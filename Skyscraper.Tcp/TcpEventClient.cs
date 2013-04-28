using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Skyscraper.Tcp.Events;

namespace Skyscraper.Tcp
{
    public class TcpEventClient : TcpClient, ITcpEventClient
    {
        public TcpEventClient()
        {
            this.Encoding = Encoding.UTF8;
        }

        public TcpEventClient(Encoding encoding)
            : base()
        {
            this.Encoding = encoding;
        }

        public TcpEventClient(AddressFamily addressFamily)
            : base(addressFamily)
        {
            this.Encoding = Encoding.UTF8;
        }

        public TcpEventClient(AddressFamily addressFamily, Encoding encoding)
            : base(addressFamily)
        {
            this.Encoding = encoding;
        }

        private Encoding encoding;
        public Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
            set
            {
                this.encoding = value;
            }
        }

        private Uri address;
        public Uri Address {
            get{ return address; }
            set
            {
                if (!base.Connected)
                {
                    this.address = value;
                }
            }
        }

        public bool IsConnected
        {
            get { return base.Connected; }
            set
            {
                if (value != this.IsConnected)
                {
                    if (value)
                    {
                        this.Connect();
                    }
                    else
                    {
                        this.Disconnect();
                    }
                }
            }
        }

        public void Connect()
        {
            if (Address != null && !base.Connected)
            {
                base.BeginConnect(this.Address.Host, this.Address.Port, this.HasConnected, null);
            }
        }

        private void HasConnected(IAsyncResult asyncResult)
        {
            try
            {
                base.EndConnect(asyncResult);
            }
            catch
            {
                return;
            }

            NetworkStream stream = base.GetStream();
            byte[] buffer = new byte[base.ReceiveBufferSize];
            if (stream.CanRead)
            {
                stream.BeginRead(buffer, 0, buffer.Length, RecievedMessage, buffer);
            }
        }

        public void Disconnect()
        {
            if (base.Connected)
            {
                NetworkStream stream = base.GetStream();
                base.Close();
                stream.Close();
                if (!base.Connected)
                {
                    OnDisconnected(new EventArgs());
                }
            }
        }

        public void Send(IRawMessage message)
        {
            NetworkStream stream = base.GetStream();
            byte[] encoding = this.Encoding.GetBytes(message.Text);
            if (stream.CanWrite)
            {
                stream.BeginWrite(encoding, 0, encoding.Length, SentMessage, encoding);
            }
            else
            {
                this.Disconnect();
            }
        }

        private void SentMessage(IAsyncResult asyncResult)
        {
            NetworkStream stream = base.GetStream();
            if (asyncResult.IsCompleted)
            {
                IRawMessage message = (IRawMessage)asyncResult.AsyncState;
                stream.EndWrite(asyncResult);
                this.OnSent(new RawMessageEventArgs { Message = message, Result = asyncResult });
            }
        }

        private void RecievedMessage(IAsyncResult asyncResult)
        {
            int readState;
            NetworkStream networkStream = base.GetStream();
            if (networkStream.CanRead)
            {
                readState = networkStream.EndRead(asyncResult);

                if (readState == 0)
                {
                    OnDisconnected(new EventArgs());
                }

                byte[] buffer = asyncResult.AsyncState as byte[];
                IRawMessage message = new RawMessage { Text = this.Encoding.GetString(buffer), Direction = RawMessageDirection.Received };
                OnRecieved(new RawMessageEventArgs { Message = message, Result = asyncResult });
                networkStream.BeginRead(buffer, 0, buffer.Length, RecievedMessage, buffer);
            }
        }

        public event EventHandler<RawMessageEventArgs> Sent;
        protected internal virtual void OnSent(RawMessageEventArgs e)
        {
            if (this.Sent != null)
            {
                this.Sent(this, e);
            }
        }

        public event EventHandler<RawMessageEventArgs> Recieved;
        protected internal virtual void OnRecieved(RawMessageEventArgs e)
        {
            if (this.Recieved != null)
            {
                this.Recieved(this, e);
            }
        }

        public event EventHandler<EventArgs> Connected;
        protected internal virtual void OnConnected(EventArgs e)
        {
            if (this.Connected != null)
            {
                this.Connected(this, e);
            }
        }

        public event EventHandler<EventArgs> Disconnected;
        protected internal virtual void OnDisconnected(EventArgs e)
        {
            if (this.Disconnected != null)
            {
                this.Disconnected(this, e);
            }
        }
    }
}
