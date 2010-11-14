using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoapWebservices.Http.Utilities
{
    public static class ICollectionExtensions
    {
        public static void ForEach<T>(this ICollection<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
