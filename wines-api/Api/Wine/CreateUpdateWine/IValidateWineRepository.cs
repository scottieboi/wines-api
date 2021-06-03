using System.Collections.Generic;

namespace WinesApi.Api.Wine.CreateUpdateWine
{
    public interface IValidateWineRepository
    {
        IEnumerable<string> ValidateWineModel(CreateWineRequest wine);

        IEnumerable<string> ValidateWineModel(UpdateWineRequest wine);
    }
}