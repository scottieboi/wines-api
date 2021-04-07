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
                };

            return (from wl in _dataContext.Winelists
                    join q in locationsQuery on wl.Id equals q.Id
                    select new FindWinesResponse
                    {
                        Id = wl.Id,
                        WineName = wl.Winename,
                        WineType = wl.Winetype.Winetype1,
                        Vineyard = wl.Vineyard.Vineyard1,
                        Vintage = wl.Vintage,
                        Qty = q.Qty ?? 0,
                    }).ToList();
        }

        public FindWineResponse FindById(int id)
        {
            var result =
                (from wl in _dataContext.Winelists
                 join l in _dataContext.Locations on wl.Id equals l.Wineid
                 join b in _dataContext.Boxes on l.Box equals b.Boxno
                 where l.Cellarversion == 1 && wl.Id == id
                 select new
                 {
                     Id = wl.Id,
                     WineName = wl.Winename,
                     WineType = wl.Winetype.Winetype1,
                     Vineyard = wl.Vineyard.Vineyard1,
                     Vintage = wl.Vintage,
                     Qty = l.No,
                     Box = b.Boxno,
                 }).ToList();

            return result.GroupBy(x => x.Id).Select(wineGrouping =>
            {
                var wine = wineGrouping.First();

                return new FindWineResponse
                {
                    Id = wineGrouping.Key,
                    WineName = wine.WineName,
                    WineType = wine.WineType,
                    Vineyard = wine.Vineyard,
                    Vintage = wine.Vintage,
                    Boxes = wineGrouping
                        .GroupBy(y => y.Box)
                        .Select(boxGrouping => new Box
                        {
                            BoxNo = boxGrouping.Key,
                            Qty = boxGrouping.Sum(box => box.Qty ?? 0)
                        })
                };
            }).Single();
        }
    }
}