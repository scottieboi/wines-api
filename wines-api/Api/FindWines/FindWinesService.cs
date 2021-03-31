using System.Collections.Generic;
using System.Linq;
using WinesApi.Models;

namespace WinesApi.Api.FindWines
{
    public class FindWinesService : IFindWinesService
    {
        private DataContext _dataContext;

        public FindWinesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<FindWinesResponse> Find()
        {
            var locationsQuery =
                from wl in _dataContext.Winelists
                join l in _dataContext.Locations on wl.Id equals l.Wineid
                where l.Cellarversion == 1
                group l by wl.Id into g
                select new
                {
                    Id = g.Key,
                    Qty = g.Sum(l => l.No),
                    // Box = g.Select(l => l.Box)
                };

            return (from wl in _dataContext.Winelists
                    join q in locationsQuery on wl.Id equals q.Id
                    select new FindWinesResponse
                    {
                        WineName = wl.Winename,
                        WineType = wl.Winetype.Winetype1,
                        Vineyard = wl.Vineyard.Vineyard1,
                        Vintage = wl.Vintage,
                        Qty = q.Qty ?? 0,
                        // BoxNos = q.Boxes
                    }).ToList();
        }
    }
}