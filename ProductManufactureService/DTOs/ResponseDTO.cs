using System.Net;

namespace ProductManufactureService.DTOs
{
    public class ResponseDTO<T>
    { 
        public HttpStatusCode Status {  get; set; }
        public List<T> Results { get; set; } = new List<T>();
        
    }
}
