using Battleship.Domain;
using Battleship.Domain.OceanGrid;
using Battleship.Domain.TargetGrid;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid();
            var targetGrid = new TargetGrid(grid);
            var oceanGrid = new OceanGrid(grid);

            ShotResult shotResult = null;
            while (!oceanGrid.IsTheEntireFleetSunk)
            {
                WriteTargetGrid(targetGrid);
                WriteShotResult(shotResult);
                shotResult = HandleShot(grid, targetGrid, oceanGrid);

                Console.Clear();
            }

            Console.WriteLine("The entire fleet is sunk. You won the game!");
        }

        private static void WriteTargetGrid(TargetGrid targetGrid)
        {
            Console.WriteLine("Legend:\n\tO - Miss\n\tX - Hit");
            Console.WriteLine();

            var visualisedTargetGrid = targetGrid.ToString();
            Console.WriteLine(visualisedTargetGrid);
            Console.WriteLine();
        }

        private static void WriteShotResult(ShotResult shotResult)
        {
            if (shotResult != null)
            {
                if (shotResult.Result == ShotResultEnum.Miss)
                {
                    Console.WriteLine("Last shot result: Miss.");
                }

                if (shotResult.Result == ShotResultEnum.Hit)
                {
                    Console.WriteLine($"Last shot result: Hit. {shotResult.ShipName}.");
                }

                if (shotResult.Result == ShotResultEnum.Sunk)
                {
                    Console.WriteLine($"Last shot result: Sunk. {shotResult.ShipName}.");
                }
                Console.WriteLine();
            }
        }

        private static ShotResult HandleShot(Grid grid, TargetGrid targetGrid, OceanGrid oceanGrid)
        {
            Console.WriteLine("Call out your shot!");

            var shotCoordinate = new Coordinate(grid.Size, Console.ReadLine());
            var shotResult = oceanGrid.TakeTheShot(shotCoordinate);
            targetGrid.MarkShot(new TargetGridMark(shotCoordinate, shotResult.Result));

            return shotResult;
        }
    }
}
