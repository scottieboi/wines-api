using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WinesApi.Api.Wine.CreateUpdateWine
{
    public class CreateUpdateWineRepository : ICreateUpdateWineRepository
    {
        private readonly Models.DataContext _dataContext;

        public CreateUpdateWineRepository(Models.DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateWine(CreateWineRequest request)
        {
            try
            {
                var wineList = MapRequestToWineListModel(request);
                _dataContext.Winelists.Add(wineList);
                
                var persisted = _dataContext.SaveChanges();
                return persisted >= 1;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool UpdateWine(UpdateWineRequest request)
        {
            // Check wine already exists
            var existingWine = _dataContext.Winelists.Find(request.Id);
            if (existingWine == null)
            {
                return false;
            }
            
            // Clear any existing locations
            _dataContext.Entry(existingWine).Collection(w => w.Locations).Load();
            if (existingWine.Locations?.Any() ?? false)
            {
                _dataContext.Locations.RemoveRange(existingWine.Locations);
            }

            try
            {
                // Populate data in WineList model, and persist
                var wineList = MapRequestToWineListModel(request, existingWine);
                _dataContext.Winelists.Update(wineList);
                
                var persisted = _dataContext.SaveChanges();
                return persisted >= 1;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        private Models.Winelist MapRequestToWineListModel(
            CreateWineRequest request,
            Models.Winelist existingWine = null)
        {
            var newWine = existingWine ?? new Models.Winelist();

            newWine.Vintage = (short?) request.Vintage;
            newWine.Winename = request.WineName;
            newWine.Percentalcohol = request.PercentAlcohol;
            newWine.Pricepaid = request.PricePaid;
            newWine.Yearbought = (short?) request.YearBought;
            newWine.Bottlesize = (short?) request.BottleSize;
            newWine.Drinkrangefrom = (short?) request.DrinkFrom;
            newWine.Drinkrangeto = (short?) request.DrinkTo;
            newWine.Notes = request.Notes;
            newWine.Rating = (short?) request.Rating;
            newWine.Locations = MapLocations(request.Locations)?.ToList();

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
                        BoxNavigation = new Models.Box
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