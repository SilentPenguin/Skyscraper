using System;

namespace Skyscraper.Irc
{
    public class ObjectEventArgs <T> : EventArgs
    {
        public T Object { get; set; }

        public ObjectEventArgs(T Object)
        {
            this.Object = Object;
        }
    }
}