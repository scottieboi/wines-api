using System.Collections.Generic;
using System.Linq;
using WinesApi.Models;

namespace WinesApi.Api.Vineyard
{
    public class VineyardService : IVineyardService
    {
        private readonly DataContext _dataContext;

        public VineyardService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<VineyardResponse> FindVineyards(string searchTerm)
        {
            // If no search, then return all results
            if (string.IsNullOrEmpty(searchTerm))
            {
                return (from wt in _dataContext.Vineyards
                        select new VineyardResponse
                        {
                            Vineyard = wt.Vineyard1,
                            Id = wt.Id
                        }).ToList();
            }

            return (from wt in _dataContext.Vineyards
                    where wt.Vineyard1.ToLower().Contains(searchTerm.ToLower())
                    select new VineyardResponse
                    {
                        Vineyard = wt.Vineyard1,
                        Id = wt.Id
                    }).ToList();
        }
    }
}