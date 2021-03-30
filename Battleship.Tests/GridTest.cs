using Battleship.Domain;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Battleship.Tests
{
    public class GridTest
    {
        [Fact]
        public void When_GridIsCreated_ThenOneHundredElementsIsAdded()
        {
            // arrange & act
            var sut = new Grid();

            // assert
            sut.Holes.Should().HaveCount(100);
        }

        [Fact]
        public void When_GridIsCreated_ThenFirstElementIsA1()
        {
            // arrange & act
            var sut = new Grid();

            // assert
            sut.Holes.First().Value.Should().Be("A1");
        }

        [Fact]
        public void When_GridIsCreated_ThenLastElementIsJ10()
        {
            // arrange & act
            var sut = new Grid();

            // assert
            sut.Holes.Last().Value.Should().Be("J10");
        }

        [Fact]
        public void Given_GridIsCreated_ThenValidizeIsReturned()
        {
            // arrange & act
            var sut = new Grid();

            // assert
            sut.Size.Should().Be(10);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Given_SizeIsInvalid_When_GridIsCreate_ThenArgumentOutOfRangeExceptionIsThrown(int size)
        {
            // arrange & act
            Action sut = () => new Grid(size);

            // assert
            sut.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}
