using System.Collections.Generic;

namespace App.Tasks.Year2019.Day20
{
    public class Maze
    {
        private const string START = "AA";

        private const string END = "ZZ";

        public int CountStepsNeededToGetFromStartTileToEndTile(
            MazeElement[,] mazeMap,
            Dictionary<string, PortalPair> portalPairs
        )
        {
            int minSteps = int.MaxValue;
            (int x, int y) = portalPairs[START].Outer;
            HashSet<(int x, int y)> visitedLocations = new HashSet<(int x, int y)>();

            FindMinimumNeededSteps(mazeMap, portalPairs, x, y, visitedLocations, 0, ref minSteps);

            return minSteps;
        }

        private void FindMinimumNeededSteps(
            MazeElement[,] mazeMap,
            Dictionary<string, PortalPair> portalPairs,
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

            // If not starting location and steps are zero
            if ((x != portalPairs[START].Outer.X || y != portalPairs[START].Outer.Y) && steps == 0)
            {
                return;
            }

            // End is reached in minimum number of steps
            if (x == portalPairs[END].Inner.X && y == portalPairs[END].Inner.Y && steps < minSteps)
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
                    FindMinimumNeededSteps(mazeMap, portalPairs, x - 1, y, visitedLocationsClone, steps, ref minSteps);
                }

                if (x + 1 < mazeMap.GetLength(0) && mazeMap[x + 1, y] == MazeElement.OpenPassage)
                {
                    FindMinimumNeededSteps(mazeMap, portalPairs, x + 1, y, visitedLocationsClone, steps, ref minSteps);
                }

                if (y - 1 >= 0 && mazeMap[x, y - 1] == MazeElement.OpenPassage)
                {
                    FindMinimumNeededSteps(mazeMap, portalPairs, x, y - 1, visitedLocationsClone, steps, ref minSteps);
                }

                if (y + 1 < mazeMap.GetLength(1) && mazeMap[x, y + 1] == MazeElement.OpenPassage)
                {
                    FindMinimumNeededSteps(mazeMap, portalPairs, x, y + 1, visitedLocationsClone, steps, ref minSteps);
                }
            }
        }
    }
}
