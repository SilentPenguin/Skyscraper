using Skyscraper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public class ChannelUser : NotifyPropertyChangedBase, IChannelUser
    {
        IUser user;
        public IUser User
        {
            get { return this.user; }
            set { this.SetProperty(ref this.user, value); }
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
