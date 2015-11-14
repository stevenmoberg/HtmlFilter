using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlFilter
{
    public class Filter
    {
        /// <summary>
        /// Add Rule to Whitelist
        /// </summary>
        /// <param name="ruleSource"></param>
        public void Allow(string ruleSource)
        {
            ParseRules(ruleSource, whitelist, whitelistSource);
        }

        /// <summary>
        /// Add Rule to BlackList
        /// </summary>
        /// <param name="ruleSource"></param>
        public void Disallow(string ruleSource)
        {
            ParseRules(ruleSource, blacklist, blacklistSource);
        }

        public bool Check(string tag)
        {
            return false;
        }

        #region statics
        // elements[attributes]{styles}(classes)
        // Regexp pattern:
        //                                         <   elements   ><                       styles, attributes and classes                        >< separator >
        private static Regex rxRule = new Regex(@"^([a-z0-9\-*\s]+)((?:\s*\{[!\w\-,\s\*]+\}\s*|\s*\[[!\w\-,\s\*]+\]\s*|\s*\([!\w\-,\s\*]+\)\s*){0,3})(?:;\s*|$)", RegexOptions.IgnoreCase);

        #endregion

        private List<string> whitelistSource = new List<string>();
        private List<string> blacklistSource = new List<string>();
        private Dictionary<string, Rule> whitelist = new Dictionary<string, Rule>();
        private Dictionary<string, Rule> blacklist = new Dictionary<string, Rule>();
               
        private void ParseRules(string ruleSource, IDictionary<string, Rule> rules, IList<string> source)
        {
            Rule rule;
            string keys, match;
            string[] tags, classNames, attributes, styles;
            // var delim = /\s*,\s*/,

            // b em strong[property,class,style]

            foreach (Match m in rxRule.Matches(ruleSource))
            {
                match = m.Value.Trim().TrimEnd(';') + ';';
                // maintain source for output to JS
                source.Add(match);

                tags = Split(m.Groups[1].Value, ' ');
                keys = (m.Groups.Count < 3) ? string.Empty : m.Groups[2].Value;
                classNames = ParseKeys(keys, RuleType.ClassName.Pattern);
                attributes = ParseKeys(keys, RuleType.Attribute.Pattern);
                styles = ParseKeys(keys, RuleType.Style.Pattern);

                foreach (var tag in tags)
                {
                    var tagNoCase = tag.ToLowerInvariant();
                    if (!rules.TryGetValue(tagNoCase, out rule))
                        rules.Add(tagNoCase, rule = new Rule(tagNoCase));

                    AppendSet(rule.ClassNames, classNames);
                    AppendSet(rule.Attributes, attributes);
                    AppendSet(rule.Styles, styles);
                }
            }
        }

        private static string[] Split(string keys, char delim)
        {
            return keys.Split(' ').Select(x => x.Trim()).Where(x => x.Length > 0).ToArray();
        }
        
        private static string[] ParseKeys(string keys, Regex pattern)
        {
            var m = pattern.Match(keys);
            return (m.Success)
                ? Split(m.Groups[1].Value, ',')
                : new string[0];
        }

        private static void AppendSet(RuleSet ruleSet, string[] keys)
        {
            if (keys == null || keys.Length == 0)
                return;

            string key;
            for (var i=0; i < keys.Length; i++)
            {
                key = keys[i];

                if (key.StartsWith("!", StringComparison.OrdinalIgnoreCase))
                {
                    key = key.Substring(1);
                    ruleSet.Required.AddDistinct(key);
                    ruleSet.Optional.Remove(key);
                }
                else if (key.Contains('*'))
                {
                    // convert to wildcard  '^(?:border*|*-style)$'
                    ruleSet.Wildcard = string.IsNullOrEmpty(ruleSet.Wildcard)
                        ? key
                        : string.Concat(ruleSet.Wildcard, '|', key);
                }
                else
                {
                    ruleSet.Optional.AddDistinct(key);
                }
            }
        }
        
        /// <summary>
        /// Debugger insight
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (whitelistSource.Count > 0)
                sb.AppendFormat("Allow:\n{0}", string.Join("\n", whitelistSource));

            if (whitelistSource.Count > 0 && blacklistSource.Count > 0)
                sb.Append("\n");

            if (blacklistSource.Count > 0)
                sb.AppendFormat("Disallow:\n{0}", string.Join("\n", blacklistSource));

            return sb.ToString();
        }

    }
}
