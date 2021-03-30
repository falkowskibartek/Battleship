using System.Text.RegularExpressions;

namespace Battleship.Domain
{
    internal class CoordinateValue
    {
        private const string ValidCoordinateRegex = @"^\b([a-zA-Z]{1}\d{1,2})$\b";
        private static int WidthStartValue = 1;

        private readonly int _gridSize;

        internal CoordinateValue() => Value = string.Empty;

        internal CoordinateValue(int gridSize, string value) : this()
        {
            _gridSize = gridSize;
            Value = string.Empty;

            if (IsValid(value))
            {
                Value = value.ToUpperInvariant().Trim();
            }
        }

        internal string Value { get; private set; }

        internal bool IsValid() => IsValid(Value);
        private bool IsValid(string value) => !string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value.Trim(), ValidCoordinateRegex);

        internal CoordinateIndexes CreateIndexes()
        {
            if (!IsValid())
            {
                return new CoordinateIndexes();
            }

            int heightIndex = GetHeightIndexFromValue();
            int widthIndex = GetWidthIndexFromValue();

            return new CoordinateIndexes(_gridSize, heightIndex, widthIndex);
        }

        private int GetHeightIndexFromValue()
        {
            var heightValue = Value[0];
            var heightIndex = HeightCoordinates.IndexOf(heightValue);

            return heightIndex;
        }

        private int GetWidthIndexFromValue()
        {
            var widthValueLength = Value.Length - 1;
            var widthValue = Value.Substring(1, widthValueLength);
            var widthIndex = int.Parse(widthValue) - WidthStartValue;

            return widthIndex;
        }
    }
}