namespace ProductManufactureService.DTOs
{
    public class ResponseDTO<T>
    { 
        public int Status {  get; set; }
        public List<T> Results { get; set; } = new List<T>();
        
    }
}
