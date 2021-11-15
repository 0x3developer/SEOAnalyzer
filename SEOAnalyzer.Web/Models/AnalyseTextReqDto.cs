using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEOAnalyzer.Web.Models
{
    public class AnalyseTextReqDto
    {
        [Required]
        public string Text { get; set; }
        public bool ExcludeStopWords { get; set; }
        public bool ExcludeNumbers { get; set; }
    }
}