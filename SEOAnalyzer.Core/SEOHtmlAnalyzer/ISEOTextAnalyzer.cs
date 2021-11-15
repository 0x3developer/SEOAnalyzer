using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.SEOHtmlAnalyzer
{
    public interface ISEOTextAnalyzer
    {
        IEnumerable<WordOccurrence> GetWordOccurrences();
        TextSEOAnalysisInfo Analyze();
        
    }
}
