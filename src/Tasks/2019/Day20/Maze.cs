using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day20
{
    public class Maze
    {
        private const string START = "AA";

        private const string END = "ZZ";

        public int CountStepsNeededToGetFromStartTileToEndTile(
            MazeElement[,] mazeMap,
            Dictionary<(int X, int Y), string> portals
        )
        {
            int minSteps = int.MaxValue;
            ((int X, int Y) start, (int X, int Y) end) = GetStartAndEndTileLocations(portals);
            HashSet<(int X, int Y)> visitedLocations = new HashSet<(int X, int Y)>();

            FindMinimumNeededSteps(mazeMap, portals, start, end, start, visitedLocations, 0, ref minSteps);

            return minSteps;
        }

        public int CountStepsNeededToGetFromStartTileToEndTileForRecursiveSpaces(
            MazeElement[,] mazeMap,
            Dictionary<(int X, int Y), string> portals
        )
        {
            int minSteps = int.MaxValue;
            ((int X, int Y) start, (int X, int Y) end) = GetStartAndEndTileLocations(portals);

            int maxDepth = 0;
            while (minSteps == int.MaxValue)
            {
                HashSet<(int X, int Y, int Level)> visitedLocations = new HashSet<(int X, int Y, int Level)>();
                FindMinimumNeededStepsForRecursiveSpaces(mazeMap, portals, (start.X, start.Y, 0), (end.X, end.Y, 0),
                    (start.X, start.Y, 0), visitedLocations, 0, maxDepth, ref minSteps);
                maxDepth++;
            }
            return minSteps;
        }

        private ((int X, int Y) start, (int X, int Y) end) GetStartAndEndTileLocations(
            Dictionary<(int X, int Y), string> portals
        )
        {
            (int X, int Y) start = portals.First(p => p.Value == START).Key;
            (int X, int Y) end = portals.First(p => p.Value == END).Key;

            return (start, end);
        }

        private void FindMinimumNeededSteps(
            MazeElement[,] mazeMap,
            Dictionary<(int X, int Y), string> portals,
            (int X, int Y) start,
            (int X, int Y) end,
            (int X, int Y) currentLocation,
            HashSet<(int X, int Y)> visitedLocations,
            int steps,
            ref int minSteps
        )
        {
            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            // If outside maze map
            if (currentLocation.X < 0 || currentLocation.Y < 0
                || currentLocation.X >= mazeMap.GetLength(0) || currentLocation.Y >= mazeMap.GetLength(1))
            {
                return;
            }

            // If location is already visited
            if (visitedLocations.Contains((currentLocation.X, currentLocation.Y)))
            {
                return;
            }

            visitedLocations.Add((currentLocation.X, currentLocation.Y));
            List<(int X, int Y)> nextLocations = new List<(int X, int Y)>();

            if (currentLocation.X - 1 >= 0
                && mazeMap[currentLocation.X - 1, currentLocation.Y] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X - 1, currentLocation.Y));
            }

            if (currentLocation.X + 1 < mazeMap.GetLength(0)
                && mazeMap[currentLocation.X + 1, currentLocation.Y] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X + 1, currentLocation.Y));
            }

            if (currentLocation.Y - 1 >= 0
                && mazeMap[currentLocation.X, currentLocation.Y - 1] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y - 1));
            }

            if (currentLocation.Y + 1 < mazeMap.GetLength(1)
                && mazeMap[currentLocation.X, currentLocation.Y + 1] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y + 1));
            }

            steps++;
            foreach ((int X, int Y) nextLocation in nextLocations)
            {
                // End is reached in minimum number of steps
                if (nextLocation.X == end.X && nextLocation.Y == end.Y && steps < minSteps)
                {
                    minSteps = steps;
                    return;
                }

                MoveToNextLocation(mazeMap, portals, start, end, nextLocation,
                    new HashSet<(int X, int Y)>(visitedLocations), steps, ref minSteps);
            }
        }

        private void MoveToNextLocation(
            MazeElement[,] mazeMap,
            Dictionary<(int X, int Y), string> portals,
            (int X, int Y) start,
            (int X, int Y) end,
            (int X, int Y) nextLocation,
            HashSet<(int X, int Y)> visitedLocations,
            int steps,
            ref int minSteps
        )
        {
            if (portals.ContainsKey((nextLocation.X, nextLocation.Y)))
            {
                string portal = portals.First(p => p.Key.X == nextLocation.X && p.Key.Y == nextLocation.Y).Value;
                (int X, int Y) portalExit = portals.FirstOrDefault(
                    p => p.Value == portal && (p.Key.X != nextLocation.X || p.Key.Y != nextLocation.Y)).Key;

                // If portal has exit
                if (portalExit.X > 0 || portalExit.Y > 0)
                {
                    FindMinimumNeededSteps(
                        mazeMap, portals, start, end, portalExit, visitedLocations, steps + 1, ref minSteps);
                }
            }
            else
            {
                FindMinimumNeededSteps(
                    mazeMap, portals, start, end, nextLocation, visitedLocations, steps, ref minSteps);
            }
        }

        private void FindMinimumNeededStepsForRecursiveSpaces(
            MazeElement[,] mazeMap,
            Dictionary<(int X, int Y), string> portals,
            (int X, int Y, int Level) start,
            (int X, int Y, int Level) end,
            (int X, int Y, int Level) currentLocation,
            HashSet<(int X, int Y, int Level)> visitedLocations,
            int steps,
            int maxDepth,
            ref int minSteps
        )
        {
            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            // If outside maze map
            if (currentLocation.X < 0 || currentLocation.Y < 0
                || currentLocation.X >= mazeMap.GetLength(0) || currentLocation.Y >= mazeMap.GetLength(1)
                || currentLocation.Level < 0)
            {
                return;
            }

            // If location is already visited
            if (visitedLocations.Contains((currentLocation.X, currentLocation.Y, currentLocation.Level)))
            {
                return;
            }

            if (currentLocation.Level > maxDepth)
            {
                return;
            }

            visitedLocations.Add((currentLocation.X, currentLocation.Y, currentLocation.Level));
            List<(int X, int Y, int Level)> nextLocations = new List<(int X, int Y, int Level)>();

            if (currentLocation.X - 1 >= 0
                && mazeMap[currentLocation.X - 1, currentLocation.Y] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X - 1, currentLocation.Y, currentLocation.Level));
            }

            if (currentLocation.X + 1 < mazeMap.GetLength(0)
                && mazeMap[currentLocation.X + 1, currentLocation.Y] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X + 1, currentLocation.Y, currentLocation.Level));
            }

            if (currentLocation.Y - 1 >= 0
                && mazeMap[currentLocation.X, currentLocation.Y - 1] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y - 1, currentLocation.Level));
            }

            if (currentLocation.Y + 1 < mazeMap.GetLength(1)
                && mazeMap[currentLocation.X, currentLocation.Y + 1] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y + 1, currentLocation.Level));
            }

            steps++;
            foreach ((int X, int Y, int Level) nextLocation in nextLocations)
            {
                // End is reached in minimum number of steps
                if (nextLocation.X == end.X && nextLocation.Y == end.Y && nextLocation.Level == 0 && steps < minSteps)
                {
                    minSteps = steps;
                    return;
                }

                MoveToNextLocationForRecursiveSpaces(mazeMap, portals, start, end, nextLocation,
                    new HashSet<(int X, int Y, int Level)>(visitedLocations), steps, maxDepth, ref minSteps);
            }
        }

        private void MoveToNextLocationForRecursiveSpaces(
            MazeElement[,] mazeMap,
            Dictionary<(int X, int Y), string> portals,
            (int X, int Y, int Level) start,
            (int X, int Y, int Level) end,
            (int X, int Y, int Level) nextLocation,
            HashSet<(int X, int Y, int Level)> visitedLocations,
            int steps,
            int maxDepth,
            ref int minSteps
        )
        {
            if (portals.ContainsKey((nextLocation.X, nextLocation.Y)))
            {
                string portal = portals.First(p => p.Key.X == nextLocation.X && p.Key.Y == nextLocation.Y).Value;
                (int X, int Y) portalExit = portals.FirstOrDefault(
                    p => p.Value == portal && (p.Key.X != nextLocation.X || p.Key.Y != nextLocation.Y)).Key;

                // If portal has exit
                if (portalExit.X > 0 || portalExit.Y > 0)
                {
                    int level = nextLocation.Level;
                    // If inner edge
                    if (nextLocation.X > 0 && nextLocation.X < mazeMap.GetLength(0) - 1
                        && nextLocation.Y > 0 && nextLocation.Y < mazeMap.GetLength(1) - 1)
                    {
                        level++;
                    }
                    else
                    {
                        level--;
                    }


                    FindMinimumNeededStepsForRecursiveSpaces(mazeMap, portals, start, end,
                        (portalExit.X, portalExit.Y, level), visitedLocations, steps + 1, maxDepth, ref minSteps);
                }
            }
            else
            {
                FindMinimumNeededStepsForRecursiveSpaces(
                    mazeMap, portals, start, end, nextLocation, visitedLocations, steps, maxDepth, ref minSteps);
            }
        }
    }
}
