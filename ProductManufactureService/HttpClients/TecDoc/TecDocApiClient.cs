using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductManufacturerService.HttpClients.DTOs;
using System.Text;

namespace ProductManufacturerService.HttpClients.TecDoc
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
        public async Task<string> GetArticles(string searchQuery)
        {
            var getArticles = new GetArticlesDTO
            {
                SearchQuery = searchQuery,
            };
            var request = JsonConvert.SerializeObject(new { getArticles }, _settings);
           

            var content = new StringContent(request, Encoding.UTF8);

            var response = await _httpClient.PostAsync(_url, content);
            response.EnsureSuccessStatusCode();

            var contentResponse = await response.Content.ReadAsStringAsync();
            return contentResponse;
        }

        public async Task<string> GetAmBrandAddress(string brandNo)
        {
            var getAmBrandAddress = new GetAmBrandAddressDTO
            {
                BrandNo = brandNo,
            };

            var request = JsonConvert.SerializeObject(new { getAmBrandAddress }, _settings);

            var content = new StringContent(request, Encoding.UTF8);
            var response = await _httpClient.PostAsync(_url, content);
            response.EnsureSuccessStatusCode();

            var contentResponse = await response.Content.ReadAsStringAsync();
            return contentResponse;
        }
    }
}
