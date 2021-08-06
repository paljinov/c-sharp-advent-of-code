using System;

namespace App.Tasks.Year2019.Day10
{
    public class MonitoringStation
    {
        public int CountNumberOfAsteroidsWhichCanBeDetectedFromMonitoringStation(bool[,] asteroidMap)
        {
            (int x, int y) monitoringStationLocation = (0, 0);
            int numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation = 0;

            for (int x = 0; x < asteroidMap.GetLength(0); x++)
            {
                for (int y = 0; y < asteroidMap.GetLength(1); y++)
                {
                    if (asteroidMap[x, y])
                    {
                        int detectableAsteroidsFromLocation = CountDetectableAsteroidsFromLocation(x, y, asteroidMap);
                        if (detectableAsteroidsFromLocation > numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation)
                        {
                            numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation = detectableAsteroidsFromLocation;
                            monitoringStationLocation = (x, y);
                        }
                    }
                }
            }

            return numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation;
        }

        private int CountDetectableAsteroidsFromLocation(int i, int j, bool[,] asteroidMap)
        {
            int detectableAsteroids = 0;

            for (int x = 0; x < asteroidMap.GetLength(0); x++)
            {
                for (int y = 0; y < asteroidMap.GetLength(1); y++)
                {
                    if ((x != i || y != j) && asteroidMap[x, y] && !IsViewBlocked(i, j, x, y, asteroidMap))
                    {
                        detectableAsteroids++;
                    }
                }
            }

            return detectableAsteroids;
        }

        private bool IsViewBlocked(int i, int j, int k, int h, bool[,] asteroidMap)
        {
            int fromX = Math.Min(i, k);
            int toX = Math.Max(i, k);
            int fromY = Math.Min(j, h);
            int toY = Math.Max(j, h);

            for (int x = fromX; x <= toX; x++)
            {
                for (int y = fromY; y <= toY; y++)
                {
                    if ((x != i || y != j) && (x != k || y != h) && asteroidMap[x, y])
                    {
                        // Vertically blocked
                        if (i == x && k == x)
                        {
                            return true;
                        }

                        // Horizontally blocked
                        if (j == y && h == y)
                        {
                            return true;
                        }

                        if ((y - fromY) > 0 && (toY - y) > 0 && (x - fromX) > 0 && (toX - x) > 0)
                        {
                            var slope1 = (y - fromY) / (x - fromX);
                            var slope2 = (toY - y) / (toX - x);
                            // Diagonally blocked
                            if (slope1 == slope2)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
