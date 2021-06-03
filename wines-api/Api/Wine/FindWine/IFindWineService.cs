using System.Collections.Generic;

namespace WinesApi.Api.Wine.FindWine
{
    public interface IFindWineService
    {
        IEnumerable<FindAllWinesResponse> Find();

        FindWineByIdResponse FindById(int id);
    }
}