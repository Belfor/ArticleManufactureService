
using Moq;
using ProductManufacturerService.HttpClients.TecDoc;

namespace ProductManufacturerService.Services.Tests
{
    [TestClass]
    public class ManufacturerServiceTests
    {

        [TestMethod]
        public void GetManufacturerInfoTest()
        {
            Mock<ITecDocApiClient> tecDocApiClientMock = new Mock<ITecDocApiClient>();
            StreamReader sr = new StreamReader(@"Data/getArticles.json");
        
            var getArticles = sr.ReadToEnd();
            sr = new StreamReader(@"Data/getAmBrandAddress.json");
            var getAmBrandAddress = sr.ReadToEnd();
            tecDocApiClientMock.Setup(m => m.GetArticles(It.IsAny<string>())).ReturnsAsync(getArticles);
            tecDocApiClientMock.Setup(m => m.GetAmBrandAddress(It.IsAny<string>())).ReturnsAsync(getAmBrandAddress);
            var manufacturerService = new ManufacturerService(tecDocApiClientMock.Object, null);
            var result = manufacturerService.GetManufacturerInfo("100").Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 21);
            Assert.IsTrue(result.First().Manufacturer.ManfucaturerEmail == "uk.info@mann-hummel.com");
        }
    }
}

