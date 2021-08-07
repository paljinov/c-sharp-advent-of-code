using System;

namespace App.Tasks.Year2019.Day10
{
    public class MonitoringStation
    {
        public int CountNumberOfAsteroidsWhichCanBeDetectedFromMonitoringStation(bool[,] asteroidMap)
        {
            int numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation = 0;

            for (int x = 0; x < asteroidMap.GetLength(0); x++)
            {
                for (int y = 0; y < asteroidMap.GetLength(1); y++)
                {
                    // If there is an asteroid on this location
                    if (asteroidMap[x, y])
                    {
                        int detectableAsteroidsFromLocation =
                            CountDetectableAsteroidsFromLocation(new Location(x, y), asteroidMap);

                        numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation = Math.Max(
                            numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation, detectableAsteroidsFromLocation);
                    }
                }
            }

            return numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation;
        }

        private int CountDetectableAsteroidsFromLocation(Location monitoringLocation, bool[,] asteroidMap)
        {
            int detectableAsteroids = 0;

            for (int x = 0; x < asteroidMap.GetLength(0); x++)
            {
                for (int y = 0; y < asteroidMap.GetLength(1); y++)
                {
                    // If not monitoring location, asteroid exists on location and view is not blocked
                    if (!monitoringLocation.HasCoordinates(x, y) && asteroidMap[x, y]
                        && !IsViewBlocked(monitoringLocation, new Location(x, y), asteroidMap))
                    {
                        detectableAsteroids++;
                    }
                }
            }

            return detectableAsteroids;
        }

        private bool IsViewBlocked(Location monitoringLocation, Location asteroidLocation, bool[,] asteroidMap)
        {
            int fromX = Math.Min(monitoringLocation.X, asteroidLocation.X);
            int toX = Math.Max(monitoringLocation.X, asteroidLocation.X);
            int fromY = Math.Min(monitoringLocation.Y, asteroidLocation.Y);
            int toY = Math.Max(monitoringLocation.Y, asteroidLocation.Y);

            for (int x = fromX; x <= toX; x++)
            {
                for (int y = fromY; y <= toY; y++)
                {
                    // If not monitoring location, not asteroid location and asteroid exists
                    if (!monitoringLocation.HasCoordinates(x, y) && !asteroidLocation.HasCoordinates(x, y)
                        && asteroidMap[x, y])
                    {
                        // Vertically blocked
                        if (monitoringLocation.X == x && asteroidLocation.X == x)
                        {
                            return true;
                        }

                        // Horizontally blocked
                        if (monitoringLocation.Y == y && asteroidLocation.Y == y)
                        {
                            return true;
                        }

                        // Diagonally blocked
                        double monitoringToAsteroidLocationAngle = Math.Atan2(monitoringLocation.Y - asteroidLocation.Y,
                            monitoringLocation.X - asteroidLocation.X);
                        double monitoringToBlockerLocationAngle =
                            Math.Atan2(monitoringLocation.Y - y, monitoringLocation.X - x);
                        double blockerToAsteroidLocationAngle =
                            Math.Atan2(y - asteroidLocation.Y, x - asteroidLocation.X);

                        // If a point (x,y) is between two points drawn on a straight line angles must match
                        if (monitoringToAsteroidLocationAngle == monitoringToBlockerLocationAngle
                            && monitoringToAsteroidLocationAngle == blockerToAsteroidLocationAngle)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
