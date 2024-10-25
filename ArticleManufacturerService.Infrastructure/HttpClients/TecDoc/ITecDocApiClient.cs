


using ArticleManufacturerService.Infrastructure.HttpClients.TecDoc.DTOs;

namespace ArticleManufacturerService.Infrastructure.HttpClients.TecDoc
{
    public interface ITecDocApiClient
    {
        Task<IEnumerable<AddressResponse>> GetAmBrandAddress(string brandNo);
        Task<IEnumerable<ArticleResponse>> GetArticles(string searchQuery);
    }
}