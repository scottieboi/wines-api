using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WinesApi.Api.FindWines
{
    [ApiController]
    public class FindWinesController : ControllerBase
    {
        private IFindWinesService _findWinesService;

        public FindWinesController(IFindWinesService findWinesService)
        {
            _findWinesService = findWinesService;
        }

        [HttpGet]
        [Route("wines")]
        public IEnumerable<FindWinesResponse> Get()
        {
            return _findWinesService.Find();
        }

        [HttpGet]
        [Route("wine")]
        public FindWineResponse GetById([FromQuery] int id)
        {
            return _findWinesService.FindById(id);
        }
    }
}