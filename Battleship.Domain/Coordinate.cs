using Battleship.Domain.OceanGrid;
using System;
using System.Collections.Generic;

namespace Battleship.Domain
{
    public class Coordinate : IEquatable<Coordinate>
    {
        private readonly int _gridSize;
        private readonly CoordinateIndexes _indexes;
        private readonly CoordinateValue _value;

        private Coordinate()
        {
            _value = new CoordinateValue();
            _indexes = new CoordinateIndexes();
        }

        public Coordinate(int gridSize, string value) : this()
        {
            _gridSize = gridSize;

            var coordinateValue = new CoordinateValue(gridSize, value);
            var coordinateIndexes = coordinateValue.CreateIndexes();

            if (AreValueAndIndexesValid(coordinateValue, coordinateIndexes))
            {
                _value = coordinateValue;
                _indexes = coordinateIndexes;
            }
        }

        public Coordinate(int gridSize, int heightIndex, int widthIndex) : this()
        {
            _gridSize = gridSize;

            var coordinateIndexes = new CoordinateIndexes(gridSize, heightIndex, widthIndex);
            var coordinateValue = coordinateIndexes.CreateValue();

            if (AreValueAndIndexesValid(coordinateValue, coordinateIndexes))
            {
                _indexes = coordinateIndexes;
                _value = coordinateValue;
            }
        }

        private bool AreValueAndIndexesValid(CoordinateValue value, CoordinateIndexes indexes) => value.IsValid() && indexes.AreValid();

        public string Value => _value.Value;

        public bool IsValid => !string.IsNullOrWhiteSpace(Value);

        internal Coordinate CreateTowards(Direction direction, int howManyHoles)
        {
            var creators = new Dictionary<Direction, Func<Coordinate>>
            {
                { Direction.Right, () =>  new Coordinate(_gridSize, _indexes.HeightIndex, _indexes.WidthIndex + howManyHoles) },
                { Direction.Down, () => new Coordinate(_gridSize, _indexes.HeightIndex + howManyHoles, _indexes.WidthIndex) },
                { Direction.Left, () => new Coordinate(_gridSize, _indexes.HeightIndex, _indexes.WidthIndex - howManyHoles) },
                { Direction.Up, () => new Coordinate(_gridSize, _indexes.HeightIndex - howManyHoles, _indexes.WidthIndex) }
            };

            var result = creators[direction]();
            return result;
        }

        #region Equality
        public override bool Equals(object obj) => Equals(obj as Coordinate);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(Coordinate other)
        {
            if (other is null)
            {
                return false;
            }

            if (!IsValid || !other.IsValid)
            {
                return false;
            }

            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool operator ==(Coordinate a, Coordinate b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !(a == b);
        }
        #endregion
    }
}