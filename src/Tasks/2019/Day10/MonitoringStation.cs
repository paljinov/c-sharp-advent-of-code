using System;
using System.Collections.Generic;
using System.Linq;

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
            Location twoHundredthAsteroidToBeVaporized = new Location(0, 0);

            (Location monitoringStation, _) =
                GetMonitoringStationLocationAndNumberOfAsteroidsWhichCanBeDetected(asteroidMap);

            List<(double, Location)> angles = GetAnglesFromMonitoringStationToAsteroids(monitoringStation, asteroidMap);

            List<Location> vaporized = new List<Location>();
            asteroidMap[monitoringStation.X, monitoringStation.Y] = false;

            while (vaporized.Count < TWO_HUNDRED)
            {
                foreach ((_, Location location) in angles)
                {
                    // If there is an asteroid on this location and view is not blocked
                    if (asteroidMap[location.X, location.Y] && !IsViewBlocked(monitoringStation, location, asteroidMap))
                    {
                        vaporized.Add(location);
                        if (vaporized.Count == TWO_HUNDRED)
                        {
                            twoHundredthAsteroidToBeVaporized = location;
                            break;
                        }
                    }
                }

                // Removing vaporized asteroids after full cycle
                foreach (Location vaporizedAsteroid in vaporized)
                {
                    asteroidMap[vaporizedAsteroid.X, vaporizedAsteroid.Y] = false;
                }
            }

            int result = twoHundredthAsteroidToBeVaporized.X * MULTIPLY_X_BY + twoHundredthAsteroidToBeVaporized.Y;

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

        private List<(double, Location)> GetAnglesFromMonitoringStationToAsteroids(
            Location monitoringStation,
            bool[,] asteroidMap
        )
        {
            List<(double Angle, Location)> angles = new List<(double, Location)>();

            for (int x = 0; x < asteroidMap.GetLength(0); x++)
            {
                for (int y = 0; y < asteroidMap.GetLength(1); y++)
                {
                    // If there is an asteroid on this location
                    if (asteroidMap[x, y])
                    {
                        double angle = Math.Atan2(monitoringStation.Y - y, monitoringStation.X - x);
                        angles.Add((angle, new Location(x, y)));
                    }
                }
            }

            // Get quandrants starting from laser pointing up and rotate clockwise
            List<(double Angle, Location)> firstQuadrantClockwise =
                angles.Where(a => a.Angle >= Math.Atan2(1, 0)).OrderBy(a => a.Angle).ToList();
            List<(double Angle, Location)> secondQuadrantClockwise =
                angles.Where(a => a.Angle <= Math.Atan2(-1, 0)).OrderBy(a => a.Angle).ToList();
            List<(double Angle, Location)> thirdQuadrantClockwise =
                angles.Where(a => a.Angle > Math.Atan2(-1, 0) && a.Angle <= 0).OrderBy(a => a.Angle).ToList();
            List<(double Angle, Location)> fourthQuadrantClockwise =
                angles.Where(a => a.Angle > 0 && a.Angle < Math.Atan2(1, 0)).OrderBy(a => a.Angle).ToList();

            angles.Clear();
            angles.AddRange(firstQuadrantClockwise);
            angles.AddRange(secondQuadrantClockwise);
            angles.AddRange(thirdQuadrantClockwise);
            angles.AddRange(fourthQuadrantClockwise);

            return angles;
        }
    }
}
