using Newtonsoft.Json.Linq;

namespace ArticleManufacturerService.Infrastructure.HttpClients.TecDoc.DTOs
{
    public class GetAmBrandAddressRequest
    {
        public string ArticleCountry { get; set; } = "GB";
        public string Lang { get; set; } = "en";
        public string BrandNo { get; set; } // Replace this value with the "mfrId" returned by each article
        public int Provider { get; set; } = 2080;
    }

}
