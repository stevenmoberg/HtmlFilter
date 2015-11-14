using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlFilter
{
    /// <summary>
    /// (Class, Property, Style) Match
    /// </summary>
    public class RuleSet
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ruleType"></param>
        public RuleSet(RuleType ruleType)
        {
            RuleType = ruleType;
            Optional = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Required = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Rule Type [Element, Attribute, Style]
        /// </summary>
        public RuleType RuleType { get; set; }

        /// <summary>
        /// Optional Keys
        /// </summary>
        public HashSet<string> Optional { get; set; }

        /// <summary>
        /// Required Keys
        /// </summary>
        public HashSet<string> Required { get; set; }

        /// <summary>
        /// WildCard '^(background.*|*.color)$'
        /// </summary>
        public string Wildcard { get; set; }
    }
}
