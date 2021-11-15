using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEOAnalyzer.Web.Models
{
    public class AnalyseUrlReqDto
    {
        [Url]
        public string Url { get; set; }
        public bool ExcludeMeta { get; set; }
        public bool ExcludeStopWords { get; set; }
        public bool ExcludeBody { get; set; }
        public bool ExcludeNumbers { get; set; }
        public bool FindExternalLinks { get; set; }
    }
}