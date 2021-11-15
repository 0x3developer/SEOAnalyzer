using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SEOAnalyzer.Web
{
    public sealed class AppConfig
    {
        private static string[] _stopWords;
        private static object _lock = new object();
        public static string[] StopWords { get { return _stopWords; } }

        public static AppConfig Config { get; }

        public static void Initialize()
        {
            if(_stopWords == null) //extra checking to prevent memory consuming by lock
                lock (_lock)
                {
                    if (_stopWords == null)
                        _stopWords = WebConfigurationManager.AppSettings["StopWords"]?.Split(",".ToCharArray());
                }
        }
            
    }
}