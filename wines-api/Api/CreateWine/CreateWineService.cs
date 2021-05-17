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

        // TODO: complete this function
        public bool CreateWine(CreateWineRequest wine)
        {
            int? newVineyardId = null;
            int? newRegionId = null;
            int? newWineTypeId = null;

            // Create Vineyard, WineType, Region - if not already exists
            if (wine.VineyardId == null && !string.IsNullOrEmpty(wine.Vineyard))
            {
                var existingVineyard = _dataContext.Vineyards.FirstOrDefault(x => x.Vineyard1 == wine.Vineyard);
                if (existingVineyard == null)
                {
                    // _dataContext.Vineyards.Add()
                }
                else
                {
                    newVineyardId = existingVineyard.Id;
                }
            }

            return true;
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

            return errors;
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
    }
}