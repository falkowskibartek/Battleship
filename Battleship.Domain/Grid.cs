using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleship.Tests")]
namespace Battleship.Domain
{
    public class Grid
    {
        internal Grid(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            Size = size;
            CreateHolesWithCoordinates();
        }

        public Grid()
        {
            Size = 10;
            CreateHolesWithCoordinates();
        }

        private void CreateHolesWithCoordinates()
        {
            Holes = new List<Coordinate>();
            for (var heightIndex = 0; heightIndex < Size; heightIndex++)
                for (int widthIndex = 0; widthIndex < Size; widthIndex++)
                {
                    Holes.Add(new Coordinate(Size, heightIndex, widthIndex));
                }
        }

        internal List<Coordinate> Holes { get; private set; }
        public int Size { get; }
    }
}
