using Microsoft.AspNetCore.Mvc;
using ProductManufactureService.DTOs;

namespace ProductManufactureService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        
        private readonly ILogger<ManufacturerController> _logger;

        public ManufacturerController(ILogger<ManufacturerController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "manufacture")]
        public IEnumerable<ResponseDTO<ManufacturerDTO>> Get()
        {
            
        }
    }
}
