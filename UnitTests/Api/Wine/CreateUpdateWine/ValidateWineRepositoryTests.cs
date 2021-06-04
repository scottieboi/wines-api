using System.Linq;
using FluentAssertions;
using WinesApi.Api.Wine.CreateUpdateWine;
using WinesApi.Models;
using Xunit;

namespace UnitTests.Api.Wine.CreateUpdateWine
{
    public class ValidateWineRepositoryTests : WineTestBase
    {
        private const string CouldNotFindMessage = "could not be found";

        [Fact]
        public void ValidateWineModel_ShouldNotReturnErrors_WhenFksExistsInDb()
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
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model);

            // __ Assert __
            result.Should().BeEmpty();
        }

        [Fact]
        public void ValidateWineModel_ShouldReturnErrors_WhenMissingFkEntries()
        {
            // __Arrange __
            var model = new CreateWineRequest
            {
                RegionId = 3,
                Region = "Black valley",
                WineTypeId = 4,
                WineType = "Chardonnay",
                VineyardId = 5,
                Vineyard = "Steve's vineyard"
            };

            using var context = new DataContext(ContextOptions);
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model).ToList();

            // __ Assert __
            result.Should()
                .Contain(x => x.Contains("Vineyard id") && x.Contains("5") && x.Contains(CouldNotFindMessage));
            result.Should().Contain(x =>
                x.Contains("Wine type id") && x.Contains("4") && x.Contains(CouldNotFindMessage));
            result.Should().Contain(x => x.Contains("Region id") && x.Contains("3") && x.Contains(CouldNotFindMessage));
        }

        [Fact]
        public void ValidateWineModel_ShouldNotReturnErrors_WhenValidYearAndRatingProvided()
        {
            // __Arrange __
            var model = new CreateWineRequest
            {
                Vintage = 2000,
                YearBought = 2002,
                DrinkFrom = 2005,
                DrinkTo = 2015,
                Rating = 92,
                BottleSize = 750
            };

            using var context = new DataContext(ContextOptions);
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model);

            // __ Assert __
            result.Should().BeEmpty();
        }

        [Fact]
        public void ValidateWineModel_ShouldReturnErrors_WhenInvalidYearAndRatingProvided()
        {
            // __Arrange __
            var model = new CreateWineRequest
            {
                Vintage = 20500,
                YearBought = 202,
                DrinkFrom = 20305,
                DrinkTo = 42015,
                Rating = 921,
                BottleSize = 100000
            };

            using var context = new DataContext(ContextOptions);
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model).ToList();

            // __ Assert __
            result.Should().Contain(x => x == "Vintage is not a valid year");
            result.Should().Contain(x => x == "Year bought is not a valid year");
            result.Should().Contain(x => x == "Drink from is not a valid year");
            result.Should().Contain(x => x == "Drink to is not a valid year");
            result.Should().Contain(x => x == "Rating is not between 1 and 100");
            result.Should().Contain(x => x == "Bottle size is invalid");
        }

        [Fact]
        public void ValidateUpdateWineModel_ShouldNotReturnErrors_WhenFksExistsInDb()
        {
            // __Arrange __
            var model = new UpdateWineRequest
            {
                Id = 50,
                RegionId = 1,
                Region = "Yarra valley",
                WineTypeId = 1,
                WineType = "Shiraz",
                VineyardId = 1,
                Vineyard = "Mark's vineyard"
            };

            using var context = new DataContext(ContextOptions);
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model);

            // __ Assert __
            result.Should().BeEmpty();
        }

        [Fact]
        public void ValidateUpdateWineModel_ShouldReturnErrors_WhenMissingFkEntries()
        {
            // __Arrange __
            var model = new UpdateWineRequest
            {
                Id = 50,
                RegionId = 3,
                Region = "Black valley",
                WineTypeId = 4,
                WineType = "Chardonnay",
                VineyardId = 5,
                Vineyard = "Steve's vineyard"
            };

            using var context = new DataContext(ContextOptions);
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model).ToList();

            // __ Assert __
            result.Should()
                .Contain(x => x.Contains("Vineyard id") && x.Contains("5") && x.Contains(CouldNotFindMessage));
            result.Should().Contain(x =>
                x.Contains("Wine type id") && x.Contains("4") && x.Contains(CouldNotFindMessage));
            result.Should().Contain(x => x.Contains("Region id") && x.Contains("3") && x.Contains(CouldNotFindMessage));
        }

        [Fact]
        public void ValidateUpdateWineModel_ShouldNotReturnErrors_WhenValidYearAndRatingProvided()
        {
            // __Arrange __
            var model = new UpdateWineRequest
            {
                Id = 50,
                Vintage = 2000,
                YearBought = 2002,
                DrinkFrom = 2005,
                DrinkTo = 2015,
                Rating = 92,
                BottleSize = 750
            };

            using var context = new DataContext(ContextOptions);
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model);

            // __ Assert __
            result.Should().BeEmpty();
        }

        [Fact]
        public void ValidateUpdateWineModel_ShouldReturnErrors_WhenInvalidYearAndRatingProvided()
        {
            // __Arrange __
            var model = new UpdateWineRequest
            {
                Id = 50,
                Vintage = 20500,
                YearBought = 202,
                DrinkFrom = 20305,
                DrinkTo = 42015,
                Rating = 921,
                BottleSize = 100000
            };

            using var context = new DataContext(ContextOptions);
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model).ToList();

            // __ Assert __
            result.Should().Contain(x => x == "Vintage is not a valid year");
            result.Should().Contain(x => x == "Year bought is not a valid year");
            result.Should().Contain(x => x == "Drink from is not a valid year");
            result.Should().Contain(x => x == "Drink to is not a valid year");
            result.Should().Contain(x => x == "Rating is not between 1 and 100");
            result.Should().Contain(x => x == "Bottle size is invalid");
        }

        [Fact]
        public void ValidateUpdateWineModel_ShouldReturnErrors_WhenWineListDoesNotExist()
        {
            // __Arrange __
            var model = new UpdateWineRequest
            {
                Id = 30,
                RegionId = 5,
                Region = "Yarra valley 2",
                WineTypeId = 1,
                WineType = "Shiraz",
                VineyardId = 1,
                Vineyard = "Mark's vineyard"
            };

            using var context = new DataContext(ContextOptions);
            var sut = new ValidateWineRepository(context);

            // __ Act __
            var result = sut.ValidateWineModel(model);

            // __ Assert __
            result.Should().Contain(x =>
                x.Contains("Wine id") && x.Contains("30") && x.Contains(CouldNotFindMessage));
        }
    }
}