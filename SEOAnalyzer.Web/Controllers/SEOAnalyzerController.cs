using SEOAnalyzer.Core.Domain;
using SEOAnalyzer.Core.SEOHtmlAnalyzer;
using SEOAnalyzer.Web.Filters;
using SEOAnalyzer.Web.Models;
using SEOAnalyzer.Web.Services;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace SEOAnalyzer.Web.Controllers
{

    public class SEOAnalyzerController : Controller
    {
        private readonly ISEOService _iseoService;

        public SEOAnalyzerController()
        {
            _iseoService = new SEOService();
        }

        /// <summary>
        /// Get the SEO analyzer page
        /// </summary>
        /// <returns>return SEO analyzer partial view</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Analyze plain English text
        /// </summary>
        /// <param name="reqDto">Analyze options</param>
        /// <returns>Return analysis result <see cref="ISEOResult{TextSEOAnalysisInfo}"/></returns>
        [HttpPost]
        [ActionName("text")]
        [ValidateModel]
        public ActionResult AnalyzeText(AnalyseTextReqDto reqDto)
        {
            var result = _iseoService.AnalyzeText(reqDto.Text, reqDto.ExcludeStopWords, reqDto.ExcludeNumbers);
            return Json(result);
        }

        /// <summary>
        /// Analyze English Html page, by priding page Url link
        /// </summary>
        /// <param name="reqDto">Analyze options</param>
        /// <returns>Return analysis result <see cref="ISEOResult{HtmlSEOAnalysisInfo}"/></returns>
        [HttpPost]
        [ActionName("url")]
        [ValidateModel]
        public async Task<ActionResult> AnalyzeUrl(AnalyseUrlReqDto reqDto)
        {
            var result = await _iseoService.AnalyzeUrl(reqDto.Url, reqDto.ExcludeStopWords, reqDto.ExcludeMeta, reqDto.ExcludeBody, reqDto.ExcludeNumbers, reqDto.FindExternalLinks);
            return Json(result);
        }


        /// <summary>
        /// Handle Model validation Error
        /// </summary>
        /// <returns>Return <see cref="ISEOResult{SEOError}"/></returns>
        public ActionResult ModelError()
        {
            var formatError = SEOError.GetError(Core.Domain.Enums.SEOErrorType.InvalidData);
            return Json(new ISEOResult<SEOError>(formatError), JsonRequestBehavior.AllowGet);
        }
    }
}
