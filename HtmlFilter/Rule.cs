using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlFilter
{
    public class Rule
    {
        public Rule(string element)
        {
            Element = element;

            // these should be dictionary items
            // [key] = optional|required|wildcard(startsWith|EndsWith|Contains)
            // - required
            // - wildcards

            // wildcard => new RegExp( '^' + propertyName.replace( /\*/g, '.*' ) + '$'
            ClassNames = new RuleSet(RuleType.ClassName);
            Attributes = new RuleSet(RuleType.Attribute);
            Styles = new RuleSet(RuleType.Style);
            
            // RuleSet
            //   .All
            //   .Required
            //   .Wildcards = ^(RegEx|RegEx)$


            // Element Names
            // Match => Func
            // Features
            // PropertiesOnly
        }

        public string Element { get; private set; }
        public RuleSet Styles { get; private set; }
        public RuleSet Attributes { get; private set; }
        public RuleSet ClassNames { get; private set; }
    }
}
