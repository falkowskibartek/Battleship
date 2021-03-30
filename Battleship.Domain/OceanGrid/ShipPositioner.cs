using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Domain.OceanGrid
{
    internal class ShipPositioner
    {
        private const int NumberOfAllDirectionOnGrid = 4;
        private static Random Random = new Random();

        internal List<Coordinate> PositionShip(int shipSize, int gridSize)
        {
            if (shipSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shipSize));
            }
            if (gridSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(gridSize));
            }
            if (shipSize > gridSize)
            {
                throw new ArgumentException("Ship size is greater than grid size.", nameof(gridSize));
            }

            var result = new List<Coordinate>();
            while (!result.Any())
            {
                var firstCoordinate = CreateFirstRandomCoordinate(gridSize);
                var direction = CreateRandomDirection();
                var coordinates = CreateCoordinatesTowardsDirection(shipSize, firstCoordinate, direction);

                if (coordinates.All(coordinate => coordinate.IsValid))
                {
                    result.AddRange(coordinates);
                }
            }

            return result;
        }

        private static Coordinate CreateFirstRandomCoordinate(int gridSize)
        {
            var heightIndex = Random.Next(gridSize);
            var widthIndex = Random.Next(gridSize);

            return new Coordinate(gridSize, heightIndex, widthIndex);
        }

        private static Direction CreateRandomDirection() => 
            (Direction)Random.Next(NumberOfAllDirectionOnGrid);

        private static IEnumerable<Coordinate> CreateCoordinatesTowardsDirection(int shipSize, Coordinate firstCoordinate, Direction direction) =>
            Enumerable.Range(1, shipSize)
                .Select(howManyHoles =>
                    firstCoordinate.CreateTowards(direction, howManyHoles));

    }
}
