using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WinesApi.Models;

namespace WinesApi.Api.Wine.CreateUpdateWine
{
    public class CreateUpdateWineRepository : ICreateUpdateWineRepository
    {
        private readonly DataContext _dataContext;

        public CreateUpdateWineRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateWine(CreateWineRequest request)
        {
            var wineList = MapRequestToWineListModel(request);

            // Persist new wine
            _dataContext.Winelists.Add(wineList);
            try
            {
                var persisted = _dataContext.SaveChanges();
                return persisted >= 1;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }

        public bool UpdateWine(UpdateWineRequest request)
        {
            var wineList = MapRequestToWineListModel(request);
            wineList.Id = request.Id;

            // Persist update wine
            _dataContext.Winelists.Update(wineList);
            try
            {
                var persisted = _dataContext.SaveChanges();
                return persisted >= 1;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }

        private Models.Winelist MapRequestToWineListModel(CreateWineRequest request)
        {
            var newWine = new Models.Winelist
            {
                Vintage = (short?) request.Vintage,
                Winename = request.WineName,
                Percentalcohol = request.PercentAlcohol,
                Pricepaid = request.PricePaid,
                Yearbought = (short?) request.YearBought,
                Bottlesize = (short?) request.BottleSize,
                Drinkrangefrom = (short?) request.DrinkFrom,
                Drinkrangeto = (short?) request.DrinkTo,
                Notes = request.Notes,
                Rating = (short?) request.Rating,
                Locations = MapLocations(request.Locations)?.ToList()
            };

            // Adds Vineyard to new wine
            if (request.VineyardId != null)
            {
                newWine.Vineyardid = request.VineyardId;
            }
            else if (!string.IsNullOrEmpty(request.Vineyard))
            {
                var existingVineyard = _dataContext.Vineyards.FirstOrDefault(x => x.Vineyard1 == request.Vineyard);
                newWine.Vineyard = existingVineyard ?? new Models.Vineyard
                {
                    Vineyard1 = request.Vineyard
                };
            }

            // Adds Region to new wine
            if (request.RegionId != null)
            {
                newWine.Regionid = request.RegionId;
            }
            else if (!string.IsNullOrEmpty(request.Region))
            {
                var existingRegion = _dataContext.Regions.FirstOrDefault(x => x.Region1 == request.Region);
                newWine.Region = existingRegion ?? new Models.Region
                {
                    Region1 = request.Region
                };
            }

            // Adds Wine type to new wine
            if (request.WineTypeId != null)
            {
                newWine.Winetypeid = request.WineTypeId;
            }
            else if (!string.IsNullOrEmpty(request.WineType))
            {
                var existingWineType = _dataContext.Winetypes.FirstOrDefault(x => x.Winetype1 == request.WineType);
                newWine.Winetype = existingWineType ?? new Models.Winetype
                {
                    Winetype1 = request.WineType
                };
            }

            return newWine;
        }

        private IEnumerable<Models.Location> MapLocations(IEnumerable<Location> locations)
        {
            return locations?.Aggregate(new List<Models.Location>(), (locs, l) =>
            {
                var existingBox = _dataContext.Boxes.Find(l.BoxNo);
                if (existingBox != null)
                {
                    // Adds to existing box
                    locs.Add(new Models.Location
                    {
                        Box = l.BoxNo,
                        No = l.Qty,
                        Cellarversion = 1,
                    });
                }
                else if (l.BoxNo.HasValue)
                {
                    // Adds to new box, when provided with a box no
                    locs.Add(new Models.Location
                    {
                        BoxNavigation = new Box
                        {
                            Boxno = l.BoxNo.Value
                        },
                        No = l.Qty,
                        Cellarversion = 1
                    });
                }

                return locs;
            }).ToList();
        }
    }
}