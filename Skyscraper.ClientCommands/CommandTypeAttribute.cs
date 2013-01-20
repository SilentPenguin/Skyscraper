using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ClientCommands
{
    [AttributeUsage(AttributeTargets.Class)]
    class CommandTypeAttribute : System.Attribute
    {
        public String Value { get; set; }
        public CommandTypeAttribute(String value)
        {
            this.Value = value.ToUpper();
        }
    }
}
