using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day17
{
    public class Tiles
    {
        private readonly (int X, int Y) springOfWater = (500, 0);

        private readonly TileType[] waterTiles = new TileType[] { TileType.FlowingWater, TileType.SettledWater };

        private readonly TileType[] blockTiles = new TileType[] { TileType.SettledWater, TileType.Clay };

        private readonly TileType[] flowOrClayTiles = new TileType[] { TileType.FlowingWater, TileType.Clay };

        public int CountTilesTheWaterCanReach(ClayVein[] clayVeins)
        {
            Dictionary<(int Y, int X), TileType> map = GetMap(clayVeins);
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
            Dictionary<(int Y, int X), TileType> map = GetMap(clayVeins);
            int minY = map.Select(m => m.Key.Y).Min();
            int maxY = map.Select(m => m.Key.Y).Max();
            int minX = map.Select(m => m.Key.X).Min();
            int maxX = map.Select(m => m.Key.X).Max();

            int leftWaterTiles = 0;
            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    if (map[(i, j)] == TileType.SettledWater)
                    {
                        leftWaterTiles += 1;
                    }
                }
            }

            return leftWaterTiles;
        }

        private Dictionary<(int, int), TileType> GetMap(ClayVein[] clayVeins)
        {
            Dictionary<(int Y, int X), TileType> map = InitializeScannedMap(clayVeins);
            int minY = map.Select(m => m.Key.Y).Min();
            int maxY = map.Select(m => m.Key.Y).Max();
            int minX = map.Select(m => m.Key.X).Min();
            int maxX = map.Select(m => m.Key.X).Max();

            Queue<(int Y, int X)> nextFlowingWaterTiles = new Queue<(int Y, int X)>();
            // First flowing water location is at map minimum Y coordinate, and spring X coordinate
            nextFlowingWaterTiles.Enqueue((minY, springOfWater.X));

            // Water flows down
            while (nextFlowingWaterTiles.Count > 0)
            {
                (int Y, int X) flowingWater = nextFlowingWaterTiles.Dequeue();

                // If tile is sand
                if (map[(flowingWater.Y, flowingWater.X)] == TileType.Sand)
                {
                    map[(flowingWater.Y, flowingWater.X)] = TileType.FlowingWater;
                }

                // To prevent counting forever, ignore tiles with a y coordinate smaller than
                // the smallest y coordinate in your scan data or larger than the largest one
                if (flowingWater.Y < minY || flowingWater.Y >= maxY)
                {
                    continue;
                }

                // If bottom tile is sand
                if (map[(flowingWater.Y + 1, flowingWater.X)] == TileType.Sand)
                {
                    nextFlowingWaterTiles.Enqueue((flowingWater.Y + 1, flowingWater.X));
                    continue;
                }

                // If water cannot flow down
                if (blockTiles.Contains(map[(flowingWater.Y + 1, flowingWater.X)]))
                {
                    // Flow left
                    if (map[(flowingWater.Y, flowingWater.X - 1)] == TileType.Sand)
                    {
                        nextFlowingWaterTiles.Enqueue((flowingWater.Y, flowingWater.X - 1));
                    }

                    // Flow right
                    if (map[(flowingWater.Y, flowingWater.X + 1)] == TileType.Sand)
                    {
                        nextFlowingWaterTiles.Enqueue((flowingWater.Y, flowingWater.X + 1));
                    }

                    // If left or right is flowing water or clay
                    if (flowOrClayTiles.Contains(map[(flowingWater.Y, flowingWater.X - 1)])
                        || flowOrClayTiles.Contains(map[(flowingWater.Y, flowingWater.X + 1)]))
                    {
                        if (!IsReservoir(flowingWater, map, minX, maxX))
                        {
                            continue;
                        }

                        SetReservoirTileAsSettledWaterAndCheckAbove(flowingWater, map, nextFlowingWaterTiles);

                        int leftX = flowingWater.X;
                        // Set left tiles as settled water
                        while (leftX > minX && map[(flowingWater.Y, leftX - 1)] == TileType.FlowingWater)
                        {
                            leftX -= 1;
                            (int Y, int X) leftFlowingWater = (flowingWater.Y, leftX);
                            SetReservoirTileAsSettledWaterAndCheckAbove(leftFlowingWater, map, nextFlowingWaterTiles);
                        }

                        int rightX = flowingWater.X;
                        // Set right tiles as settled water
                        while (rightX < maxX && map[(flowingWater.Y, rightX + 1)] == TileType.FlowingWater)
                        {
                            rightX += 1;
                            (int Y, int X) rightFlowingWater = (flowingWater.Y, rightX);
                            SetReservoirTileAsSettledWaterAndCheckAbove(rightFlowingWater, map, nextFlowingWaterTiles);
                        }
                    }
                }
            }

            return map;
        }

        private Dictionary<(int Y, int X), TileType> InitializeScannedMap(ClayVein[] clayVeins)
        {
            // If reservoir is at the edge water can overflow
            int minX = clayVeins.Select(cv => cv.XFrom).Min() - 1;
            int maxX = clayVeins.Select(cv => cv.XTo).Max() + 1;
            int minY = clayVeins.Select(cv => cv.YFrom).Min();
            int maxY = clayVeins.Select(cv => cv.YTo).Max();

            Dictionary<(int Y, int X), TileType> scannedMap = new Dictionary<(int Y, int X), TileType>();
            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    scannedMap[(i, j)] = TileType.Sand;
                }
            }

            foreach (ClayVein clayVein in clayVeins)
            {
                // If X is fixed
                if (clayVein.XFrom == clayVein.XTo)
                {
                    for (int i = clayVein.YFrom; i <= clayVein.YTo; i++)
                    {
                        scannedMap[(i, clayVein.XFrom)] = TileType.Clay;
                    }
                }
                // If Y is fixed
                else
                {
                    for (int j = clayVein.XFrom; j <= clayVein.XTo; j++)
                    {
                        scannedMap[(clayVein.YFrom, j)] = TileType.Clay;
                    }
                }
            }

            return scannedMap;
        }

        private bool IsReservoir(
            (int Y, int X) flowingWater,
            Dictionary<(int Y, int X), TileType> map,
            int minX,
            int maxX
        )
        {
            // Go left while water tile
            int leftX = flowingWater.X;
            while (leftX > minX && waterTiles.Contains(map[(flowingWater.Y, leftX - 1)]))
            {
                leftX -= 1;
            }
            // If water left boundary is not clay
            if (leftX <= minX || map[(flowingWater.Y, leftX - 1)] != TileType.Clay)
            {
                return false;
            }

            // Go right while water tile
            int rightX = flowingWater.X;
            while (rightX < maxX && waterTiles.Contains(map[(flowingWater.Y, rightX + 1)]))
            {
                rightX += 1;
            }
            // If water right boundary is not clay
            if (rightX >= maxX || map[(flowingWater.Y, rightX + 1)] != TileType.Clay)
            {
                return false;
            }

            return true;
        }

        private void SetReservoirTileAsSettledWaterAndCheckAbove(
            (int Y, int X) flowingWater,
            Dictionary<(int Y, int X), TileType> map,
            Queue<(int Y, int X)> nextFlowingWaterTiles)
        {
            // If water is in reservoir and cannot flow down, it is settled water
            map[(flowingWater.Y, flowingWater.X)] = TileType.SettledWater;
            // If tile above is flowing water
            if (map[(flowingWater.Y - 1, flowingWater.X)] == TileType.FlowingWater)
            {
                nextFlowingWaterTiles.Enqueue((flowingWater.Y - 1, flowingWater.X));
            }
        }
    }
}
