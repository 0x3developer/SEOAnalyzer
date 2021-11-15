using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOAnalyzer.Core.Domain.Exceptions
{
    public class UnhandledSEOErrorTypeException : Exception
    {
        public UnhandledSEOErrorTypeException() : base("SEO error type not supported") 
        {
        }
    }
}
