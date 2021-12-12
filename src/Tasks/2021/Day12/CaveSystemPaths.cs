using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day12
{
    public class CaveSystemPaths
    {
        private const string START = "start";

        private const string END = "end";

        public int CountCaveSystemPathsThatVisitSmallCavesAtMostOnce((string, string)[] caveSystem)
        {
            List<string> currentPath = new List<string>();
            HashSet<string> pathsCache = new HashSet<string>();
            int caveSystemPathsThatVisitSmallCavesAtMostOnce =
                DoCountCaveSystemPathsThatVisitSmallCavesAtMostOnce(caveSystem, currentPath, START, pathsCache);

            return caveSystemPathsThatVisitSmallCavesAtMostOnce;
        }

        private int DoCountCaveSystemPathsThatVisitSmallCavesAtMostOnce(
            (string, string)[] caveSystem,
            List<string> currentPath,
            string currentCave,
            HashSet<string> pathsCache
        )
        {
            int caveSystemPathsThatVisitSmallCavesAtMostOnce = 0;

            currentPath.Add(currentCave);

            string pathString = string.Join(',', currentPath);
            // If this path is already reached
            if (pathsCache.Contains(pathString))
            {
                return 0;
            }
            pathsCache.Add(pathString);

            foreach ((string First, string Second) caves in caveSystem)
            {
                if (caves.First == currentCave || caves.Second == currentCave)
                {
                    string nextCave = caves.First;
                    if (caves.First == currentCave)
                    {
                        nextCave = caves.Second;
                    }

                    // If small cave is not already visited
                    if (!IsCaveSmallAndAlreadyVisited(nextCave, currentPath))
                    {
                        // If end is reached
                        if (nextCave == END)
                        {
                            caveSystemPathsThatVisitSmallCavesAtMostOnce++;
                        }
                        else
                        {
                            caveSystemPathsThatVisitSmallCavesAtMostOnce +=
                                DoCountCaveSystemPathsThatVisitSmallCavesAtMostOnce(
                                    caveSystem, currentPath.ToList(), nextCave, pathsCache);
                        }
                    }
                }
            }

            return caveSystemPathsThatVisitSmallCavesAtMostOnce;
        }

        private bool IsCaveSmallAndAlreadyVisited(string cave, List<string> path)
        {
            // Big caves are written in uppercase
            if (char.IsUpper(cave[0]))
            {
                return false;
            }

            // If path already contains small cave
            if (!path.Contains(cave))
            {
                return false;
            }

            return true;
        }
    }
}
