using System.Collections.Generic;

namespace WinesApi.Api.Wine.CreateUpdateWine
{
    public interface ICreateUpdateWineService
    {
        bool CreateWine(CreateWineRequest request, out IEnumerable<string> errors);

        bool UpdateWine(UpdateWineRequest request, out IEnumerable<string> errors);
    }
}