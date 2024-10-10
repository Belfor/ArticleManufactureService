namespace ProductManufacturerService.HttpClients.TecDoc
{
    public class TecDocApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly string _url;
        public TecDocApiClient(HttpClient httpClient, IConfiguration configuration, ILogger logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _url = _configuration["TecDocAPI"];
        }
        public async Task GetArticles()
        {
            var response = await _httpClient.PostAsync(_url, Content);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task GetAmBrandAddress()
        {
            var response = await _httpClient.PostAsync(_url, Content);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
