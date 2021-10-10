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
            (int x, int y) = portals.First(p => p.Value == START).Key;
            HashSet<(int x, int y)> visitedLocations = new HashSet<(int x, int y)>();

            FindMinimumNeededSteps(mazeMap, portals, x, y, visitedLocations, 0, ref minSteps);

            return minSteps;
        }

        private void FindMinimumNeededSteps(
            MazeElement[,] mazeMap,
            Dictionary<(int x, int y), string> portals,
            int x,
            int y,
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

            // If location is already visited
            if (visitedLocations.Contains((x, y)))
            {
                return;
            }

            (int X, int Y) start = portals.First(p => p.Value == START).Key;
            (int X, int Y) end = portals.First(p => p.Value == END).Key;

            // If not starting location and steps are zero
            if ((x != start.X || y != start.Y) && steps == 0)
            {
                return;
            }

            // End is reached in minimum number of steps
            if (x == end.X && y == end.Y && steps < minSteps)
            {
                minSteps = steps;
                return;
            }

            visitedLocations.Add((x, y));

            // If location is opened passage
            if (mazeMap[x, y] == MazeElement.OpenPassage)
            {
                steps++;

                HashSet<(int x, int y)> visitedLocationsClone = new HashSet<(int x, int y)>(visitedLocations);

                if (x - 1 >= 0 && mazeMap[x - 1, y] == MazeElement.OpenPassage)
                {
                    FindMinimumNeededSteps(mazeMap, portals, x - 1, y, visitedLocationsClone, steps, ref minSteps);
                    UsePortalIfPossible(mazeMap, portals, x - 1, y, visitedLocationsClone, steps, ref minSteps);
                }

                if (x + 1 < mazeMap.GetLength(0) && mazeMap[x + 1, y] == MazeElement.OpenPassage)
                {
                    FindMinimumNeededSteps(mazeMap, portals, x + 1, y, visitedLocationsClone, steps, ref minSteps);
                    UsePortalIfPossible(mazeMap, portals, x + 1, y, visitedLocationsClone, steps, ref minSteps);
                }

                if (y - 1 >= 0 && mazeMap[x, y - 1] == MazeElement.OpenPassage)
                {
                    FindMinimumNeededSteps(mazeMap, portals, x, y - 1, visitedLocationsClone, steps, ref minSteps);
                    UsePortalIfPossible(mazeMap, portals, x, y - 1, visitedLocationsClone, steps, ref minSteps);
                }

                if (y + 1 < mazeMap.GetLength(1) && mazeMap[x, y + 1] == MazeElement.OpenPassage)
                {
                    FindMinimumNeededSteps(mazeMap, portals, x, y + 1, visitedLocationsClone, steps, ref minSteps);
                    UsePortalIfPossible(mazeMap, portals, x, y + 1, visitedLocationsClone, steps, ref minSteps);
                }
            }
        }

        private void UsePortalIfPossible(
            MazeElement[,] mazeMap,
            Dictionary<(int x, int y), string> portals,
            int x,
            int y,
            HashSet<(int x, int y)> visitedLocations,
            int steps,
            ref int minSteps
        )
        {
            if (portals.ContainsKey((x, y)))
            {
                string portal = portals.First(p => p.Key.x == x && p.Key.y == y).Value;
                (int X, int Y) portalExit = portals.FirstOrDefault(
                    p => p.Value == portal && (p.Key.x != x || p.Key.y != y)).Key;

                if (portalExit.X > 0 || portalExit.Y > 0)
                {
                    FindMinimumNeededSteps(
                        mazeMap, portals, portalExit.X, portalExit.Y, visitedLocations, steps + 1, ref minSteps);
                }
            }
        }
    }
}
