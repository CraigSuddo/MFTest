using System.Collections.Generic;

namespace UrlShortener.Logic.Storage
{
    /// <summary>
    /// This interface exists to provide the potential to create a real world database which might use Entity Framework.
    /// It would implement the methods below and should be fairly easy to follow.
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// The URLs which have been processed and saved.
        /// </summary>
        public List<ProcessedUrl> ProcessedUrls { get; set; }

        /// <summary>
        /// Save method which stores the URL to the database.
        /// </summary>
        /// <param name="url">The completed URL.</param>
        public void Save(ProcessedUrl url);

        /// <summary>
        /// Retrieve a previously encoded URL.
        /// </summary>
        /// <param name="url">The URL the user is asking to get by.</param>
        /// <returns></returns>
        public ProcessedUrl GetByUrl(string url);

        /// <summary>
        /// Get a URL from it's code.
        /// </summary>
        /// <param name="code">The code that the user is asking to get by.</param>
        /// <returns></returns>
        public ProcessedUrl GetByCode(string code);
    }
}
