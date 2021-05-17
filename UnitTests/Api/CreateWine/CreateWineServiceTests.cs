using FluentAssertions;
using WinesApi.Api.CreateWine;
using WinesApi.Models;
using Xunit;

namespace UnitTests.Api.CreateWine
{
    public class CreateWineServiceTests : CreateWineServiceTestBase
    {
        [Fact]
        public void ValidateWineModel_ShouldNotReturnErrors_WhenIdExistsInDb()
        {
            // __Arrange __
            var model = new CreateWineRequest
            {
                RegionId = 1,
                Region = "Yarra valley",
                WineTypeId = 1,
                WineType = "Shiraz",
                VineyardId = 1,
                Vineyard = "Mark's vineyard"
            };

            using var context = new DataContext(ContextOptions);
            var sut = new CreateWineService(context);

            // __ Act __
            var result = sut.ValidateWineModel(model);

            // __ Assert __
            result.Should().BeEmpty();
        }
    }
}