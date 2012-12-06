using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Models
{
    public interface IUser
    {
        String Nickname { get; set; }
        String Hostname { get; set; }
        String Server { get; }
        UserStatus Status { get; set; }
    }

    class User : IUser
    {
        public String Nickname { get; set; }
        public String Hostname { get; set; }
        public String Server { get; set; }
        public UserStatus Status { get; set; }
    }

    public enum UserStatus
    {
        Active,
        Away,
    }
}
