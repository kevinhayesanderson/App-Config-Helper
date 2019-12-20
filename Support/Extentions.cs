using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Support
{
    public static class Extentions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumeration, Func<T, Task> func)
        {
            foreach (T item in enumeration)
            {
                await func(item);
            }
        }

        public static void ForEachXML<XmlNode>(this XmlNodeList nodeList, Action<XmlNode> action)
        {
            foreach (XmlNode node in nodeList)
            {
                action(node);
            }
        }
    }
}
