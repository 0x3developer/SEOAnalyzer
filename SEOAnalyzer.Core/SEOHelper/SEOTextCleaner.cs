using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.SEOHelper
{
    public class SEOTextCleaner
    {
        // symbols that may has no meaning for SEO, can be replaced with UNICODE range
        public static readonly string[] NOISY_WORDS = 
            { "-", "_", "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")",
            "_", "_", "+", "=", "{", "}", "[", "]", ":", ";", "<", ">", ",", ".", "?","/","\"","|","\\"
        };

        private readonly string _original;
        private readonly string[] _stopWors;
        private readonly bool _excludeDigits;

        public SEOTextCleaner(string original, string[] stopWords = null, bool excludeDigits = true)
        {
            _original = original;
            _stopWors = stopWords;
            _excludeDigits = excludeDigits;
        }

        /// <summary>
        /// Do clean operation based on the constructor arguments
        /// </summary>
        /// <returns>Return cleaned string ready for statics SEO analysis</returns>
        public string Clean()
        {
            var cleanedText = _original.ToLower();
            StringBuilder stringBuilder;

            // remove stop-words use \b to remove full word
            if (_stopWors != null)
                cleanedText = Regex.Replace(cleanedText, "\\b" + string.Join("\\b|\\b", _stopWors) + "\\b", " ");

            // delete all digits 0 to 9
            if (_excludeDigits)
                cleanedText = Regex.Replace(cleanedText, "[0-9]", " ", RegexOptions.None);

            stringBuilder = new StringBuilder(cleanedText);

            //TODO: try use UNICODE to exclude those symbols rather than using list
            foreach (var item in NOISY_WORDS)
                stringBuilder.Replace(item, " ");

            // replace multi spaces may caused by previous operations
            return Regex.Replace(stringBuilder.ToString(), @"\s+", " ").Trim();
        }
    }
}
