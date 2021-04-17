using System.Collections.Generic;
using System.Linq;
using WinesApi.Models;

namespace WinesApi.Api.WineType
{
    public class WineTypeService : IWineTypeService
    {
        private readonly DataContext _dataContext;

        public WineTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<WineTypeResponse> FindTypes(string searchTerm)
        {
            // If no search, then return all results
            if (string.IsNullOrEmpty(searchTerm))
            {
                return (from wt in _dataContext.Winetypes
                        select new WineTypeResponse
                        {
                            WineType = wt.Winetype1,
                            Id = wt.Id
                        }).ToList();
            }

            return (from wt in _dataContext.Winetypes
                    where wt.Winetype1.ToLower().Contains(searchTerm.ToLower())
                    select new WineTypeResponse
                    {
                        WineType = wt.Winetype1,
                        Id = wt.Id
                    }).ToList();
        }
    }
}