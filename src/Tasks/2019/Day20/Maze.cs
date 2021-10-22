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
            Dictionary<(int x, int y), string> portals
        )
        {
            int minSteps = int.MaxValue;
            (int X, int Y) start = portals.First(p => p.Value == START).Key;
            (int X, int Y) end = portals.First(p => p.Value == END).Key;
            HashSet<(int x, int y)> visitedLocations = new HashSet<(int x, int y)>();

            FindMinimumNeededSteps(mazeMap, portals, start, end, start, visitedLocations, 0, ref minSteps);

            return minSteps;
        }

        private void FindMinimumNeededSteps(
            MazeElement[,] mazeMap,
            Dictionary<(int x, int y), string> portals,
            (int X, int Y) start,
            (int X, int Y) end,
            (int X, int Y) currentLocation,
            HashSet<(int x, int y)> visitedLocations,
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

            if (currentLocation.X - 1 >= 0 && mazeMap[currentLocation.X - 1, currentLocation.Y] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X - 1, currentLocation.Y));
            }

            if (currentLocation.X + 1 < mazeMap.GetLength(0) && mazeMap[currentLocation.X + 1, currentLocation.Y] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X + 1, currentLocation.Y));
            }

            if (currentLocation.Y - 1 >= 0 && mazeMap[currentLocation.X, currentLocation.Y - 1] == MazeElement.OpenPassage)
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y - 1));
            }

            if (currentLocation.Y + 1 < mazeMap.GetLength(1) && mazeMap[currentLocation.X, currentLocation.Y + 1] == MazeElement.OpenPassage)
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
                    new HashSet<(int x, int y)>(visitedLocations), steps, ref minSteps);
            }
        }

        private void MoveToNextLocation(
            MazeElement[,] mazeMap,
            Dictionary<(int x, int y), string> portals,
            (int X, int Y) start,
            (int X, int Y) end,
            (int X, int Y) nextLocation,
            HashSet<(int x, int y)> visitedLocations,
            int steps,
            ref int minSteps
        )
        {
            if (portals.ContainsKey((nextLocation.X, nextLocation.Y)))
            {
                string portal = portals.First(p => p.Key.x == nextLocation.X && p.Key.y == nextLocation.Y).Value;
                (int X, int Y) portalExit = portals.FirstOrDefault(
                    p => p.Value == portal && (p.Key.x != nextLocation.X || p.Key.y != nextLocation.Y)).Key;

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
    }
}
