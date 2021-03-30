using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Domain.OceanGrid
{
    public class OceanGrid
    {
        private readonly Grid _grid;

        internal OceanGrid(IEnumerable<Ship> ships) => Fleet = ships.ToList();

        public OceanGrid(Grid grid)
        {   
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));

            var positioner = new FleetPositioner(_grid.Size);
            var fleetCreators = new List<Func<int, Ship>> { Ship.CreateDestroyer, Ship.CreateDestroyer, Ship.CreateBattleship };
            Fleet = positioner.PositionFleet(fleetCreators);
        }

        internal List<Ship> Fleet { get; }

        public bool IsTheEntireFleetSunk => Fleet.All(ship => ship.IsSunk);

        public ShotResult TakeTheShot(Coordinate coordinate)
        {
            if (!coordinate.IsValid)
            {
                return ShotResult.Miss();
            }

            var shipThatOccupiesTheLocation = Fleet.FirstOrDefault(ship => ship.GetCoordinates().Any(shipCoordinate => shipCoordinate == coordinate));

            if (shipThatOccupiesTheLocation != null)
            {
                var shotResult = shipThatOccupiesTheLocation.TakeTheShot(coordinate);
                if (shipThatOccupiesTheLocation.IsSunk)
                {
                    return ShotResult.Sunk(shipThatOccupiesTheLocation.Name);
                }

                return ShotResult.Hit(shipThatOccupiesTheLocation.Name);
            }

            return ShotResult.Miss();
        }
    }
}
