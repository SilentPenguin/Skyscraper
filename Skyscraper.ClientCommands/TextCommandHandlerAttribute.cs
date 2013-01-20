using System;
using System.Collections.Generic;

namespace Skyscraper.ClientCommands
{
    [AttributeUsage(AttributeTargets.Class)]
    class TextCommandHandlerAttribute : Attribute
    {
        public IEnumerable<string> CommandWords { get; set; }

        public TextCommandHandlerAttribute(params string[] commandWords)
        {
            this.CommandWords = commandWords;
        }
    }
}