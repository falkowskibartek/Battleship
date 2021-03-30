namespace Battleship.Domain
{
    public class ShotResult
    {
        internal static ShotResult Miss() => new ShotResult { Result = ShotResultEnum.Miss };
        internal static ShotResult Hit(string shipName) => new ShotResult { Result = ShotResultEnum.Hit, ShipName = shipName };
        internal static ShotResult Sunk(string shipName) => new ShotResult { Result = ShotResultEnum.Sunk, ShipName = shipName };

        public ShotResultEnum Result { get; private set; }
        public string ShipName { get; private set; }
    }

    public enum ShotResultEnum
    {
        Hit,
        Miss,
        Sunk
    }
}
