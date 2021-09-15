using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Tests
{
    public static class Settings
    {
        public static IConfiguration GetSettings()
        {
            var settings = new Dictionary<string, string>();
            settings.Add("ApplicationSettings:Hostname", "https://localhost:5001");
            var iconfig = new ConfigurationBuilder().AddInMemoryCollection(settings).Build();
            return iconfig;
        }
    }
}
