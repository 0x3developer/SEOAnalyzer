using HtmlAgilityPack;
using SEOAnalyzer.Core.Domain;
using SEOAnalyzer.Core.Domain.Enums;
using SEOAnalyzer.Core.SEOHtmlAnalyzer;
using SEOAnalyzer.Web.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace SEOAnalyzer.Web.Services
{
    public class SEOService : ISEOService
    {

        public ISEOResult<TextSEOAnalysisInfo> AnalyzeText(string text, bool excludeStopWords, bool excludeDgits = true)
        {

            if (string.IsNullOrEmpty(text))
                return new ISEOResult<TextSEOAnalysisInfo>(SEOError.GetError(SEOErrorType.InvalidInputFormat));

            var stopWords = excludeStopWords ? AppConfig.StopWords : null;
            ISEOTextAnalyzer analyzer = new SEOTextAnalyzer(stopWords, text, excludeDgits);
            return  new ISEOResult<TextSEOAnalysisInfo>(analyzer.Analyze());
        }

        public async Task<ISEOResult<HtmlSEOAnalysisInfo>> AnalyzeUrl(string url, bool excludeStopWords, bool excludeMeta, bool excludeBody, bool excludeDgits, bool findExternalLinks)
        {

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri _uri))
                return new ISEOResult<HtmlSEOAnalysisInfo>(SEOError.GetError(SEOErrorType.InvaidUrlFormat));

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var htmlDoc = new HtmlDocument();

                // using Html web causing some issue with SSL connections
                //HtmlWeb web = new HtmlWeb();
                //htmlDoc = await web.LoadFromWebAsync(url);

                using (WebClient client = new WebClient())
                    htmlDoc.LoadHtml(await client.DownloadStringTaskAsync(_uri));

                var stopWords = excludeStopWords ? AppConfig.StopWords : null;
                var htmAnalyzer = new SEOHtmAnalyzer(htmlDoc, _uri.Host, stopWords, excludeBody, excludeMeta, excludeDgits, findExternalLinks);
                return new ISEOResult<HtmlSEOAnalysisInfo>(htmAnalyzer.Analyze());

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Fail to load html from url @{url}", url);
                return new ISEOResult<HtmlSEOAnalysisInfo>(SEOError.GetError(SEOErrorType.FailToLoadHtmlPage));
            }
        }
    }
}