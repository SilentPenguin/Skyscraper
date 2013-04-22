using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public class ChannelUser : IChannelUser
    {
        public IUser User
        {
            get { throw new NotImplementedException(); }
        }

        public string Modes
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
