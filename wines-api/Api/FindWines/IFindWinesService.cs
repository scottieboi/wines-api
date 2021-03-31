using System.Collections.Generic;

namespace WinesApi.Api.FindWines
{
    public interface IFindWinesService
    {
        IEnumerable<FindWinesResponse> Find();
    }
}