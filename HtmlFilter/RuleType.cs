using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlFilter
{
    public class RuleType
    {
        public static readonly RuleType Style = new RuleType(@"{([^}]+)}");
        public static readonly RuleType Attribute = new RuleType(@"\[([^\]]+)\]");
        public static readonly RuleType ClassName = new RuleType(@"\(([^\)]+)\)");


        private RuleType(string rx)
        {
            Pattern = new Regex(rx);
        }

        public Regex Pattern { get; private set; }
    }
}
