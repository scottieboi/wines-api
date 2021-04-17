using System.Collections.Generic;

namespace WinesApi.Api.Vineyard
{
    public interface IVineyardService
    {
        IEnumerable<VineyardResponse> FindVineyards(string searchTerm);
    }
}