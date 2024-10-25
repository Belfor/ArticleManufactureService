using ArticleManufacturerService.Domain.Entities;

namespace ArticleManufacturerService.Application.Interfaces
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetManufacturerInfo(string searchQuery);
    }
}