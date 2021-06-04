using System.Collections.Generic;
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
                Boxno = 101,
                Size = "Tiny"
            };

            var wineList = new Winelist
            {
                Id = 50,
                Winetype = new Winetype
                {
                    Winetype1 = "Sav Blanc"
                },
                Region = new Region
                {
                    Region1 = "My Region"
                },
                Vineyard = new Vineyard
                {
                    Vineyard1 = "My vineyard"
                },
                Locations = new List<Location>
                {
                    new Location
                    {
                        BoxNavigation = new Box
                        {
                            Boxno = 55,
                            Size = "big"
                        },
                        No = 2
                    }
                }
            };

            context.AddRange(region, wineType, vineyard, box, wineList);
            context.SaveChanges();
        }
    }
}