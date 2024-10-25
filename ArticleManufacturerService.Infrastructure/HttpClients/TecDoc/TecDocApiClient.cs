
using ArticleManufacturerService.Infrastructure.HttpClients.TecDoc;
using ArticleManufacturerService.Infrastructure.HttpClients.TecDoc.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace ArticleManufacturerService.Infrastructure.HttpClients.TecDoc
{
    public class TecDocApiClient : ITecDocApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ITecDocApiClient> _logger;
        private readonly string _url;
        private readonly JsonSerializerSettings _settings;
        public TecDocApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TecDocApiClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _logger = logger;
            _url = _configuration["TecDocAPI"];
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
        }
        public async Task<IEnumerable<ArticleResponse>> GetArticles(string searchQuery)
        {
            _logger.LogInformation($"Call to GetArticles, searchQuery:{searchQuery}");
   
            var getArticles = new GetArticlesRequest
            {
                SearchQuery = searchQuery,
            };
            var request = JsonConvert.SerializeObject(new { getArticles }, _settings);
           

            var content = new StringContent(request, Encoding.UTF8);
           
            var response = await _httpClient.PostAsync(_url, content);
            response.EnsureSuccessStatusCode();

            var contentResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiArticleResponse>(contentResponse);

            if (apiResponse.Status != (int)HttpStatusCode.OK)
            {
                _logger.LogError($"Error call to GetArticles, searchQuery:{searchQuery}");
                throw new Exception("Error to GetArticles");
            }

            return apiResponse.Articles;
        }

        public async Task<IEnumerable<AddressResponse>> GetAmBrandAddress(string brandNo)
        {
            _logger.LogInformation($"Call to GetAmBrandAddress, brandNo:{brandNo}");
            var getAmBrandAddress = new GetAmBrandAddressRequest
            {
                BrandNo = brandNo,
            };

            var request = JsonConvert.SerializeObject(new { getAmBrandAddress }, _settings);

            var content = new StringContent(request, Encoding.UTF8);
           
            var response = await _httpClient.PostAsync(_url, content);
            response.EnsureSuccessStatusCode();

            var contentResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiAddressResponse>(contentResponse);

            if (apiResponse.Status != (int)HttpStatusCode.OK)
            {
                _logger.LogError($"Error call to GetAmBrandAddress, brandNo:{brandNo}");
                throw new Exception("Error to GetAmBrandAddress");
            }

            return apiResponse.Data.Array;
        }
    }
}
