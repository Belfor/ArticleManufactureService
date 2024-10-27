using System.Net;

namespace ArticleManufacturerService.DTOs
{
    public class ResponseResponse<T>
    { 
        public HttpStatusCode Status {  get; set; }
        public IEnumerable<T>? Results { get; set; } = null;
        public string? StatusText { get; set; } = null;

    }
}
