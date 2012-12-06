using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface IPart : IUserEvent
    {

    }

    class Part : UserEvent, IPart
    {
        public Part(IUser User) : base(User) { }
    }
}
