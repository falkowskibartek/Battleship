using Battleship.Domain;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests
{
    public class HeightCoordinatesTest
    {
        [Theory]
        [InlineData(0, "A")]
        [InlineData(4, "E")]
        [InlineData(9, "J")]
        public void When_GetValue_Then_ValidValueIsReturned(int heightIndex, string expected)
        {
            // act
            var result = HeightCoordinates.GetValue(heightIndex);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData('A', 0)]
        [InlineData('E', 4)]
        [InlineData('J', 9)]
        public void When_IndexOfIsExecuted_Then_ValidIndexIsReturned(char value, int expected)
        {
            // act
            var result = HeightCoordinates.IndexOf(value);

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void When_Count_Then_TwentySixIsReturned()
        {
            //arrange
            int expected = 26;

            // act
            var result = HeightCoordinates.Count;

            // assert
            result.Should().Be(expected);
        }
    }
}
