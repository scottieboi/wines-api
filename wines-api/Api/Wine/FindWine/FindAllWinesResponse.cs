namespace WinesApi.Api.Wine.FindWine
{
    public class FindAllWinesResponse
    {
        public int Id { get; set; }

        public string WineName { get; set; }

        public string WineType { get; set; }

        public string Vineyard { get; set; }

        public int? Vintage { get; set; }

        public int Qty { get; set; }
    }
}