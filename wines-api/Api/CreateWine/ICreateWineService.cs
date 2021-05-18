using System.Collections.Generic;

namespace WinesApi.Api.CreateWine
{
    public interface ICreateWineService
    {
        bool CreateWine(CreateWineRequest request);

        IEnumerable<string> ValidateWineModel(CreateWineRequest wine);
    }
}