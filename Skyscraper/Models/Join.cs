using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface IJoin : IUserEvent
    {
        
    }

    public class Join : UserEvent
    {
        public Join(IUser User) : base(User) { }
    }
}
