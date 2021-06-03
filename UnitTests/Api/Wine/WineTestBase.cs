using UnitTests.TestInfrastructure;
using WinesApi.Models;

namespace UnitTests.Api.Wine
{
    public abstract class WineTestBase : SqliteTestBase
    {
        protected WineTestBase()
        {
            Seed();
        }

        private void Seed()
        {
            using var context = new DataContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var region = new Region
            {
                Id = 1,
                Region1 = "Yarra valley"
            };

            var wineType = new Winetype
            {
                Id = 1,
                Winetype1 = "Shiraz"
            };

            var vineyard = new Vineyard
            {
                Id = 1,
                Vineyard1 = "Mark's vineyard"
            };

            var box = new Box
            {
                Boxno = 55,
                Size = "big"
            };

            context.AddRange(region, wineType, vineyard, box);
            context.SaveChanges();
        }
    }
}