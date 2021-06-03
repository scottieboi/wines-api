using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WinesApi.Api.Wine.CreateUpdateWine;
using WinesApi.Api.Wine.FindWine;

namespace WinesApi.Api.Wine
{
    public class WineController : ControllerBase
    {
        private readonly IFindWineService _findWineService;
        private readonly ICreateUpdateWineService _createUpdateWineService;

        public WineController(IFindWineService findWineService, ICreateUpdateWineService createUpdateWineService)
        {
            _findWineService = findWineService;
            _createUpdateWineService = createUpdateWineService;
        }

        [HttpGet]
        [Route("wines")]
        public IEnumerable<FindAllWinesResponse> Get()
        {
            return _findWineService.Find();
        }

        [HttpGet]
        [Route("wine")]
        public FindWineByIdResponse GetById([FromQuery] int id)
        {
            return _findWineService.FindById(id);
        }

        [HttpPost]
        [Route("wine")]
        public IActionResult Post([FromBody] CreateWineRequest wine)
        {
            var created = _createUpdateWineService.CreateWine(wine, out var errors);
            var errorsList = errors.ToList();
            if (errorsList.Any())
            {
                return BadRequest(new
                {
                    Errors = errorsList
                });
            }

            return created ? (IActionResult) Ok() : Conflict(new {Error = "Cannot create wine"});
        }

        [HttpPut]
        [Route("wine")]
        public IActionResult Put([FromBody] UpdateWineRequest wine)
        {
            var updated = _createUpdateWineService.UpdateWine(wine, out var errors);
            var errorsList = errors.ToList();
            if (errorsList.Any())
            {
                return BadRequest(new
                {
                    Errors = errorsList
                });
            }

            return updated ? (IActionResult) Ok() : Conflict(new {Error = "Cannot create wine"});
        }
    }
}