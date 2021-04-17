using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WinesApi.Api.Region
{
    [Route("[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet]
        public IEnumerable<RegionResponse> Get([FromQuery] string searchTerm)
        {
            return _regionService.FindRegions(searchTerm);
        }
    }
}