using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Utilities
{
    public static class TypeExtentionMethods
    {
        public static bool WithinBounds<T>(this IList<T> list, Int32 index)
        {
            return 0 <= index && index < list.Count();
        }
    }
}
