

using Newtonsoft.Json;
using SEOAnalyzer.Core.Domain;
using SEOAnalyzer.Web.Models;
using System;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace SEOAnalyzer.Web.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        const string MODEL_ERROR_ACTION = "~/SEOAnalyzer/ModelError";
        public override void OnActionExecuting (ActionExecutingContext filterContext)
        {

            if (!filterContext.Controller.ViewData.ModelState.IsValid)
                filterContext.Result = new RedirectResult(MODEL_ERROR_ACTION);
        }
    }
}