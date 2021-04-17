using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WinesApi.Api.WineType
{
    [Route("[controller]")]
    [ApiController]
    public class WineTypeController : ControllerBase
    {
        private readonly IWineTypeService _wineTypeService;

        public WineTypeController(IWineTypeService wineTypeService)
        {
            _wineTypeService = wineTypeService;
        }

        [HttpGet]
        public IEnumerable<WineTypeResponse> Get([FromQuery] string searchTerm)
        {
            return _wineTypeService.FindTypes(searchTerm);
        }
    }
}