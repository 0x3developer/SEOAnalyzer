using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SEOAnalyzer.Core.SEOHtmlAnalyzer
{
    public class SEOHtmAnalyzer : ISEOHtmAnalyzer
    {
        private readonly HtmlDocument _htmlDocument;
        private readonly string _documentHost;
        private readonly string[] _stopWords;
        private readonly bool _excludeDigits;
        private readonly bool _excludeBody;
        private readonly bool _excludeMeta;
        private readonly bool _findExternalLinks;

        public SEOHtmAnalyzer(HtmlDocument htmlDocument, string documentHost, string[] stopWords, bool excludeBody, bool excludeMeta, bool excludeDigits, bool findExternalLinks)
        {
            _htmlDocument = htmlDocument;
            _documentHost = documentHost;
            _stopWords = stopWords;
            _excludeBody = excludeBody;
            _excludeMeta = excludeMeta;
            _excludeDigits = excludeDigits;
            _findExternalLinks = findExternalLinks;

        }

        public HtmlSEOAnalysisInfo Analyze()
        {
            WordOccurrence[] bodyWordOccurances;
            WordOccurrence[] metaWordOccurances;
            List<string> externalLinks = new List<string>();

            if(_excludeBody)
            {
                bodyWordOccurances = new WordOccurrence[] { };
            }
            else
            {
                var bodyContent = _excludeBody ? "" : _htmlDocument.DocumentNode.SelectSingleNode("//body").InnerText;
                var bodyTextAnalyzer = new SEOTextAnalyzer(_stopWords, bodyContent, _excludeDigits);
                bodyWordOccurances = bodyTextAnalyzer.GetWordOccurrences().ToArray();
            }

            if (_excludeMeta)
            {
                metaWordOccurances = new WordOccurrence[] { };
            }
            else
            {
                var nodes =  _htmlDocument.DocumentNode.SelectNodes("//meta/@content");
                var contents = nodes.Select(n => n.Attributes["content"].Value);
                var metaContent = string.Join(" ", contents);

                var metaTextAnalyzer = new SEOTextAnalyzer(_stopWords, metaContent, _excludeDigits);
                metaWordOccurances = metaTextAnalyzer.GetWordOccurrences().ToArray();
            }

            if (_findExternalLinks)
            {
                var aNodes = _htmlDocument.DocumentNode.SelectNodes("//a[@href]");
                if (aNodes != null)
                {
                    foreach (var n in aNodes)
                    {
                        var href = n.Attributes["href"].Value;
                        href = href.Trim("//".ToCharArray());

                        if (Uri.TryCreate(href, UriKind.Absolute, out Uri uri) && _documentHost != uri.Host && !externalLinks.Contains(href))
                            externalLinks.Add(href);
                    }
                }
            }


            return new HtmlSEOAnalysisInfo(bodyWordOccurances,metaWordOccurances, externalLinks.ToArray());
        }
    }
}
