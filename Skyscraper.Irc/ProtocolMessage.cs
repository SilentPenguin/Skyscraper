using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Irc
{
    class ProtocolMessage : RawMessage, IMessage
    {
        public ProtocolMessage(IRawMessage rawMessage)
        {
            this.Text = rawMessage.Text;
            this.Direction = rawMessage.Direction;
        }

        public string Text {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.split = null;
            }
        }
        
        private string[] split;
        private string[] Split {
            get{
                this.split = this.split ?? this.Text.Split(' ');
                return this.split ?? this.Text.Split(' ');
            }
        }

        public string CommandWord
        {
            get {
                return this.Split[0];
            }
        }

        public string[] Arguments
        {
            get
            {
                return this.Split.Skip(1)
                                 .ToArray();
            }
        }
    }
}
