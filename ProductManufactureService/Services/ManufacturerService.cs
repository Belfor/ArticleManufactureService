using Newtonsoft.Json.Linq;
using ProductManufacturerService.HttpClients.TecDoc;
using ProductManufacturerService.Models;



namespace ProductManufacturerService.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly ITecDocApiClient TecDocApiClient;
        private readonly ILogger<ManufacturerService> _logger;
        public ManufacturerService(ITecDocApiClient tecDocApiClient, ILogger<ManufacturerService> logger)
        {
            TecDocApiClient = tecDocApiClient;
            _logger = logger;
        }
        public async Task<IEnumerable<ArticleManufacter>> GetManufacturerInfo(string searchQuery)
        {
            var articlesManufacter = new List<ArticleManufacter>();
            var articles = await GetArticles(searchQuery);
            var mfrids = articles.Select(s => s.ManufacturerId).Distinct();

            foreach (var mfrid in mfrids)
            {
                var manufacturer = await GetBrandAddress(mfrid);
                var articleManufacter = articles.Where(article => article.ManufacturerId == mfrid).Select(article => new ArticleManufacter
                {
                    Article = article,
                    Manufacturer = manufacturer

                }).ToList();



                articlesManufacter.AddRange(articleManufacter);

            }
            return articlesManufacter;
        }

        private async Task<Manufacturer> GetBrandAddress(int mfrid)
        {
            var brandAddress = new Manufacturer();
            var responseBrandAddress = await TecDocApiClient.GetAmBrandAddress(mfrid.ToString());

            var BrandAddressObject = JObject.Parse(responseBrandAddress);
            if (!string.IsNullOrEmpty(BrandAddressObject["data"]?.ToString()))
            {
                var brandAddressItems = from item in BrandAddressObject["data"]["array"]
                                   select new Manufacturer
                                   { 
                                       ManufacturerId = mfrid,
                                       ManufacturerName = item["name"]?.ToString(), 
                                       ManfucaturerEmail = item["email"]?.ToString(),
                                       ManufacturerAddress = $"{item["street"]} {item["city"]} {item["zip"]}"
                                   };

                brandAddress = brandAddressItems.FirstOrDefault();
            }

            return brandAddress;
        }

        private async Task<IEnumerable<Article>> GetArticles(string searchQuery)
        {
            var responseArticles = await TecDocApiClient.GetArticles(searchQuery);
            var ArticlesObject = JObject.Parse(responseArticles);

            var articles = from item in ArticlesObject["articles"]
                                       select new Article 
                                       { 
                                           ManufacturerId = (int)item["mfrId"], 
                                           ArticleNumber = item["articleNumber"].ToString() 
                                       };
            return articles;
        }
    }
}
