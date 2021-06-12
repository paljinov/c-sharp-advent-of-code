using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day6
{
    public class Orbits
    {
        private const string YOU = "YOU";

        private const string SANTA = "SAN";

        public int CountTotalNumberOfDirectAndIndirectOrbits(List<(string, string)> localOrbits)
        {
            string rootObject = localOrbits.Select(o => o.Item1).Except(localOrbits.Select(o => o.Item2)).First();
            int totalOrbits = DoCountOrbits(localOrbits, rootObject, 0);

            return totalOrbits;
        }

        public int CalculateMinimumNumberOfOrbitalTransfersRequiredToReachSanta(List<(string, string)> localOrbits)
        {
            List<string> parentObjectsForYou = GetParentObjects(localOrbits, YOU);
            List<string> parentObjectsForSanta = GetParentObjects(localOrbits, SANTA);

            int minimumNumberOfOrbitalTransfersRequiredToReachSanta =
                parentObjectsForYou.Except(parentObjectsForSanta).Count()
                + parentObjectsForSanta.Except(parentObjectsForYou).Count();

            return minimumNumberOfOrbitalTransfersRequiredToReachSanta;
        }

        private int DoCountOrbits(List<(string, string)> localOrbits, string current, int depth)
        {
            int totalOrbits = 0;

            depth++;
            foreach ((string, string) localOrbit in localOrbits)
            {
                if (localOrbit.Item1 == current)
                {
                    totalOrbits += depth;
                    totalOrbits += DoCountOrbits(localOrbits, localOrbit.Item2, depth);
                }
            }

            return totalOrbits;
        }

        private List<string> GetParentObjects(List<(string, string)> localOrbits, string from)
        {
            List<string> parentObjects = new List<string>();

            foreach ((string, string) localOrbit in localOrbits)
            {
                if (localOrbit.Item2 == from)
                {
                    parentObjects.Add(localOrbit.Item1);
                    parentObjects = parentObjects.Union(GetParentObjects(localOrbits, localOrbit.Item1)).ToList();
                }
            }

            return parentObjects;
        }
    }
}
