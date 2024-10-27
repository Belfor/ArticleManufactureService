using ArticleManufacturerService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ArticleManufacturerService.DTOs;
using System.Net;
using ArticleManufacturerService.Application.Exceptions;

namespace ArticleManufacturerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        
        private readonly ILogger<ManufacturerController> _logger;
        private readonly IManufacturerService _manufactureService;

        public ManufacturerController(ILogger<ManufacturerController> logger, IManufacturerService manufacturerService)
        {
            _logger = logger;
            _manufactureService = manufacturerService;
        }

        [HttpGet("{searchQuery}")]
        public async Task<ResponseResponse<ManufacturerResponse>> Get(string searchQuery)
        {
            try
            {
                var manufacturers = await _manufactureService.GetManufacturerInfo(searchQuery);
         
                var manufacturersResponse = manufacturers
                .SelectMany(item => item.Articles.SelectMany(article =>
                    item.Addresses.Select(address => new ManufacturerResponse
                    {
                        ArticleNumber = article.ArticleNumber,
                        ManufacturerName = address.Name,
                        ManufacturerAddress = $"{address.Street} {address.City} {address.Zip}",
                        ManufacturerEmail = address.Email,
                        ManufacturerId = article.ManufacturerId
                    })));

              
                return new ResponseResponse<ManufacturerResponse>
                {
                    Status = HttpStatusCode.OK,
                    Results = manufacturersResponse
                };
            }
            catch(ManufacturerException ex)
            {
                _logger.LogError(ex.StackTrace);
                return new ResponseResponse<ManufacturerResponse>
                {
                    Status = ex.StatusCode,
                    StatusText = ex.Message
                };
            }          
            catch (Exception ex) 
            {
                _logger.LogError(ex.StackTrace);
                return new ResponseResponse<ManufacturerResponse>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusText = "An Error has ocurred"
                };
            }
        }
    }
}
