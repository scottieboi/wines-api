using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WinesApi.Api.CreateWine
{
    [ApiController]
    public class CreateWineController : ControllerBase
    {
        private readonly ICreateWineService _createWineService;

        public CreateWineController(ICreateWineService createWineService)
        {
            _createWineService = createWineService;
        }

        [HttpPost]
        [Route("wine")]
        public IActionResult Post([FromBody] CreateWineRequest wine)
        {
            var validationMessages = _createWineService.ValidateWineModel(wine).ToArray();
            if (validationMessages.Any())
            {
                return BadRequest(new CreateWine400Response
                {
                    Errors = validationMessages
                });
            }

            var created = _createWineService.CreateWine(wine);
            return created ? Ok() : StatusCode(500);
        }
    }
}