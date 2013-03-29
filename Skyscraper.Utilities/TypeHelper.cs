using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Skyscraper.Utilities
{
    public class TypeHelper
    {
        public static IEnumerable<Type> ClassesForInterface<T>()
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(T).IsAssignableFrom(p) && p.IsClass)
                .ToList();
        }

        public static IEnumerable<Type> ClassesForInterfaceInAssembly<T>(Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            return  assembly
                .GetTypes()
                .Where(p => typeof(T).IsAssignableFrom(p) && p.IsClass)
                .ToList();
        }
    }
}