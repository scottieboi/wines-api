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
        public IEnumerable<FindAllWinesResponse> Get()
        {
            return _findWinesService.Find();
        }

        [HttpGet]
        [Route("wine")]
        public FindWineByIdResponse GetById([FromQuery] int id)
        {
            return _findWinesService.FindById(id);
        }
    }
}