using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Models
{
    public interface IChannelUser : IUser
    {
        IUser User { get; }
        string Modes { get; set; }
    }
}
