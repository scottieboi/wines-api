using System.Collections.Generic;

namespace WinesApi.Api.FindWines
{
    public class FindWineResponse
    {
        public int Id { get; set; }

        public string WineName { get; set; }

        public string WineType { get; set; }

        public string Vineyard { get; set; }

        public int? Vintage { get; set; }

        public IEnumerable<Box> Boxes { get; set; }
    }
}