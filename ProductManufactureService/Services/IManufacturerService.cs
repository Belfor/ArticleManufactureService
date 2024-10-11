
using ProductManufacturerService.Models;


namespace ProductManufacturerService.Services
{
    public interface IManufacturerService
    {
        Task<IEnumerable<ArticleManufacter>> GetManufacturerInfo(string searchQuery);
    }
}