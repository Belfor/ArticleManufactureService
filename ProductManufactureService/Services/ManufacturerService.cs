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
            
            var articles = await GetArticles(searchQuery);
            var mfrids = articles.Select(s => s.ManufacturerId).Distinct();
            var result = await Task.WhenAll(mfrids.Select(mfrid => GetArticleManfucaturer(articles, mfrid)));
            if (result == null) throw new Exception("Error obtaining the manufacturer of the article");
            
            var articlesManufacter  = result.SelectMany(s => s).ToList();
            return articlesManufacter;
        }

        private async Task<List<ArticleManufacter>> GetArticleManfucaturer(IEnumerable<Article> articles, int mfrid)
        {
            var manufacturer = await GetBrandAddress(mfrid);
            var articleManufacter = articles.Where(article => article.ManufacturerId == mfrid).Select(article => new ArticleManufacter
            {
                Article = article,
                Manufacturer = manufacturer

            }).ToList();
            return articleManufacter;
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
