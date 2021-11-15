using SEOAnalyzer.Core.Domain.Enums;
using SEOAnalyzer.Core.Domain.Exceptions;

namespace SEOAnalyzer.Core.Domain
{
    public class SEOError
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public SEOError(SEOErrorType errorType, string message) 
        {
            Code = errorType.ToString();
            Message = message;
        }

        public override string ToString()
        {
            return $"Error While Processing SEO Request \r\n Code: {Code}, \r\n {Message}";
        }


        public static SEOError GetError(SEOErrorType error)
        {
            switch (error)
            {
                case SEOErrorType.InvalidData:
                    return new SEOError(error, "Given data is invalid");
                case SEOErrorType.InvalidInputFormat:
                    return new SEOError(error, "Given data is invalid format");
                case SEOErrorType.InvaidUrlFormat:
                    return new SEOError(error, "Given URL is invalid format");
                case SEOErrorType.InvalidHtmlFormat:
                    return new SEOError(error, "Given Html has invalid format");
                case SEOErrorType.FailToLoadHtmlPage:
                    return new SEOError(error, "Fail to load Html page, please check url");
                case SEOErrorType.InternalException:
                    return new SEOError(error, "Internal Server Error");
                default:
                    throw new UnhandledSEOErrorTypeException();
            }
        }
    }
}
