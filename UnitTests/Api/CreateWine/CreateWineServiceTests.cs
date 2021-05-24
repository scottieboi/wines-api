﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UnitTests.TestInfrastructure;
using WinesApi.Api.CreateWine;
using WinesApi.Models;
using Xunit;
using Location = WinesApi.Api.CreateWine.Location;

namespace UnitTests.Api.CreateWine
{
    public class CreateWineServiceTests : SqliteTestBase
    {
        public CreateWineServiceTests()
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

        [Fact]
        public void ValidateWineModel_ShouldReturnErrors_WhenIdDoesNotExistInDb()
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
            var sut = new CreateWineService(context);

            // __ Act __
            var result = sut.ValidateWineModel(model).ToList();

            // __ Assert __
            result.Should().Contain(x => x.Contains("Vineyard id") && x.Contains("5"));
            result.Should().Contain(x => x.Contains("Wine type id") && x.Contains("4"));
            result.Should().Contain(x => x.Contains("Region id") && x.Contains("3"));
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
            var sut = new CreateWineService(context);

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
            var sut = new CreateWineService(context);

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
        public void CreateWine_ShouldCreateWine_WhenGivenValidRequestModel()
        {
            // __Arrange __
            var model = new CreateWineRequest
            {
                WineName = "Sample",
                PercentAlcohol = 14.5f,
                PricePaid = 20.99m,
                Notes = "These are my notes",
                Vintage = 2000,
                YearBought = 2002,
                DrinkFrom = 2005,
                DrinkTo = 2015,
                Rating = 92,
                BottleSize = 750,
                Region = "Hunter Valley",
                Vineyard = "Dave's place",
                WineType = "Chardonnay"
            };

            using var context = new DataContext(ContextOptions);
            var sut = new CreateWineService(context);

            // __ Act __
            var result = sut.CreateWine(model);

            // __ Assert __
            result.Should().BeTrue();
            var vineyard = context.Vineyards.Single(x => x.Vineyard1 == "Dave's place");
            var region = context.Regions.Single(x => x.Region1 == "Hunter Valley");
            var wineType = context.Winetypes.Single(x => x.Winetype1 == "Chardonnay");
            var wine = context.Winelists.First(x => x.Winename == "Sample");
            wine.Winename.Should().Be("Sample");
            wine.Percentalcohol.Should().Be(14.5f);
            wine.Pricepaid.Should().Be(20.99m);
            wine.Notes.Should().Be("These are my notes");
            wine.Vintage.Should().Be(2000);
            wine.Yearbought.Should().Be(2002);
            wine.Drinkrangefrom.Should().Be(2005);
            wine.Drinkrangeto.Should().Be(2015);
            wine.Rating.Should().Be(92);
            wine.Bottlesize.Should().Be(750);
            wine.Regionid.Should().Be(region.Id);
            region.Region1.Should().Be("Hunter Valley");
            wine.Vineyardid.Should().Be(vineyard.Id);
            vineyard.Vineyard1.Should().Be("Dave's place");
            wine.Winetypeid.Should().Be(wineType.Id);
            wineType.Winetype1.Should().Be("Chardonnay");
        }

        [Fact]
        public void CreateWine_ShouldCreateWine_WhenGivenExistingRegionVineyardAndWineType()
        {
            // __Arrange __
            var model = new CreateWineRequest
            {
                WineName = "Sample",
                RegionId = 1,
                VineyardId = 1,
                WineTypeId = 1
            };

            using var context = new DataContext(ContextOptions);
            var sut = new CreateWineService(context);

            // __ Act __
            var result = sut.CreateWine(model);

            // __ Assert __
            result.Should().BeTrue();
            var vineyard = context.Vineyards.Find(1);
            var region = context.Regions.Find(1);
            var wineType = context.Winetypes.Find(1);
            var wine = context.Winelists.First(x => x.Winename == "Sample");
            wine.Regionid.Should().Be(1);
            region.Region1.Should().Be("Yarra valley");
            wine.Vineyardid.Should().Be(1);
            vineyard.Vineyard1.Should().Be("Mark's vineyard");
            wine.Winetypeid.Should().Be(1);
            wineType.Winetype1.Should().Be("Shiraz");
        }

        [Fact]
        public void CreateWine_ShouldCreateWine_WhenGivenExistingRegionVineyardAndWineTypeAsDuplicates()
        {
            // __Arrange __
            var model = new CreateWineRequest
            {
                WineName = "Sample",
                Vineyard = "Mark's vineyard",
                Region = "Yarra valley",
                WineType = "Shiraz"
            };

            using var context = new DataContext(ContextOptions);
            var sut = new CreateWineService(context);

            // __ Act __
            var result = sut.CreateWine(model);

            // __ Assert __
            result.Should().BeTrue();
            var vineyard = context.Vineyards.Find(1);
            var region = context.Regions.Find(1);
            var wineType = context.Winetypes.Find(1);
            var wine = context.Winelists.First(x => x.Winename == "Sample");
            wine.Regionid.Should().Be(1);
            region.Region1.Should().Be("Yarra valley");
            wine.Vineyardid.Should().Be(1);
            vineyard.Vineyard1.Should().Be("Mark's vineyard");
            wine.Winetypeid.Should().Be(1);
            wineType.Winetype1.Should().Be("Shiraz");
        }

        [Fact]
        public void CreateWine_ShouldCreateWine_WhenGivenValidRequestModelWithLocations()
        {
            // __Arrange __
            var model = new CreateWineRequest
            {
                WineName = "Sample",
                PercentAlcohol = 14.5f,
                PricePaid = 20.99m,
                Notes = "These are my notes",
                Vintage = 2000,
                Region = "Hunter Valley",
                Locations = new List<Location>
                {
                    new Location { BoxNo = 55, Qty = 2 },
                    new Location { BoxNo = 2, Qty = 1 },
                    new Location { Qty = 3 }
                }
            };

            using var context = new DataContext(ContextOptions);
            var sut = new CreateWineService(context);

            // __ Act __
            var result = sut.CreateWine(model);

            // __ Assert __
            result.Should().BeTrue();
            var region = context.Regions.Single(x => x.Region1 == "Hunter Valley");
            var wine = context.Winelists.First(x => x.Winename == "Sample");
            wine.Winename.Should().Be("Sample");
            wine.Notes.Should().Be("These are my notes");
            wine.Vintage.Should().Be(2000);
            wine.Regionid.Should().Be(region.Id);
            region.Region1.Should().Be("Hunter Valley");

            var locations = context.Locations.Where(x => x.Wineid == wine.Id).ToList();
            wine.Locations.Select(x => x.Id).Should().BeEquivalentTo(locations.Select(x => x.Id));
            locations.Should().HaveCount(2);
            locations.Should().Contain(x => x.Box == 55 && x.No == 2);
            locations.Should().Contain(x => x.Box == 2 && x.No == 1);
            locations.Should().NotContain(x => x.No == 3);
        }
    }
}