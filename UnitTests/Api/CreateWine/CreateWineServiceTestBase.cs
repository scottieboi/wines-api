using UnitTests.TestInfrastructure;
using WinesApi.Models;

namespace UnitTests.Api.CreateWine
{
    public abstract class CreateWineServiceTestBase : SqliteTestBase
    {
        protected CreateWineServiceTestBase()
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

            context.AddRange(region, wineType, vineyard);
            context.SaveChanges();
        }
    }
}