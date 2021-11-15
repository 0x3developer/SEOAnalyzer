using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.Domain.Enums
{
    public enum SEOErrorType
    {
        InvalidData,
        InvalidInputFormat,
        InvaidUrlFormat,
        InvalidHtmlFormat,
        FailToLoadHtmlPage,
        InternalException
    }
}
