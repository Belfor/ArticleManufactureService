

namespace ArticleManufacturerService.Infrastructure.HttpClients.TecDoc.DTOs
{
    public class ArticleResponse
    {
        public int DataSupplierId { get; set; }
        public string ArticleNumber { get; set; }
        public int MfrId { get; set; }
        public string MfrName { get; set; }
        public int TotalLinkages { get; set; }
    }
}
