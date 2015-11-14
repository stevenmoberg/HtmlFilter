using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlFilter
{
    internal static class HtmlAgilityPackExtensions
    {
        public static bool AddDistinct(this HashSet<string> set, string value)
        {
            if (set.Contains(value, StringComparer.OrdinalIgnoreCase))
                return false;

            set.Add(value);
            return true;
        }



        /// <summary>
        /// Walk node if is match
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        public static void Walk(this HtmlNode node, Predicate<HtmlNode> action)
        {
            if (node != null && action != null && action(node) && node.HasChildNodes)
                Walk(node.ChildNodes, action);
        }

        /// <summary>
        /// Walk nodes if they match
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="action"></param>
        public static void Walk(this HtmlNodeCollection nodes, Predicate<HtmlNode> action)
        {
            // transverse in reverse so that they can be safely removed
            if (nodes != null && action != null)
                foreach (HtmlNode node in nodes.Reverse().ToArray())
                    if (action(node) && node.HasChildNodes)
                        Walk(node.ChildNodes, action);      
        }
    }
}
