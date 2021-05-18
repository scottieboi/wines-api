using System.Collections.Generic;

namespace WinesApi.Api.CreateWine
{
    public class Location
    {
        public int? BoxNo { get; set; }

        public int? Qty { get; set; }
    }

    public class CreateWineRequest
    {
        public string WineName { get; set; }

        public string Vineyard { get; set; }

        public int? VineyardId { get; set; }

        public string WineType { get; set; }

        public int? WineTypeId { get; set; }

        public string Region { get; set; }

        public int? RegionId { get; set; }

        public int? Vintage { get; set; }

        public int? YearBought { get; set; }

        public int? DrinkFrom { get; set; }

        public int? DrinkTo { get; set; }

        public float? PercentAlcohol { get; set; }

        public decimal? PricePaid { get; set; }

        public int? Rating { get; set; }

        public int? BottleSize { get; set; }

        public string Notes { get; set; }

        public IEnumerable<Location> Locations { get; set; }
    }
}