using System.Collections.Generic;
using System.Linq;
using WinesApi.Models;

namespace WinesApi.Api.Region
{
    public class RegionService : IRegionService
    {
        private readonly DataContext _dataContext;

        public RegionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<RegionResponse> FindRegions(string searchTerm)
        {
            // If no search, then return all results
            if (string.IsNullOrEmpty(searchTerm))
            {
                return (from wt in _dataContext.Regions
                        select new RegionResponse
                        {
                            Region = wt.Region1,
                            Id = wt.Id
                        }).ToList();
            }

            return (from wt in _dataContext.Regions
                    where wt.Region1.ToLower().Contains(searchTerm.ToLower())
                    select new RegionResponse
                    {
                        Region = wt.Region1,
                        Id = wt.Id
                    }).ToList();
        }
    }
}