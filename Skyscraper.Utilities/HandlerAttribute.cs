using System;
using System.Collections.Generic;

namespace Skyscraper.Utilities
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HandlerAttribute : Attribute
    {
        private IEnumerable<string> words { get; set; }

        public HandlerAttribute(params string[] words)
        {
            this.words = words;
        }
    }
}