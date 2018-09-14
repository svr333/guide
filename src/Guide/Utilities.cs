using System;
using System.Collections.Generic;
using System.Linq;

namespace Guide
{
    public static class Utilities
    {
        public static Random random = new Random();

        public static T GetRandomElement<T>(this IEnumerable<T> collection)
        {
            return collection.ElementAt(random.Next(0, collection.Count()));
        }
    }
}
