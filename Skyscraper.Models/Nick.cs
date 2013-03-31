using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public class Nick : UserEvent
    {
        public Nick(IUser user, string oldUsername) : base(user) {
            base.NicknameContinuity = oldUsername;
        }
    }
}
