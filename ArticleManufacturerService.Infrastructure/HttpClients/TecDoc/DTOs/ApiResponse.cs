

namespace ArticleManufacturerService.Infrastructure.HttpClients.TecDoc.DTOs
{
    public class ApiArticleResponse
    {
        public int? TotalMatchingArticles { get; set; }
        public int? MaxAllowedPage { get; set; }
        public IEnumerable<ArticleResponse> Articles { get; set; }
        public int Status { get; set; }
    }

}
