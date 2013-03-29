using System;

namespace Skyscraper.Models
{
    public interface IUser
    {
        String Nickname { get; set; }
        String Hostname { get; set; }
        String Realname { get; set; }
        String Modes { get; set; }
        bool IsAway { get; set; }
    }
}