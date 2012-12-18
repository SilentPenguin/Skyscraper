using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyscraper.Utilities
{
    public static class IListExtensionMethods
    {
        public static bool WithinBounds<T>(this IList<T> list, Int32 index)
        {
            return 0 <= index && index < list.Count();
        }
    }
}