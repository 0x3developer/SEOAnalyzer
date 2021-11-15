using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.SEOHtmlAnalyzer
{
    public class SEOAnalisisResult
    {
        public WordOccurrence[] BodyWordOccurrences { get; set; }
        public WordOccurrence[] MetaTags { get; set; }

        public WordOccurrence[] AllTags()
        {
            var allTags = new WordOccurrence[MetaTags.Length + BodyWordOccurrences.Length];

            MetaTags.CopyTo(allTags, 0);
            BodyWordOccurrences.CopyTo(allTags, MetaTags.Length);

            return allTags;
        }

    }
}
