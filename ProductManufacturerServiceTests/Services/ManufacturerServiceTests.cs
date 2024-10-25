
using ArticleManufacturerService.Application.Mappers;
using ArticleManufacturerService.Application.Services;
using ArticleManufacturerService.Infrastructure.HttpClients.TecDoc;
using ArticleManufacturerService.Infrastructure.HttpClients.TecDoc.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;


namespace ProductManufacturerService.Services.Tests
{
    [TestClass]
    public class ManufacturerServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ManufacturerService>> _loggerMock;

        public ManufacturerServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Agrega tus perfiles aquí
                cfg.AddProfile<ManufacturerProfile>();
              
            });

            _mapper = config.CreateMapper();

            // Configuración del Logger Mockeado
            _loggerMock = new Mock<ILogger<ManufacturerService>>();
        }

        [TestMethod]
        public void GetManufacturerInfoTestOk()
        {
            var tecDocApiClientMock = new Mock<ITecDocApiClient>();
            var json =  File.ReadAllText(@"Data/getArticles.json");
            var getArticles =  JsonConvert.DeserializeObject<ApiArticleResponse>(json); 
 
            json =  File.ReadAllText(@"Data/getAmBrandAddress.json");
            var getAmBrandAddress = JsonConvert.DeserializeObject<ApiAddressResponse>(json);

            tecDocApiClientMock.Setup(m => m.GetArticles(It.IsAny<string>())).ReturnsAsync(getArticles.Articles);

            tecDocApiClientMock.Setup(m => m.GetAmBrandAddress(It.IsAny<string>())).ReturnsAsync(getAmBrandAddress.Data.Array);

            var manufacturerService = new ManufacturerService(tecDocApiClientMock.Object, _mapper, _loggerMock.Object);
            var result = manufacturerService.GetManufacturerInfo("100").Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.SelectMany(a => a.Articles).Count() == 21);
            Assert.IsTrue(result.SelectMany(a => a.Addresses).Count() == 3);
        }
    }
}

