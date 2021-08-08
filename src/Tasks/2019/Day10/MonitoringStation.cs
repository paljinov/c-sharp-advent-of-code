using System;
using System.Collections.Generic;

namespace App.Tasks.Year2019.Day10
{
    public class MonitoringStation
    {
        private const int TWO_HUNDRED = 200;

        private const int MULTIPLY_X_BY = 100;

        public int CountNumberOfAsteroidsWhichCanBeDetectedFromMonitoringStation(bool[,] asteroidMap)
        {
            (_, int numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation) =
                GetMonitoringStationLocationAndNumberOfAsteroidsWhichCanBeDetected(asteroidMap);

            return numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation;
        }

        public int CalculateResultForTwoHundredthAsteroidToBeVaporized(bool[,] asteroidMap)
        {
            int result;
            Location twoHundredthAsteroidToBeVaporized = new Location(0, 0);

            (Location monitoringStation, _) =
                GetMonitoringStationLocationAndNumberOfAsteroidsWhichCanBeDetected(asteroidMap);

            List<Location> vaporized = new List<Location>();
            asteroidMap[monitoringStation.X, monitoringStation.Y] = false;

            int quadrant = 1;

            while (vaporized.Count < TWO_HUNDRED)
            {
                switch (quadrant)
                {
                    case 1:
                    default:
                        for (int x = monitoringStation.X; x < asteroidMap.GetLength(0) - 1; x++)
                        {
                            for (int y = 0; y <= monitoringStation.Y; y++)
                            {
                                // If there is an asteroid on this location
                                if (asteroidMap[x, y])
                                {
                                    // If view is not blocked
                                    if (!IsViewBlocked(monitoringStation, new Location(x, y), asteroidMap))
                                    {
                                        vaporized.Add(new Location(x, y));
                                        if (vaporized.Count == TWO_HUNDRED)
                                        {
                                            twoHundredthAsteroidToBeVaporized = new Location(x, y);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 2:
                        for (int x = asteroidMap.GetLength(0) - 1; x >= monitoringStation.X; x--)
                        {
                            for (int y = monitoringStation.Y; y < asteroidMap.GetLength(1) - 1; y++)
                            {
                                // If there is an asteroid on this location
                                if (asteroidMap[x, y])
                                {
                                    // If view is not blocked
                                    if (!IsViewBlocked(monitoringStation, new Location(x, y), asteroidMap))
                                    {
                                        vaporized.Add(new Location(x, y));
                                        if (vaporized.Count == TWO_HUNDRED)
                                        {
                                            twoHundredthAsteroidToBeVaporized = new Location(x, y);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 3:
                        for (int x = monitoringStation.X; x >= 0; x--)
                        {
                            for (int y = asteroidMap.GetLength(1) - 1; y >= monitoringStation.Y; y--)
                            {
                                // If there is an asteroid on this location
                                if (asteroidMap[x, y])
                                {
                                    // If view is not blocked
                                    if (!IsViewBlocked(monitoringStation, new Location(x, y), asteroidMap))
                                    {
                                        vaporized.Add(new Location(x, y));
                                        if (vaporized.Count == TWO_HUNDRED)
                                        {
                                            twoHundredthAsteroidToBeVaporized = new Location(x, y);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 4:
                        for (int x = 0; x < monitoringStation.X; x++)
                        {
                            for (int y = monitoringStation.Y; y >= 0; y--)
                            {
                                // If there is an asteroid on this location
                                if (asteroidMap[x, y])
                                {
                                    // If view is not blocked
                                    if (!IsViewBlocked(monitoringStation, new Location(x, y), asteroidMap))
                                    {
                                        vaporized.Add(new Location(x, y));
                                        if (vaporized.Count == TWO_HUNDRED)
                                        {
                                            twoHundredthAsteroidToBeVaporized = new Location(x, y);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }

                foreach (Location vaporizedAsteroid in vaporized)
                {
                    asteroidMap[vaporizedAsteroid.X, vaporizedAsteroid.Y] = false;
                }

                quadrant++;
                if (quadrant > 4)
                {
                    quadrant = 1;
                }
            }

            result = twoHundredthAsteroidToBeVaporized.X * MULTIPLY_X_BY + twoHundredthAsteroidToBeVaporized.Y;

            return result;
        }

        private (Location, int) GetMonitoringStationLocationAndNumberOfAsteroidsWhichCanBeDetected(bool[,] asteroidMap)
        {
            Location monitoringStation = new Location(0, 0);
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

                        if (detectableAsteroidsFromLocation > numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation)
                        {
                            monitoringStation = new Location(x, y);
                            numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation = detectableAsteroidsFromLocation;
                        }
                    }
                }
            }

            return (monitoringStation, numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation);
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
