using SEOAnalyzer.Core.SEOHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.SEOHtmlAnalyzer
{
    public class SEOTextAnalyzer : ISEOTextAnalyzer
    {
        private readonly string _text;
        private Dictionary<string, int> _wordOccurrence;

        /// <summary>
        /// Initialize new instance of <see cref="SEOTextAnalyzer"/>
        /// </summary>
        /// <param name="stopWords">List of stop words to count its occurrences </param>
        /// <param name="text">Source of text to count the stop words occurrences  in</param>
        public SEOTextAnalyzer(string[] excludeWords, string text, bool excludeDgits)
        {
            var textCleaner = new SEOTextCleaner(text, excludeWords, excludeDgits);
            _text = textCleaner.Clean();
        }

        /// <summary>
        /// Get List of matched stop words
        /// </summary>
        /// <returns>List of each stop-word occurrence count</returns>
        public IEnumerable<WordOccurrence> GetWordOccurrences() 
        {
            if (_wordOccurrence == null)
            {
                string[] words = _text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var wordGroups = words.GroupBy(w => w);

                _wordOccurrence = wordGroups.ToDictionary(g => g.Key, g => g.Count());
            }

            return _wordOccurrence.Select(w => new WordOccurrence(w.Key, w.Value));
        }

        /// <summary>
        /// Get List of matched stop words
        /// </summary>
        /// <returns>List of each stop-word occurrence count</returns>
        public TextSEOAnalysisInfo Analyze()
        {
            return new TextSEOAnalysisInfo(GetWordOccurrences().ToArray());
        }
    }
}
