using Newtonsoft.Json;
using SEOAnalyzer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEOAnalyzer.Web.Models
{

    public class ISEOResult<T> where T : class
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SEOError Error { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        public bool Success { get { return Error == null; } }

        public ISEOResult(T data)
        {
            Data = data;
        }

        public ISEOResult(SEOError error)
        {
            Error = error;
        }

        
    }
}