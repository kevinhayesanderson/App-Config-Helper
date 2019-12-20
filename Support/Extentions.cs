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
            foreach (var item in enumeration) action(item);
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumeration, Func<T, Task> func)
        {
            foreach (var item in enumeration) await func(item);
        }

        public static void ForEachXml<TXmlNode>(this XmlNodeList nodeList, Action<TXmlNode> action)
        {
            foreach (TXmlNode node in nodeList) action(node);
        }
    }
}