using System.Linq;

namespace Battleship.Domain
{
    internal static class HeightCoordinates
    {
        private static char[] GridHeightPossibleCoordinates = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        internal static string GetValue(int heightIndex)
        {
            if (heightIndex < 0 || heightIndex >= GridHeightPossibleCoordinates.Count())
            {
                return "";
            }

            return GridHeightPossibleCoordinates[heightIndex].ToString();
        }

        internal static int Count => GridHeightPossibleCoordinates.Count();

        internal static int IndexOf(char value) => GridHeightPossibleCoordinates.ToList().IndexOf(value);
    }
}
