using System.Collections.Generic;

namespace WinesApi.Api.FindWines
{
    public interface IFindWinesService
    {
        IEnumerable<FindAllWinesResponse> Find();

        FindWineByIdResponse FindById(int id);
    }
}