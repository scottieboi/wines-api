using System.Collections.Generic;
using System.Linq;
using WinesApi.Models;

namespace WinesApi.Api.CreateWine
{
    public class CreateWineService : ICreateWineService
    {
        private readonly DataContext _dataContext;

        public CreateWineService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateWine(CreateWineRequest request)
        {
            var newWine = new Models.Winelist
            {
                Vintage = (short?)request.Vintage,
                Winename = request.WineName,
                Percentalcohol = request.PercentAlcohol,
                Pricepaid = request.PricePaid,
                Yearbought = (short?)request.YearBought,
                Bottlesize = (short?)request.BottleSize,
                Drinkrangefrom = (short?)request.DrinkFrom,
                Drinkrangeto = (short?)request.DrinkTo,
                Notes = request.Notes,
                Rating = (short?)request.Rating,
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

            // Persist new wine
            _dataContext.Winelists.Add(newWine);
            var persisted = _dataContext.SaveChanges();
            return persisted >= 1;
        }

        public IEnumerable<string> ValidateWineModel(CreateWineRequest wine)
        {
            var errors = new List<string>();
            if (wine.VineyardId != null)
            {
                var vineyard = _dataContext.Vineyards.Find(wine.VineyardId);
                if (vineyard == null)
                {
                    // Existing vineyard cannot be found
                    errors.Add("Vineyard id: " + wine.VineyardId + " could not be found");
                }
            }

            if (wine.WineTypeId != null)
            {
                var wineType = _dataContext.Vineyards.Find(wine.WineTypeId);
                if (wineType == null)
                {
                    // Existing wineType cannot be found
                    errors.Add("Wine type id: " + wine.WineTypeId + " could not be found");
                }
            }

            if (wine.RegionId != null)
            {
                var region = _dataContext.Vineyards.Find(wine.RegionId);
                if (region == null)
                {
                    // Existing region cannot be found
                    errors.Add("Region id: " + wine.RegionId + " could not be found");
                }
            }

            if (!ValidateYear(wine.Vintage))
            {
                errors.Add("Vintage is not a valid year");
            }

            if (!ValidateYear(wine.YearBought))
            {
                errors.Add("Year bought is not a valid year");
            }

            if (!ValidateYear(wine.DrinkFrom))
            {
                errors.Add("Drink from is not a valid year");
            }

            if (!ValidateYear(wine.DrinkTo))
            {
                errors.Add("Drink to is not a valid year");
            }

            if (!ValidateRating(wine.Rating))
            {
                errors.Add("Rating is not between 1 and 100");
            }

            if (!ValidateBottleSize(wine.BottleSize))
            {
                errors.Add("Bottle size is invalid");
            }

            return errors;
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
                        No = l.Qty
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
                        No = l.Qty
                    });
                }

                return locs;
            }).ToList();
        }

        /// <summary>
        /// Validates a given year
        /// </summary>
        /// <param name="year">Year to validate</param>
        /// <returns>True when year is null, or true when valid year</returns>
        private static bool ValidateYear(int? year)
        {
            if (year == null)
            {
                return true;
            }
            return year >= 1000 && year <= 9999;
        }

        /// <summary>
        /// Validates a given rating
        /// </summary>
        /// <param name="rating">Rating to validate</param>
        /// <returns>True when rating is null, or true when valid rating</returns>
        private static bool ValidateRating(int? rating)
        {
            if (rating == null)
            {
                return true;
            }
            return rating >= 1 && rating <= 100;
        }

        /// <summary>
        /// Validates a given bottle size
        /// </summary>
        /// <param name="rating">Rating to validate</param>
        /// <returns>True when rating is null, or true when valid bottle size in mL</returns>
        private static bool ValidateBottleSize(int? bottleSize)
        {
            if (bottleSize == null)
            {
                return true;
            }
            return bottleSize >= 1 && bottleSize <= short.MaxValue;
        }
    }
}