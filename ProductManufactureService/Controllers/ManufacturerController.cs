using ArticleManufacturerService.Application.Interfaces;
using AutoMapper;
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
        private readonly IManufacturerService ManufactureService;
        private readonly IMapper _mapper;

        public ManufacturerController(ILogger<ManufacturerController> logger, IMapper mapper, IManufacturerService manufacturerService)
        {
            _mapper = mapper;
            _logger = logger;
            ManufactureService = manufacturerService;
        }

        [HttpGet("{searchQuery}")]
        public async Task<ResponseDTO<ManufacturerDTO>> Get(string searchQuery)
        {
            try
            {
                var manufacturer = await ManufactureService.GetManufacturerInfo(searchQuery);
                var manufacturerDTO = new List<ManufacturerDTO>();
                foreach (var item in manufacturer)
                {
                    foreach (var article in item.Articles)
                    {
                        foreach (var address in item.Addresses)
                        {
                            manufacturerDTO.Add(new ManufacturerDTO
                            {
                                ArticleNumber = article.ArticleNumber,
                                ManufacturerName = address.Name,
                                ManufacturerAddress = $"{address.Street} {address.City} {address.Zip}",
                                ManufacturerEmail = address.Email,
                                ManufacturerId = article.ManufacturerId
                            });
                        }
                    }
                }
                return new ResponseDTO<ManufacturerDTO>
                {
                    Status = HttpStatusCode.OK,
                    Results = manufacturerDTO
                };
            }
            catch(ManufacturerException ex)
            {
                _logger.LogError(ex.StackTrace);
                return new ResponseDTO<ManufacturerDTO>
                {
                    Status = ex.StatusCode,
                    StatusText = ex.Message
                };
            }          
            catch (Exception ex) 
            {
                _logger.LogError(ex.StackTrace);
                return new ResponseDTO<ManufacturerDTO>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusText = "An Error has ocurred"
                };
            }
        }
    }
}
