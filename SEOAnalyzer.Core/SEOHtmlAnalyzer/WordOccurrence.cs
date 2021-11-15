using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.SEOHtmlAnalyzer
{
    public class WordOccurrence
    {
        public string Word { get; }
        public int Count { get; set; }

        public WordOccurrence(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
