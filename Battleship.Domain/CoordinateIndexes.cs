namespace Battleship.Domain
{
    internal class CoordinateIndexes
    {
        private static int WidthStartValue = 1;
        private readonly int _gridSize;

        internal CoordinateIndexes() => HeightIndex = WidthIndex = -1;

        internal CoordinateIndexes(int gridSize, int heightIndex, int widthIndex) : this()
        {
            _gridSize = gridSize;

            if (AreValid(heightIndex, widthIndex))
            {
                HeightIndex = heightIndex;
                WidthIndex = widthIndex;
            }
        }

        internal int HeightIndex { get; }
        internal int WidthIndex { get; }

        internal bool AreValid() => AreValid(HeightIndex, WidthIndex);

        private bool AreValid(int heightIndex, int widthIndex) =>
            heightIndex >= 0 && widthIndex >= 0 && heightIndex < HeightCoordinates.Count && heightIndex < _gridSize && widthIndex < _gridSize;

        internal CoordinateValue CreateValue()
        {
            if (!AreValid())
            {
                return new CoordinateValue();
            }

            string value = GetValueFromIndexes();

            return new CoordinateValue(_gridSize, value);
        }

        private string GetValueFromIndexes()
        {
            var heightValue = HeightCoordinates.GetValue(HeightIndex);
            var widthValue = WidthStartValue + WidthIndex;
            var value = $"{heightValue}{widthValue}";

            return value;
        }
    }
}