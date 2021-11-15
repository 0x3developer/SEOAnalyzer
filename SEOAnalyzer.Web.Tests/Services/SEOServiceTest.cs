using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEOAnalyzer.Web.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOAnalyzer.Web.Tests.Services
{
    [TestClass]
    public class SEOServiceTest
    {
        const string textContent = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of  (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.The standard and chunk of Lorem Ipsum used since the 1500s is any reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H.Rackham";
        const string url = "https://www.msn.com/";

        [TestMethod]
        public void AnalyzeTextTest()
        {
            
            ISEOService service = new SEOService();
            var res = service.AnalyzeText(textContent, excludeStopWords:true, excludeDigits:  true);
            Assert.IsTrue(res.Success);
        }

        [TestMethod]
        public void AnalyzeTextExcludeNumbersText()
        {

            ISEOService service = new SEOService();
            var res = service.AnalyzeText(textContent, excludeStopWords: false, excludeDigits: true);
            Assert.IsTrue(res.Success && res.Data.WordOccurrences.All(x => !Regex.IsMatch(x.Word, @"\d+")));
        }

        [TestMethod]
        public void AnalyzeTextExcludeStopWords()
        {

            ISEOService service = new SEOService();
            var res = service.AnalyzeText(textContent, excludeStopWords: true, excludeDigits: false);
            Assert.IsTrue(res.Success && !res.Data.WordOccurrences.Any(x => Regex.IsMatch(x.Word, @"\\bthe\\b")));
        }

        [TestMethod]
        public void AnalyzeTextExcludeStopWordsAndExcludeDigits()
        {

            ISEOService service = new SEOService();
            var res = service.AnalyzeText(textContent, excludeStopWords: true, excludeDigits: true);
            Assert.IsTrue(res.Success && !res.Data.WordOccurrences.Any(x => Regex.IsMatch(x.Word, @"\\bthe\\b") || Regex.IsMatch(x.Word, @"\d+")));
        }

        [TestMethod]
        public async Task AnalyzeRemoteHtmlPage()
        {

            ISEOService service = new SEOService();
            var res = await service.AnalyzeUrl(url, true, true, true, true, false);
            Assert.IsTrue(res.Success);
        }

        [TestMethod]
        public async Task AnalyzeRemoteHtmlPageExcludeBody()
        {

            ISEOService service = new SEOService();
            var res = await service.AnalyzeUrl(url, false, false, true, false, false);
            Assert.IsTrue(res.Success && res.Data.BodyWordOccurrences.Length == 0);
        }
        [TestMethod]
        public async Task AnalyzeRemoteHtmlPageExcludeMeta()
        {

            ISEOService service = new SEOService();
            var res = await service.AnalyzeUrl(url, false, true, false, false, false);
            Assert.IsTrue(res.Success && res.Data.MetaWordOccurrences.Length == 0);
        }
        [TestMethod]
        public async Task AnalyzeRemoteDontSearchLinks()
        {

            ISEOService service = new SEOService();
            var res = await service.AnalyzeUrl(url, false, false, true, false, false);
            Assert.IsTrue(res.Success && res.Data.ExternalLinks.Length == 0);
        }

    }
}
