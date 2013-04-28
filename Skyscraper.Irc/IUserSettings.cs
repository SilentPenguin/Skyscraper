using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Irc
{
    public interface IUserSettings
    {
        string Nickname { get; set; }
        string Realname { get; }
        string Username { get; }
        string Password { get; }
    }
}
