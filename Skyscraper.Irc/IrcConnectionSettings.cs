using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    class IrcConnectionSettings : IIrcConnectionSettings
    {
        private Uri host;
        public Uri Host
        {
            get
            {
                return this.host;
            }
            set
            {
                this.host = value;
            }
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
    }
}
