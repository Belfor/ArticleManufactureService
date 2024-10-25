using ArticleManufacturerService.Application.Exceptions;
using ArticleManufacturerService.Application.Interfaces;
using ArticleManufacturerService.Domain.Entities;
using ArticleManufacturerService.Infrastructure.HttpClients.TecDoc;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ArticleManufacturerService.Application.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly ITecDocApiClient TecDocApiClient;
        private readonly ILogger<ManufacturerService> _logger;
        private readonly IMapper _mapper;
        public ManufacturerService(ITecDocApiClient tecDocApiClient, IMapper mapper, ILogger<ManufacturerService> logger)
        {
            TecDocApiClient = tecDocApiClient;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<Manufacturer>> GetManufacturerInfo(string searchQuery)
        {
            try
            {
                _logger.LogDebug("Call GetManufacturerInfo");
                var articles = await GetArticles(searchQuery);

                if (articles == null || !articles.Any())
                {
                    throw new ManufacturerException(System.Net.HttpStatusCode.NotFound, "Articles not found");
                }

                var dataSupplierIds = articles.Select(s => s.DataSupplierId).Distinct();

                var manufacturersTasks = dataSupplierIds.Select(async id => new Manufacturer
                {
                    Addresses = await GetArticleManfucaturer(id),
                    Articles = articles.Where(article => article.DataSupplierId == id).ToList(),
                });


                var manufactures = await Task.WhenAll(manufacturersTasks);

                return manufactures.Select(s => s);
            }
            catch (ManufacturerException ex)
            { 
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw new ManufacturerException(System.Net.HttpStatusCode.InternalServerError, "An Error has ocurred");
            }
        }

        private async Task<List<Address>> GetArticleManfucaturer(int id)
        {
            _logger.LogDebug($"Call GetArticleManfucaturer with id: {id}");
            var addressResponse = await TecDocApiClient.GetAmBrandAddress(id.ToString());
            return _mapper.Map<List<Address>>(addressResponse);
        }

        private async Task<List<Article>> GetArticles(string searchQuery)
        {
            _logger.LogDebug($"Call GetArticles with searchQuery: {searchQuery}");

            var responseArticles = await TecDocApiClient.GetArticles(searchQuery);
            return  _mapper.Map<List<Article>>(responseArticles);         
            
        }
    }
}
