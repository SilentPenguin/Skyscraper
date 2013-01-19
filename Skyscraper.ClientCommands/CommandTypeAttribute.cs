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
        public CommandType Value { get; set; }
        public CommandTypeAttribute(CommandType value)
        {
            this.Value = value;
        }
    }
}
