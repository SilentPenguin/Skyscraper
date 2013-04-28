using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Irc
{
    public interface IMessage : IRawMessage
    {
        string CommandWord { get; }
        string[] Arguments { get; }
    }
}
