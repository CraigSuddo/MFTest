using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Logic.Storage;

namespace UrlShortener.Logic
{
    /// <summary>
    /// This is a made up storage solution which is simply used in a Singleton pattern using DI to ensure I have some kind of Storage.
    /// I am aiming to follow a Repository pattern with this so that I can perform bits of logic which are reusable. 
    /// I would normally implement something like this around an Entity Framework DbContext.
    /// </summary>
    public class Store : IStore
    {
        public List<ProcessedUrl> ProcessedUrls { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Store()
        {
            ProcessedUrls = new List<ProcessedUrl>();
        }

        public ProcessedUrl GetByCode(string code)
        {
            return ProcessedUrls.FirstOrDefault(pu => pu.Code == code);
        }

        public ProcessedUrl GetByUrl(string url)
        {
            return ProcessedUrls.FirstOrDefault(pu => pu.Url == url);
        }

        public void Save(ProcessedUrl url)
        {
            ProcessedUrls.Add(url);
        }
    }
}
