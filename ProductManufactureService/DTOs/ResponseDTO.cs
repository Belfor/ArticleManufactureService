using System.Net;

namespace ArticleManufacturerService.DTOs
{
    public class ResponseDTO<T>
    { 
        public HttpStatusCode Status {  get; set; }
        public List<T>? Results { get; set; } = null;
        public string? StatusText { get; set; } = null;

    }
}
