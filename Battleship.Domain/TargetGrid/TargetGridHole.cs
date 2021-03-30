namespace Battleship.Domain.TargetGrid
{
    internal class TargetGridHole
    {
        private ITargetGridHoleStatus _status;

        internal TargetGridHole(Coordinate coordinate)
        {
            _status = new NotChecked();
            Coordinate = coordinate;
        }

        internal Coordinate Coordinate { get; }
        internal string Peg => _status.Peg;
        internal TargetGridHoleStatus Status => _status.Status;

        internal void Mark(ShotResultEnum shotResult)
        {
            if (shotResult == ShotResultEnum.Miss)
            {
                _status = new Missed();
            }

            if (shotResult == ShotResultEnum.Hit || shotResult == ShotResultEnum.Sunk)
            {
                _status = new Hit();
            }
        }
    }

    internal interface ITargetGridHoleStatus
    {
        string Peg { get; }

        TargetGridHoleStatus Status { get; }
    }

    internal class NotChecked : ITargetGridHoleStatus
    {
        public string Peg => " ";

        public TargetGridHoleStatus Status => TargetGridHoleStatus.NoPeg;
    }

    internal class Missed : ITargetGridHoleStatus
    {
        public string Peg => "O";

        public TargetGridHoleStatus Status => TargetGridHoleStatus.WhitePeg;
    }

    internal class Hit : ITargetGridHoleStatus
    {
        public string Peg => "X";

        public TargetGridHoleStatus Status => TargetGridHoleStatus.RedPeg;
    }

    internal enum TargetGridHoleStatus
    {
        NoPeg,
        WhitePeg,
        RedPeg
    }
}
