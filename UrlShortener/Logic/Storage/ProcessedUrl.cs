using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Logic.Storage
{
    /// <summary>
    /// A URL where processing has been completed .
    /// </summary>
    public class ProcessedUrl
    {
        /// <summary>
        /// The full URL of the webpage.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The Code designated to the URL.
        /// </summary>
        public string Code { get; set; }
    }
}
