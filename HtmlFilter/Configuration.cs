using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlFilter
{
    /// <summary>
    /// Filter Settings
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Whitelist
        /// </summary>
        public string AllowedContent { get; set; }

        /// <summary>
        /// Blacklist
        /// </summary>
        public string DisallowContent { get; set; }
    }
}
