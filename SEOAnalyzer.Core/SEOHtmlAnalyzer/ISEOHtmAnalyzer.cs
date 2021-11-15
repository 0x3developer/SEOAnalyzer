using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.SEOHtmlAnalyzer
{
    public interface ISEOHtmAnalyzer
    {
        HtmlSEOAnalysisInfo Analyze();
    }
}
