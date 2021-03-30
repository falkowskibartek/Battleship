using Battleship.Domain;
using Battleship.Domain.OceanGrid;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Battleship.Tests
{
    public class ShipTest
    {
        [Fact]
        public void When_ShipTakesAMissedShot_Then_ItReturnsMiss()
        {
            // arrange
            var shotCoordinate = new Coordinate(10, "A5");
            var shipCoordinates =
                new List<Coordinate>
                { new Coordinate(10, "A1"), new Coordinate(10, "A2"), new Coordinate(10, "A3"), new Coordinate(10, "A4") };
            var sut = Ship.CreateDestroyer(shipCoordinates);

            // act
            var result = sut.TakeTheShot(shotCoordinate);

            // assert
            result.Should().Be(ShotResultEnum.Miss);
        }

        [Fact]
        public void When_ShipTakesAHit_Then_ItReturnsHit()
        {
            // arrange
            var shotCoordinate = new Coordinate(10, "A1");
            var shipCoordinates =
                new List<Coordinate>
                { new Coordinate(10, "A1"), new Coordinate(10, "A2"), new Coordinate(10, "A3"), new Coordinate(10, "A4") };
            var sut = Ship.CreateDestroyer(shipCoordinates);

            // act
            var result = sut.TakeTheShot(shotCoordinate);

            // assert
            result.Should().Be(ShotResultEnum.Hit);
        }

        [Theory]
        [InlineData("A1", "A2", "A3", "A4")]
        [InlineData("A4", "A3", "A2", "A1")]
        [InlineData("A2", "A3", "A4", "A1")]
        [InlineData("A1", "A3", "A2", "A4")]
        [InlineData("A1", "A4", "A2", "A3")]
        [InlineData("A1", "A1", "A4", "A2", "A3")]
        [InlineData("A1", "A4", "A7", "A2", "A3")]
        [InlineData("A1", "A4", "A2", "A8", "A3")]
        public void When_DestroyerTakesFourDifferentHits_Then_ShipIsSunk(params string[] rawCoordinates)
        {
            // arrange
            var shotsCoordinates = rawCoordinates.AsEnumerable().Select(c => new Coordinate(10, c));
            var shipCoordinates =
                new List<Coordinate>
                    { new Coordinate(10, "A1"), new Coordinate(10, "A2"), new Coordinate(10, "A3"), new Coordinate(10, "A4") };
            var sut = Ship.CreateDestroyer(shipCoordinates);

            // act
            foreach (var shotCoordinate in shotsCoordinates)
            {
                sut.TakeTheShot(shotCoordinate);
            }

            // assert
            sut.IsSunk.Should().BeTrue();
        }

        [Theory]
        [InlineData("A1", "A2", "A3", "A5")]
        [InlineData("A5", "A3", "A2", "A1")]
        [InlineData("A5", "A6", "A7", "A8")]
        [InlineData("A1", "A1", "A1", "A1")]
        [InlineData("A1", "A4", "A2", "A8")]
        public void When_DestroyerTakesLessThanFourDifferentHits_Then_ShipIsNotSunk(params string[] rawCoordinates)
        {
            // arrange
            var shotsCoordinates = rawCoordinates.AsEnumerable().Select(c => new Coordinate(10, c));
            var shipCoordinates =
                new List<Coordinate>
                    { new Coordinate(10, "A1"), new Coordinate(10, "A2"), new Coordinate(10, "A3"), new Coordinate(10, "A4") };
            var sut = Ship.CreateDestroyer(shipCoordinates);

            // act
            foreach (var shotCoordinate in shotsCoordinates)
            {
                sut.TakeTheShot(shotCoordinate);
            }

            // assert
            sut.IsSunk.Should().BeFalse();
        }

        [Fact]
        public void When_DestroyerIsCreated_Then_NameIsDestroyer()
        {
            // arrange
            var shipCoordinates =
                new List<Coordinate>
                    { new Coordinate(10, "A1"), new Coordinate(10, "A2"), new Coordinate(10, "A3"), new Coordinate(10, "A4") };
            var destroyer = Ship.CreateDestroyer(shipCoordinates);

            // act
            var result = destroyer.Name;

            // assert
            result.Should().Be("Destroyer");
        }

        [Fact]
        public void When_DestroyerIsCreated_Then_LengthIsFour()
        {
            // arrange
            int gridSize = 10;
            var destroyer = Ship.CreateDestroyer(gridSize);

            // act
            var result = destroyer.Holes;

            // assert
            result.Should().HaveCount(4);
        }

        [Fact]
        public void When_BattleshipIsCreated_Then_LengthIsFive()
        {
            // arrange
            int gridSize = 10;
            var destroyer = Ship.CreateBattleship(gridSize);

            // act
            var result = destroyer.Holes;

            // assert
            result.Should().HaveCount(5);
        }

        [Fact]
        public void When_DestroyerIsCreated_Then_AllCoordinatesAreValid()
        {
            // arrange
            int gridSize = 10;
            var destroyer = Ship.CreateDestroyer(gridSize);

            // act
            var result = destroyer.Holes.Select(element => element.Coordinate);

            // assert
            result.Should().OnlyContain(coordinate => coordinate.IsValid);
        }

        [Fact]
        public void When_BattleshipIsCreated_Then_AllCoordinatesAreValid()
        {
            // arrange
            int gridSize = 10;
            var destroyer = Ship.CreateBattleship(gridSize);

            // act
            var result = destroyer.Holes.Select(element => element.Coordinate);

            // assert
            result.Should().OnlyContain(coordinate => coordinate.IsValid);
        }

        [Fact]
        public void Given_ShipIsCreatedWithCoordinates_When_GetCoordinatesIsCalled_ItReturnsAllFlattenedCoordinates()
        {
            // arrange
            int gridSize = 10;
            var coordinates = new List<Coordinate>{
                new Coordinate(gridSize, "A1"), new Coordinate(gridSize, "B2"), new Coordinate(gridSize, "C3")
            };
            var sut = Ship.CreateBattleship(coordinates);

            // act
            var result = sut.GetCoordinates();

            // assert
            result.Should().BeEquivalentTo(new List<Coordinate>{
                new Coordinate(gridSize, "A1"), new Coordinate(gridSize, "B2"), new Coordinate(gridSize, "C3")
            });
        }
    }
}
