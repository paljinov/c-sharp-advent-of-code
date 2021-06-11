using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day6
{
    public class Orbits
    {
        public int CountTotalNumberOfDirectAndIndirectOrbits(List<(string, string)> orbits)
        {
            string orbitsNothing = orbits.Select(o => o.Item1).Except(orbits.Select(o => o.Item2)).First();

            int totalOrbits = DoCountOrbits(orbits, orbitsNothing, 0);

            return totalOrbits;
        }

        private int DoCountOrbits(List<(string, string)> orbits, string current, int depth)
        {
            int totalOrbits = 0;

            depth++;
            foreach ((string, string) orbit in orbits)
            {
                if (orbit.Item1 == current)
                {
                    totalOrbits += depth;
                    totalOrbits += DoCountOrbits(orbits, orbit.Item2, depth);
                }
            }

            return totalOrbits;
        }
    }
}
