using System.Collections.Generic;

namespace WinesApi.Api.Region
{
    public interface IRegionService
    {
        IEnumerable<RegionResponse> FindRegions(string searchTerm);
    }
}