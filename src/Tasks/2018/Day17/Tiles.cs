using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day17
{
    public class Tiles
    {
        private readonly (int X, int Y) springOfWater = (500, 0);

        public int CountTilesTheWaterCanReach(ClayVein[] clayVeins)
        {
            Dictionary<(int, int), char> map = GetMap(clayVeins);

            int tilesTheWaterCanReach = 0;

            return tilesTheWaterCanReach;
        }

        public int CountLeftWaterTiles(ClayVein[] clayVeins)
        {
            Dictionary<(int, int), char> map = GetMap(clayVeins);

            int leftWaterTiles = 0;

            return leftWaterTiles;
        }

        private Dictionary<(int, int), char> GetMap(ClayVein[] clayVeins)
        {
            int minY = clayVeins.Select(cv => cv.XFrom).Min();
            int maxY = clayVeins.Select(cv => cv.XTo).Max();
            int minX = clayVeins.Select(cv => cv.YFrom).Min();
            int maxX = clayVeins.Select(cv => cv.YTo).Max();

            Dictionary<(int, int), char> map = InitializeScannedMap(clayVeins);

            return map;
        }

        private Dictionary<(int, int), char> InitializeScannedMap(ClayVein[] clayVeins)
        {
            int minX = clayVeins.Select(cv => cv.XFrom).Min() - 1;
            int maxX = clayVeins.Select(cv => cv.XTo).Max() + 1;
            int minY = clayVeins.Select(cv => cv.YFrom).Min();
            int maxY = clayVeins.Select(cv => cv.YTo).Max();

            Dictionary<(int, int), char> scannedMap = new Dictionary<(int, int), char>();
            for (int i = minX; i <= maxY; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    scannedMap[(i, j)] = (char)TileType.Sand;
                }
            }

            scannedMap[(springOfWater.X, springOfWater.Y)] = (char)TileType.WaterSpring;
            foreach (ClayVein clayVein in clayVeins)
            {
                if (clayVein.XFrom == clayVein.XTo)
                {
                    for (int i = clayVein.YFrom; i <= clayVein.YTo; i++)
                    {
                        scannedMap[(clayVein.XFrom, i)] = (char)TileType.Clay;
                    }
                }
                else
                {
                    for (int i = clayVein.XFrom; i <= clayVein.XTo; i++)
                    {
                        scannedMap[(i, clayVein.YFrom)] = (char)TileType.Clay;
                    }
                }
            }

            return scannedMap;
        }
    }
}
