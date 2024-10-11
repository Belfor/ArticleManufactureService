namespace ProductManufacturerService.HttpClients.DTOs
{
    public class GetArticlesDTO
    {
        public string SearchQuery { get; set; }  // This is the search term you need to collect in your endpoint and send to getArticles
        public string ArticleCountry { get; set; } = "GB";
        public string Lang { get; set; } = "en";
        public string SearchMatchType { get; set; } = "prefix_or_suffix";
        public int SearchType { get; set; } = 0;
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 100;
        public bool IncludeAll { get; set; } = false;
        public bool IncludeLinkages { get; set; } = true;
        public int linkagesPerPage { get; set; } = 100;
        public int Provider { get; set; } = 2080;

    }
}
