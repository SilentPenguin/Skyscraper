using System;

namespace Skyscraper.Data
{
    public interface IUser
    {
        String Nickname { get; set; }
        String Hostname { get; set; }
        String Modes { get; set; }
        bool IsAway { get; set; }
    }
}