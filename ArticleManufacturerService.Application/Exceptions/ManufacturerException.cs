using System.Net;


namespace ArticleManufacturerService.Application.Exceptions
{
    public class ManufacturerException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ManufacturerException(HttpStatusCode statusCode, string message) : base(message)
        { 
            StatusCode = statusCode;
        }
    }
}
