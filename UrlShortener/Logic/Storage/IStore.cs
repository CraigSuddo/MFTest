using System.Collections.Generic;

namespace UrlShortener.Logic.Storage
{
    /// <summary>
    /// This interface exists to provide the potential to create a real world database which might use Entity Framework.
    /// It would implement the methods below and should be fairly easy to follow.
    /// </summary>
    public interface IStore
    {
        public List<ProcessedUrl> ProcessedUrls { get; set; }
        public void Save(ProcessedUrl url);
        public ProcessedUrl GetByUrl(string url);
        public ProcessedUrl GetByCode(string code);
    }
}
