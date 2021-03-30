namespace Battleship.Domain.TargetGrid
{
    public class TargetGridMark
    {
        public TargetGridMark(Coordinate coordinate, ShotResultEnum shotResult)
        {
            Coordinate = coordinate;
            ShotResult = shotResult;
        }

        public Coordinate Coordinate { get; }
        public ShotResultEnum ShotResult { get; }
    }
}
