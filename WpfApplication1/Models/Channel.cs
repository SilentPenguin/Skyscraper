using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.Models
{
    public interface IChannel : IEnumerable<ILogEntry>
    {
        String Name { get; }
        String Modes { get; set; }
        String Topic { get; set; }
        IList<IUser> Users { get; set; }
    }

    class Channel : List<ILogEntry>, IChannel
    {
        public String Name { get; set; }
        public String Modes { get; set; }
        public String Topic { get; set; }
        public IList<IUser> Users { get; set; }
    }
}
