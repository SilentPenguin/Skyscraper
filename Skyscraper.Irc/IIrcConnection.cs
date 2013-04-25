using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Irc
{
    public interface IIrcConnection
    {
        void SendRawMessage(string rawMessage, params object[] formatArgs);
    }
}
