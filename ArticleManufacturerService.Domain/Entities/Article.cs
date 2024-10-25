namespace ArticleManufacturerService.Domain.Entities;

    public class Article
    {
        public string ArticleNumber { get; set; }
        public int DataSupplierId { get; set; }
        public int ManufacturerId { get; set; }
    }

