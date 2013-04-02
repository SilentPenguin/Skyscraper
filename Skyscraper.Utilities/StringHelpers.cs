using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Utilities
{
    public static class StringHelpers
    {
        public static bool HasUpperCase(this String str){
            return !string.IsNullOrEmpty(str) && str.Any(c => char.IsUpper(c));
        }
    }
}
