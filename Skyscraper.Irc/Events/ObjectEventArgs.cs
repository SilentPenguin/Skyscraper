using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Irc.Events
{
    public class ObjectEventArgs <T> : EventArgs
    {
        public T Object { get; set; }
        public ObjectEventArgs(T Object){
            this.Object = Object;
        }
    }
}
