using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Domain.OceanGrid
{
    internal class FleetPositioner
    {
        private readonly int _gridSize;
        private readonly List<Ship> _ships = new List<Ship>();

        public FleetPositioner(int gridSize) => _gridSize = gridSize;

        internal List<Ship> PositionFleet(IEnumerable<Func<int, Ship>> shipCreators)
        {
            shipCreators.ToList().ForEach(creator => AddShip(creator));

            return _ships;
        }

        private void AddShip(Func<int, Ship> shipCreator)
        {
            Ship result = null;
            while (result == null)
            {
                var ship = shipCreator(_gridSize);
                if (!AreShipCoordinatesAlreadyTaken(ship))
                {
                    result = ship;
                }
            }

            _ships.Add(result);
        }

        private bool AreShipCoordinatesAlreadyTaken(Ship newShip)
        {
            var alreadyTakenCoordinates =
                _ships.SelectMany(ship => ship.GetCoordinates());
            var anyOfNewCoordinatesIsAlreadyTaken =
                newShip.GetCoordinates().Intersect(alreadyTakenCoordinates).Any();

            return anyOfNewCoordinatesIsAlreadyTaken;
        }
    }
}
