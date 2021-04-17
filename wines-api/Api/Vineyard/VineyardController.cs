using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WinesApi.Api.Vineyard
{
    [Route("[controller]")]
    [ApiController]
    public class VineyardController : ControllerBase
    {
        private readonly IVineyardService _vineyardService;

        public VineyardController(IVineyardService vineyardService)
        {
            _vineyardService = vineyardService;
        }

        [HttpGet]
        public IEnumerable<VineyardResponse> Get([FromQuery] string searchTerm)
        {
            return _vineyardService.FindVineyards(searchTerm);
        }
    }
}