using System.Collections.Generic;
using System.Linq;

namespace Battleship.Domain.OceanGrid
{
    internal class Ship
    {
        private const string DestroyerName = "Destroyer";
        private const string BattleshipName = "Battleship";
        private static ShipPositioner ShipPositioner = new ShipPositioner();

        private Ship(string name, IEnumerable<Coordinate> coordinates)
        {
            Name = name;
            Holes = coordinates.Select(coordinate => new OceanGridHole(coordinate)).ToList();
        }

        internal static Ship CreateDestroyer(IEnumerable<Coordinate> shipCoordinates)
        {
            return new Ship(DestroyerName, shipCoordinates);
        }

        internal static Ship CreateDestroyer(int gridSize)
        {
            const int destroyerLength = 4;
            var shipCoordinates = ShipPositioner.PositionShip(destroyerLength, gridSize);

            return new Ship(DestroyerName, shipCoordinates);
        }

        internal static Ship CreateBattleship(IEnumerable<Coordinate> shipCoordinates)
        {
            return new Ship(BattleshipName, shipCoordinates);
        }

        internal static Ship CreateBattleship(int gridSize)
        {
            const int destroyerLength = 5;
            var shipCoordinates = ShipPositioner.PositionShip(destroyerLength, gridSize);

            return new Ship(BattleshipName, shipCoordinates);
        }

        internal string Name { get; }
        internal List<OceanGridHole> Holes { get; }

        internal ShotResultEnum TakeTheShot(Coordinate shotCoordinate)
        {
            var hitElement = Holes.FirstOrDefault(element => element.Coordinate == shotCoordinate);
            if (hitElement != null)
            {
                hitElement.MarkAsHit();
                return ShotResultEnum.Hit;
            }

            return ShotResultEnum.Miss;
        }

        internal bool IsSunk => Holes.All(element => element.IsHit);

        internal List<Coordinate> GetCoordinates() => Holes.Select(element => element.Coordinate).ToList();
    }
}
