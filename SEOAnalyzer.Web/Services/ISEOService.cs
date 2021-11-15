using SEOAnalyzer.Core.SEOHtmlAnalyzer;
using SEOAnalyzer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SEOAnalyzer.Web.Services
{
    public interface ISEOService
    {
        /// <summary>
        /// Analyze plain English text
        /// </summary>
        /// <param name="text">Plain text</param>
        /// <param name="excludeStopWords">True to exclude common stop words, otherwise stop words will be counted</param>
        /// <param name="excludeDigits">True to exclude digits, otherwise digits will be counted</param>
        /// <returns>Return analysis result <see cref="ISEOResult{TextSEOAnalysisInfo}"/></returns>
        ISEOResult<TextSEOAnalysisInfo> AnalyzeText(string text, bool excludeStopWords, bool excludeDigits);

        /// <summary>
        /// Analyze html page
        /// </summary>
        /// <param name="url"></param>
        /// <param name="excludeStopWords">True to exclude common stop words, otherwise stop words will be counted</param>
        /// <param name="excludeMeta">True to exclude meta tags in Html page head from analysis process, otherwise meta tags will be included</param>
        /// <param name="excludeBody">True to exclude Html page body from analysis process, otherwise Html body will be included</param>
        /// <param name="excludeDigits">True to exclude digits, otherwise digits will be counted, otherwise digits will be included</param>
        /// <param name="findExternalLinks">True to find external links in Html body, analysis process will skip searching external links</param>
        /// <returns>Return <see cref="ISEOResult{HtmlSEOAnalysisInfo}"/></returns>
        Task<ISEOResult<HtmlSEOAnalysisInfo>> AnalyzeUrl(string url, bool excludeStopWords, bool excludeMeta, bool excludeBody, bool excludeDigits, bool findExternalLinks);
    }
}