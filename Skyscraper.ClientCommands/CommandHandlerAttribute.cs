using System;
using System.Collections.Generic;

namespace Skyscraper.ClientCommands
{
    [AttributeUsage(AttributeTargets.Class)]
    class CommandHandlerAttribute : Attribute
    {
        public IEnumerable<string> CommandWords { get; set; }

        public CommandHandlerAttribute(params string[] commandWords)
        {
            this.CommandWords = commandWords;
        }
    }
}