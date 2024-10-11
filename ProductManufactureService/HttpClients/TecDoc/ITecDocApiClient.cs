
namespace ProductManufacturerService.HttpClients.TecDoc
{
    public interface ITecDocApiClient
    {
        Task<string> GetAmBrandAddress(string brandNo);
        Task<string> GetArticles(string searchQuery);
    }
}