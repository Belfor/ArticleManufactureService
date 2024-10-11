namespace ProductManufacturerService.Models
{
    public class Article
    {
        public string ArticleNumber { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
