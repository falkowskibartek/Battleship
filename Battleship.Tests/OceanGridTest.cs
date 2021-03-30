using Battleship.Domain;
using Battleship.Domain.OceanGrid;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Battleship.Tests
{
    internal class BattleShipFixture
    {
        private readonly List<Coordinate> _defaultCoordinates;
        private readonly Ship _defaultDestroyer;
        private readonly List<Ship> _defaultShips;

        public BattleShipFixture()
        {
            _defaultCoordinates =
                new List<Coordinate>
                    { new Coordinate(10, "A1"), new Coordinate(10, "A2"), new Coordinate(10, "A3"), new Coordinate(10, "A4") };
            _defaultDestroyer = Ship.CreateDestroyer(_defaultCoordinates);
            _defaultShips = new List<Ship> { _defaultDestroyer };
        }

        internal Ship CreateDefaultShip(IEnumerable<Coordinate> coordinates)
        {
            if (coordinates == null)
            {
                return _defaultDestroyer;
            }

            return Ship.CreateDestroyer(coordinates);
        }

        internal OceanGrid CreateOceanGrid(IEnumerable<Ship> ships = null)
        {
            return new OceanGrid(ships ?? _defaultShips);
        }
    }

    public class OceanGridTest
    {
        private readonly BattleShipFixture _fixture = new BattleShipFixture();

        [Fact]
        public void When_ShotIsTaken_Then_ResultIsProvided()
        {
            // arrange
            var coordinate = new Coordinate(10, "A5");
            var sut = _fixture.CreateOceanGrid();

            // act
            var result = sut.TakeTheShot(coordinate);

            // assert
            result.Should().BeOfType<ShotResult>();
        }

        [Fact]
        public void When_ShotIsTakenWithInvalidCoordinate_Then_ResultIsMissed()
        {
            // arrange
            var invalidCoordinate = new Coordinate(10, "invalid");
            var sut = _fixture.CreateOceanGrid();

            // act
            var result = sut.TakeTheShot(invalidCoordinate);

            // assert
            result.Result.Should().Be(ShotResultEnum.Miss);
        }

        [Fact]
        public void When_OceanGridTakesAHitThen_ReturnsHitWithShipName()
        {
            // arrange
            var hitCoordinate = new Coordinate(10, "A1");
            var sut = _fixture.CreateOceanGrid();

            // act
            var result = sut.TakeTheShot(hitCoordinate);

            // assert
            result.Result.Should().Be(ShotResultEnum.Hit);
            result.ShipName.Should().Be("Destroyer");
        }

        [Fact]
        public void When_OceanGridTakesAMissThen_ReturnsMiss()
        {
            // arrange
            var missCoordinate = new Coordinate(10, "A5");
            var sut = _fixture.CreateOceanGrid();

            // act
            var result = sut.TakeTheShot(missCoordinate);

            // assert
            result.Result.Should().Be(ShotResultEnum.Miss);
        }

        [Fact]
        public void Given_OceanGridWithOneDestroyer_When_ItTakesFourHitsAndDestroyerIsSunk_Then_AllTheShipsAreSunkIsTrue()
        {
            // arrange
            var firstHitCoordinate = new Coordinate(10, "A1");
            var secondHitCoordinate = new Coordinate(10, "A2");
            var thirdHitCoordinate = new Coordinate(10, "A3");
            var fourthHitCoordinate = new Coordinate(10, "A4");

            var sut = _fixture.CreateOceanGrid();

            // act
            sut.TakeTheShot(firstHitCoordinate);
            sut.TakeTheShot(secondHitCoordinate);
            sut.TakeTheShot(thirdHitCoordinate);
            sut.TakeTheShot(fourthHitCoordinate);
            
            var result = sut.IsTheEntireFleetSunk;

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void When_OceanGridIsCreated_Then_ItConsistsOfThreeShips()
        {
            // arrange
            var grid = new Grid();
            var sut = new OceanGrid(grid);

            // act
            var result = sut.Fleet;

            // assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public void When_OceanGridIsCreated_Then_ItConsistsOfTwoDestroyersAndOneBattleship()
        {
            // arrange
            var expected = new List<string> { "Battleship", "Destroyer", "Destroyer" };
            var grid = new Grid();
            var sut = new OceanGrid(grid);

            // act
            var result = sut.Fleet;

            // assert
            result.Select(ship => ship.Name).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_OceanGridIsCreated_Then_NoneOfTheShipsOverlapsEachOther()
        {
            // arrange
            var grid = new Grid();
            var sut = new OceanGrid(grid);

            // act
            var result = sut.Fleet;

            // assert
            var flattenCoordinates = result.SelectMany(ship => ship.GetCoordinates()).ToList();
            flattenCoordinates.Should().OnlyHaveUniqueItems();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Given_ShipSizeIsGreaterThanGridSize_When_OceanGridIsCreated_Then_ArgumentExceptionIsThrown(int gridSize)
        {
            // arrange
            var grid = new Grid(gridSize);

            // act
            Action sut = () => new OceanGrid(grid);

            // assert
            sut.Should().ThrowExactly<ArgumentException>();
        }
    }
}
