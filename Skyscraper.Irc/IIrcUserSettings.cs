using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IIrcUserSettings
    {
        string Nickname { get; set; }
        string Realname { get; }
        string Username { get; }
        string Password { get; }
    }
}
