namespace Battleship.Domain.OceanGrid
{
    internal class OceanGridHole
    {
        internal OceanGridHole(Coordinate coordinate) => Coordinate = coordinate;

        internal Coordinate Coordinate { get; }

        internal void MarkAsHit() => IsHit = true;

        internal bool IsHit { get; private set; }
    }
}
