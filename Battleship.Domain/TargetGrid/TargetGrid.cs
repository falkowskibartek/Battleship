using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Domain.TargetGrid
{
    public class TargetGrid
    {
        private readonly Grid _grid;

        public TargetGrid(Grid grid)
        {
            _grid = grid;
            Holes = _grid.Holes.Select(coordinate => new TargetGridHole(coordinate)).ToList();
        }

        internal List<TargetGridHole> Holes { get; private set; }

        public void MarkShot(TargetGridMark mark)
        {
            if (!mark.Coordinate.IsValid)
            {
                return;
            }

            var shotHole = Holes.FirstOrDefault(square => square.Coordinate == mark.Coordinate);
            if (shotHole == null)
            {
                return;
            }

            shotHole.Mark(mark.ShotResult);
        }

        public override string ToString()
        {
            var battleship = new StringBuilder();
            AddWidthHeaders(battleship);

            for (var heightIndex = 0; heightIndex < _grid.Size; heightIndex++)
            {
                AddHeightHeader(battleship, heightIndex);

                for (int widthIndex = 0; widthIndex < _grid.Size; widthIndex++)
                {
                    AddPeg(battleship, heightIndex, widthIndex);
                }
                battleship.AppendLine();
            }

            return battleship.ToString();
        }

        private void AddWidthHeaders(StringBuilder battleship)
        {
            battleship.Append(" ");
            for (int widthIndex = 1; widthIndex <= _grid.Size; widthIndex++)
            {
                battleship.Append(" ");
                battleship.Append(widthIndex);
            }
            battleship.AppendLine();
        }

        private void AddHeightHeader(StringBuilder battleship, int heightIndex)
        {
            var heightCoordinate = HeightCoordinates.GetValue(heightIndex);
            battleship.Append(heightCoordinate);
        }

        private void AddPeg(StringBuilder battleship, int heightIndex, int widthIndex)
        {            
            var coordinate = new Coordinate(_grid.Size, heightIndex, widthIndex);
            var hole = Holes.FirstOrDefault(square => square.Coordinate == coordinate);
            battleship.Append(" ");
            battleship.Append(hole.Peg);
        }
    }
}
