using Battleship.Domain;
using Battleship.Domain.TargetGrid;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Battleship.Tests
{
    public class TargetGridTests
    {
        [Fact]
        public void When_SquareIsNotShotYet_Then_PegIsNotPlaced()
        {
            // arrange
            var holeCoordinate = new Coordinate(10, "A1");
            var hole = new TargetGridHole(holeCoordinate);

            // act
            var result = hole.Status;

            // assert
            result.Should().Be(TargetGridHoleStatus.NoPeg);
        }

        [Fact]
        public void When_SquareIsShotButShipMissed_Then_WhitePegIsPlaced()
        {
            // arrange
            var holeCoordinate = new Coordinate(10, "A1");
            var hole = new TargetGridHole(holeCoordinate);

            // act
            hole.Mark(ShotResultEnum.Miss);
            var result = hole.Status;

            // assert
            result.Should().Be(TargetGridHoleStatus.WhitePeg);
        }

        [Fact]
        public void When_SquareIsShotAndShipHit_Then_RedPegIsPlaced()
        {
            // arrange
            var holeCoordinate = new Coordinate(10, "A1");
            var hole = new TargetGridHole(holeCoordinate);

            // act
            hole.Mark(ShotResultEnum.Hit);
            var result = hole.Status;

            // assert
            result.Should().Be(TargetGridHoleStatus.RedPeg);
        }

        [Fact]
        public void When_TargetGridSquareIsCreated_Then_ListOfSquaresIsBasedOnSize()
        {
            // arrange
            var grid = new Grid();
            var sut = new TargetGrid(grid);

            // act
            var result = sut.Holes.Count;

            // assert
            result.Should().Be(100);
        }

        [Fact]
        public void Given_TargetGridIsCreated_When_NoShotsAreMarked_Then_AllSquaresHaveNoPegs()
        {
            // arrange
            var grid = new Grid();
            var sut = new TargetGrid(grid);

            // act
            var result = sut.Holes.Select(square => square.Status);

            // 
            result.Should().AllBeEquivalentTo(TargetGridHoleStatus.NoPeg);
        }

        [Fact]
        public void Given_TargetGridIsCreated_When_SquareIsMarkedAsMissed_Then_SquareInListHasWhitePeg()
        {
            // arrange
            var shotCoordinate = new Coordinate(10, "A1");
            var missedShot = new TargetGridMark(shotCoordinate, ShotResultEnum.Miss);
            var grid = new Grid();
            var sut = new TargetGrid(grid);

            // act
            sut.MarkShot(missedShot);
            var result = sut.Holes.First(square => square.Coordinate == shotCoordinate).Status;

            // 
            result.Should().Be(TargetGridHoleStatus.WhitePeg);
        }

        [Fact]
        public void Given_TargetGridIsCreated_When_SquareIsMarkedAsHit_Then_SquareInListHasRedPeg()
        {
            // arrange
            var shotCoordinate = new Coordinate(10, "A1");
            var missedShot = new TargetGridMark(shotCoordinate, ShotResultEnum.Hit);
            var grid = new Grid();
            var sut = new TargetGrid(grid);

            // act
            sut.MarkShot(missedShot);
            var result = sut.Holes.First(square => square.Coordinate == shotCoordinate).Status;

            // 
            result.Should().Be(TargetGridHoleStatus.RedPeg);
        }

        [Fact]
        public void Given_TargetGridIsCreated_When_NoShotsAreMarked_Then_ToStringConsistsOfWhiteSpaces()
        {
            // arrange
            var expected = "  1 2\r\nA    \r\nB    \r\n";
            var grid = new Grid(2);
            var sut = new TargetGrid(grid);

            // act
            var result = sut.ToString();

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Given_TargetGridIsCreated_When_AllShotsAreMarkedAsMisses_Then_ToStringConsistsOfOs()
        {
            // arrange
            var expected = "  1 2\r\nA O O\r\nB O O\r\n";
            var grid = new Grid(2);
            var sut = new TargetGrid(grid);
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "A1"), ShotResultEnum.Miss));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "A2"), ShotResultEnum.Miss));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "B1"), ShotResultEnum.Miss));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "B2"), ShotResultEnum.Miss));

            // act
            var result = sut.ToString();

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Given_TargetGridIsCreated_When_AllShotsAreMarkedAsHits_Then_ToStringConsistsOfXs()
        {
            // arrange
            var expected = "  1 2\r\nA X X\r\nB X X\r\n";
            var grid = new Grid(2);
            var sut = new TargetGrid(grid);
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "A1"), ShotResultEnum.Hit));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "A2"), ShotResultEnum.Hit));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "B1"), ShotResultEnum.Hit));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "B2"), ShotResultEnum.Hit));

            // act
            var result = sut.ToString();

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Given_TargetGridIsCreated_When_MultipleShotsAreMarked_Then_ToStringIsAsExpected()
        {
            // arrange
            var expected = "  1 2 3\r\nA O O X\r\nB     X\r\nC     X\r\n";
            var grid = new Grid(3);
            var sut = new TargetGrid(grid);
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "A1"), ShotResultEnum.Miss));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "A2"), ShotResultEnum.Miss));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "A3"), ShotResultEnum.Hit));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "B3"), ShotResultEnum.Hit));
            sut.MarkShot(new TargetGridMark(new Coordinate(10, "C3"), ShotResultEnum.Hit));

            // act
            var result = sut.ToString();

            // assert
            result.Should().Be(expected);
        }
    }
}
