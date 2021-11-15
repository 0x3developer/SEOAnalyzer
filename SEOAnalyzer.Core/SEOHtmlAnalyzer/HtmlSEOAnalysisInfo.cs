using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.SEOHtmlAnalyzer
{
    public class HtmlSEOAnalysisInfo
    {
        public WordOccurrence[] MetaWordOccurrences { get; }
        public WordOccurrence[] BodyWordOccurrences { get; }
        public string[] ExternalLinks { get; }
        public HtmlSEOAnalysisInfo(WordOccurrence[] bodayWordOccurrences, WordOccurrence[] metaWordOccurrences, string[] externalLinks)
        {
            BodyWordOccurrences = bodayWordOccurrences;
            MetaWordOccurrences = metaWordOccurrences;
            ExternalLinks = externalLinks;
        }
    }
}
