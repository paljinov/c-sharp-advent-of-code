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
            int caveSystemPathsThatVisitSmallCavesAtMostOnce =
                DoCountCaveSystemPaths(caveSystem, 1, currentPath, START);

            return caveSystemPathsThatVisitSmallCavesAtMostOnce;
        }

        public int CountCaveSystemPathsThatVisitSmallCavesAtMostTwice((string, string)[] caveSystem)
        {
            List<string> currentPath = new List<string>();
            int caveSystemPathsThatVisitSmallCavesAtMostTwice =
                DoCountCaveSystemPaths(caveSystem, 2, currentPath, START);

            return caveSystemPathsThatVisitSmallCavesAtMostTwice;
        }

        private int DoCountCaveSystemPaths(
            (string, string)[] caveSystem,
            int singleSmallCaveMaxVisits,
            List<string> currentPath,
            string currentCave
        )
        {
            int totalPaths = 0;

            currentPath.Add(currentCave);

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
                    if (!IsCaveSmallAndAlreadyVisitedMaxTimes(nextCave, currentPath, singleSmallCaveMaxVisits))
                    {
                        // If end is reached
                        if (nextCave == END)
                        {
                            totalPaths++;
                        }
                        else
                        {
                            totalPaths += DoCountCaveSystemPaths(
                                caveSystem, singleSmallCaveMaxVisits, currentPath.ToList(), nextCave);
                        }
                    }
                }
            }

            return totalPaths;
        }

        private bool IsCaveSmallAndAlreadyVisitedMaxTimes(string cave, List<string> path, int singleSmallCaveMaxVisits)
        {
            // Big caves are written in uppercase
            if (char.IsUpper(cave[0]))
            {
                return false;
            }

            int caveOccurencesInPath = path.Count(c => c == cave);

            // The small caves named start and end can only be visited exactly once
            if ((cave == START || cave == END) && caveOccurencesInPath >= 1)
            {
                return true;
            }

            int maxSmallCaveOccurencesInPath = path.Where(c => char.IsLower(c[0]))
                .GroupBy(c => c)
                .OrderByDescending(c => c.Count())
                .First()
                .Count();

            // If small cave is already visited max times
            if ((maxSmallCaveOccurencesInPath <= 1 && caveOccurencesInPath >= singleSmallCaveMaxVisits)
                || (maxSmallCaveOccurencesInPath >= 2 && caveOccurencesInPath >= 1))
            {
                return true;
            }

            return false;
        }
    }
}
