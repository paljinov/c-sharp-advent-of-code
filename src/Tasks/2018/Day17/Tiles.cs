using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day17
{
    public class Tiles
    {
        private readonly (int X, int Y) springOfWater = (500, 0);

        private readonly char[] waterTiles = new char[] { (char)TileType.FlowingWater, (char)TileType.SettledWater };

        private readonly char[] blockTiles = new char[] { (char)TileType.SettledWater, (char)TileType.Clay };

        private readonly char[] flowOrClayTiles = new char[] { (char)TileType.FlowingWater, (char)TileType.Clay };

        public int CountTilesTheWaterCanReach(ClayVein[] clayVeins)
        {
            Dictionary<(int Y, int X), char> map = GetMap(clayVeins);
            int minY = map.Select(m => m.Key.Y).Min();
            int maxY = map.Select(m => m.Key.Y).Max();
            int minX = map.Select(m => m.Key.X).Min();
            int maxX = map.Select(m => m.Key.X).Max();

            int tilesTheWaterCanReach = 0;
            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    if (waterTiles.Contains(map[(i, j)]))
                    {
                        tilesTheWaterCanReach += 1;
                    }
                }
            }

            return tilesTheWaterCanReach;
        }

        public int CountLeftWaterTiles(ClayVein[] clayVeins)
        {
            Dictionary<(int Y, int X), char> map = GetMap(clayVeins);
            int minY = map.Select(m => m.Key.Y).Min();
            int maxY = map.Select(m => m.Key.Y).Max();
            int minX = map.Select(m => m.Key.X).Min();
            int maxX = map.Select(m => m.Key.X).Max();

            int leftWaterTiles = 0;
            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    if (map[(i, j)] == (char)TileType.SettledWater)
                    {
                        leftWaterTiles += 1;
                    }
                }
            }

            return leftWaterTiles;
        }

        private Dictionary<(int, int), char> GetMap(ClayVein[] clayVeins)
        {
            Dictionary<(int Y, int X), char> map = InitializeScannedMap(clayVeins);
            int minY = map.Select(m => m.Key.Y).Min();
            int maxY = map.Select(m => m.Key.Y).Max();
            int minX = map.Select(m => m.Key.X).Min();
            int maxX = map.Select(m => m.Key.X).Max();

            Queue<(int Y, int X)> nextWaterFlowTiles = new Queue<(int Y, int X)>();
            nextWaterFlowTiles.Enqueue((springOfWater.Y + 1, springOfWater.X));

            // Water flows down
            while (nextWaterFlowTiles.Count > 0)
            {
                (int Y, int X) water = nextWaterFlowTiles.Dequeue();

                // If current tiles is sand
                if (map[(water.Y, water.X)] == (char)TileType.Sand)
                {
                    map[(water.Y, water.X)] = (char)TileType.FlowingWater;
                }

                // If map edge is reached
                if (water.Y == minY || water.Y == maxY || water.X == minX || water.X == maxX)
                {
                    continue;
                }

                // If next tile is sand
                if (map[(water.Y + 1, water.X)] == (char)TileType.Sand)
                {
                    nextWaterFlowTiles.Enqueue((water.Y + 1, water.X));
                    continue;
                }
                // If water cannot flow down
                else if (blockTiles.Contains(map[(water.Y + 1, water.X)]))
                {
                    // Flow left
                    if (map[(water.Y, water.X - 1)] == (char)TileType.Sand)
                    {
                        nextWaterFlowTiles.Enqueue((water.Y, water.X - 1));
                    }

                    // Flow right
                    if (map[(water.Y, water.X + 1)] == (char)TileType.Sand)
                    {
                        nextWaterFlowTiles.Enqueue((water.Y, water.X + 1));
                    }

                    // If left and right is flowing water or clay
                    if (flowOrClayTiles.Contains(map[(water.Y, water.X - 1)])
                        && flowOrClayTiles.Contains(map[(water.Y, water.X + 1)]))
                    {
                        // Go left while water tile
                        int leftX = water.X;
                        while (leftX > minX && waterTiles.Contains(map[(water.Y, leftX - 1)]))
                        {
                            leftX -= 1;
                        }
                        // If left boundary is not clay
                        if (leftX <= minX || map[(water.Y, leftX - 1)] != (char)TileType.Clay)
                        {
                            continue;
                        }

                        // Go right while water tile
                        int rightX = water.X;
                        while (rightX < maxX && waterTiles.Contains(map[(water.Y, rightX + 1)]))
                        {
                            rightX += 1;
                        }
                        // If right boundary is not clay
                        if (rightX >= maxX || map[(water.Y, rightX + 1)] != (char)TileType.Clay)
                        {
                            continue;
                        }

                        map[(water.Y, water.X)] = (char)TileType.SettledWater;
                        // If tile down is flowing water
                        if (map[(water.Y - 1, water.X)] == (char)TileType.FlowingWater)
                        {
                            nextWaterFlowTiles.Enqueue((water.Y - 1, water.X));
                        }

                        leftX = water.X;
                        while (leftX > minX && waterTiles.Contains(map[(water.Y, leftX - 1)]))
                        {
                            map[(water.Y, leftX - 1)] = (char)TileType.SettledWater;
                            leftX -= 1;

                            if (map[(water.Y - 1, leftX)] == (char)TileType.FlowingWater)
                            {
                                nextWaterFlowTiles.Enqueue((water.Y - 1, leftX));
                            }
                        }

                        rightX = water.X;
                        while (rightX < maxX && waterTiles.Contains(map[(water.Y, rightX + 1)]))
                        {
                            map[(water.Y, rightX + 1)] = (char)TileType.SettledWater;
                            rightX += 1;

                            if (map[(water.Y - 1, rightX)] == (char)TileType.FlowingWater)
                            {
                                nextWaterFlowTiles.Enqueue((water.Y - 1, rightX));
                            }
                        }
                    }
                }
            }

            return map;
        }

        private Dictionary<(int Y, int X), char> InitializeScannedMap(ClayVein[] clayVeins)
        {
            int minX = clayVeins.Select(cv => cv.XFrom).Append(springOfWater.X).Min();
            int maxX = clayVeins.Select(cv => cv.XTo).Append(springOfWater.X).Max();
            int minY = clayVeins.Select(cv => cv.YFrom).Append(springOfWater.Y).Min();
            int maxY = clayVeins.Select(cv => cv.YTo).Append(springOfWater.Y).Max();

            Dictionary<(int Y, int X), char> scannedMap = new Dictionary<(int Y, int X), char>();
            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    scannedMap[(i, j)] = (char)TileType.Sand;
                }
            }

            scannedMap[(springOfWater.Y, springOfWater.X)] = (char)TileType.WaterSpring;
            foreach (ClayVein clayVein in clayVeins)
            {
                // If X is fixed
                if (clayVein.XFrom == clayVein.XTo)
                {
                    for (int i = clayVein.YFrom; i <= clayVein.YTo; i++)
                    {
                        scannedMap[(i, clayVein.XFrom)] = (char)TileType.Clay;
                    }
                }
                // If Y is fixed
                else
                {
                    for (int j = clayVein.XFrom; j <= clayVein.XTo; j++)
                    {
                        scannedMap[(clayVein.YFrom, j)] = (char)TileType.Clay;
                    }
                }
            }

            return scannedMap;
        }
    }
}
