using Battleship.Domain;
using Battleship.Domain.OceanGrid;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests
{
    public class CoordinateTest
    {
        [Theory]
        [InlineData("A5", "A5")]
        [InlineData("B1", "B1")]
        [InlineData("J10", "J10")]
        [InlineData(" A1  ", "A1")]
        [InlineData(" a1  ", "A1")]
        [InlineData("a1  ", "A1")]
        [InlineData("   a1", "A1")]
        public void When_CoordinateIsValid_Then_ExpectedValueIsReturned(string value, string expected)
        {
            // arrange
            var sut = new Coordinate(10, value);

            // act
            var result = sut.Value;

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("AA5")]
        [InlineData("B 1")]
        [InlineData("1A")]
        [InlineData("  ")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("1")]
        [InlineData("AAA")]
        [InlineData("A10 BCDERG")]
        public void When_CoordinateIsInvalid_Then_EmptyValueIsReturned(string position)
        {
            // arrange
            var sut = new Coordinate(10, position);

            // act
            var result = sut;

            // assert
            result.Value.Should().BeEmpty();
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(10, "A11")]
        [InlineData(10, "K1")]
        [InlineData(10, "Z20")]
        [InlineData(1, "B2")]
        public void Given_CoordinateIsInvalidDueToGridSizeWhen_ConstructorWithStringIsCalled_Then_IsValidIsFalse(int gridSize, string position)
        {
            // arrange
            var sut = new Coordinate(gridSize, position);

            // act
            var result = sut;

            // assert
            result.Value.Should().BeEmpty();
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(10, 10, 5)]
        [InlineData(10, 5, 10)]
        [InlineData(10, 100, 100)]
        public void Given_CoordinateIsInvalidDueToGridSize_When_ConstructorWithPositionsIsCalled_Then_IsValidIsFalse(int gridSize, int height, int width)
        {
            // arrange
            var sut = new Coordinate(gridSize, height, width);

            // act
            var result = sut;

            // assert
            result.Value.Should().BeEmpty();
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("B5", Direction.Right, 10)]
        [InlineData("B5", Direction.Left, 5)]
        [InlineData("B5", Direction.Up, 2)]
        [InlineData("B5", Direction.Down, 15)]
        internal void Given_InvalidCoordinate_When_CreateTowardsEndsUpWithInvalidPosition_Then_IsValidReturnsFalse(string firstPosition, Direction direction, int howManyHoles)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, firstPosition);
            var sut = firstCoordinate.CreateTowards(direction, howManyHoles);

            // act
            var result = sut;

            // assert
            result.Value.Should().BeEmpty();
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("A1", "A1")]
        [InlineData(" C1", "c1 ")]
        [InlineData("b1", "b1")]
        [InlineData("A5", "a5")]
        [InlineData("J10", "j10")]
        public void Given_TwoDifferentCoordinatesWithSamePositions_When_Equals_Then_ReturnsTrue(string firstPosition, string secondPosition)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, firstPosition);
            var secondCoordinate = new Coordinate(10, secondPosition);

            // act
            var result = firstCoordinate.Equals(secondCoordinate);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("A1", "B1")]
        [InlineData("b1", "c1")]
        [InlineData("A5", "D5")]
        [InlineData("J10", "I10")]
        public void Given_TwoDifferentCoordinatesWithDifferentPositions_When_Equals_Then_ReturnsFalse(string firstPosition, string secondPosition)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, firstPosition);
            var secondCoordinate = new Coordinate(10, secondPosition);

            // act
            var result = firstCoordinate.Equals(secondCoordinate);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("A1", "B 1")]
        [InlineData("b1", "")]
        [InlineData("", "")]
        [InlineData("J10", "I 1 0")]
        public void Given_AtLeastOneOfCoordinatesIsInvalid_When_Equals_Then_ReturnsFalse(string firstPosition, string secondPosition)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, firstPosition);
            var secondCoordinate = new Coordinate(10, secondPosition);

            // act
            var result = firstCoordinate.Equals(secondCoordinate);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("A1", "A1")]
        [InlineData(" C1", "c1 ")]
        [InlineData("b1", "b1")]
        [InlineData("A5", "a5")]
        [InlineData("J10", "j10")]        
        public void Given_TwoDifferentCoordinatesWithSamePositions_When_EqualityOperatorUsed_Then_ReturnsTrue(string firstPosition, string secondPosition)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, firstPosition);
            var secondCoordinate = new Coordinate(10, secondPosition);

            // act
            var result = firstCoordinate == secondCoordinate;

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("A1", 0, 0)]
        [InlineData("B5", 1, 4)]
        [InlineData("C6", 2, 5)]
        [InlineData("J10", 9, 9)]
        public void Given_SamePositionsWithDifferentRepresentations_When_EqualityOperatorUsed_Then_ReturnsTrue(
            string position, int height, int width)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, position);
            var secondCoordinate = new Coordinate(10, height, width);

            // act
            var result = firstCoordinate == secondCoordinate;

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("A1", 0, 1)]
        [InlineData("B5", 1, 7)]
        [InlineData("C6", 3, 5)]
        [InlineData("J10", 8, 9)]
        public void Given_DifferentPositionsWithDifferentRepresentations_When_EqualityOperatorUsed_Then_ReturnsFalse(
            string position, int height, int width)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, position);
            var secondCoordinate = new Coordinate(10, height, width);

            // act
            var result = firstCoordinate == secondCoordinate;

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("B5", Direction.Right, 1, "B6")]
        [InlineData("B5", Direction.Left, 2, "B3")]
        [InlineData("B5", Direction.Up, 1, "A5")]
        [InlineData("B5", Direction.Down, 5, "G5")]
        internal void Given_CoordinateIsCreated_When_CreateTowards_Then_ReturnsMovedCoordinate(
            string firstPosition, Direction direction, int howManyHoles, string expectedPosition)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, firstPosition);
            var expectedCoordinate = new Coordinate(10, expectedPosition);

            // act
            var result = firstCoordinate.CreateTowards(direction, howManyHoles);

            // assert
            result.Should().Be(expectedCoordinate);
        }

        [Theory]
        [InlineData("B5", Direction.Right, 10)]
        [InlineData("B5", Direction.Left, 5)]
        [InlineData("B5", Direction.Up, 2)]
        [InlineData("B5", Direction.Down, 15)]
        internal void Given_CoordinateIsCreatedAndGridSizeIsTen_When_CreateTowardsEndsUpWithInvalidPosition_Then_IsValidReturnsFalse(
            string firstPosition, Direction direction, int howManyHoles)
        {
            // arrange
            var firstCoordinate = new Coordinate(10, firstPosition);
            var sut = firstCoordinate.CreateTowards(direction, howManyHoles);

            // act
            var result = sut.IsValid;

            // assert
            result.Should().BeFalse();
        }
    }
}
