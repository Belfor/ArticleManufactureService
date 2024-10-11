using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProductManufacturerService.Services;
using ProductManufactureService.DTOs;
using System.Net;

namespace ProductManufactureService.Controllers
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
                var content = await ManufactureService.GetManufacturerInfo(searchQuery);
                return new ResponseDTO<ManufacturerDTO>
                {
                    Status = HttpStatusCode.OK,
                    Results = _mapper.Map<List<ManufacturerDTO>>(content)
                };
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.StackTrace);
                return new ResponseDTO<ManufacturerDTO>
                {               
                    Status = ex.StatusCode ?? HttpStatusCode.InternalServerError
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return new ResponseDTO<ManufacturerDTO>
                {
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
