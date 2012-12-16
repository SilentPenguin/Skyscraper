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

        public static bool Between<T>(this T number, T min, T max) where T : IComparable
        {
            return max.IsLessThan(min) ? number.Between(max, min) : min.IsEqualOrLessThan(number) && max.IsEqualOrGreaterThan(number);
        }

        private static bool IsGreaterThan<T>(this T value, T other) where T : IComparable
        {
            return value.CompareTo(other) > 0;
        }

        private static bool IsLessThan<T>(this T value, T other) where T : IComparable
        {
            return value.CompareTo(other) < 0;
        }
        private static bool IsEqualOrGreaterThan<T>(this T value, T other) where T : IComparable
        {
            return value.Equals(other) || value.IsGreaterThan(other);
        }

        private static bool IsEqualOrLessThan<T>(this T value, T other) where T : IComparable
        {
            return value.Equals(other) || value.IsLessThan(other);
        }
    }
}
