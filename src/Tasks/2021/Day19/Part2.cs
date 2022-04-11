/*
--- Part Two ---

Sometimes, it's a good idea to appreciate just how big the ocean is. Using the
Manhattan distance, how far apart do the scanners get?

In the above example, scanners 2 (1105,-1205,1229) and 3 (-92,-2380,-20) are the
largest Manhattan distance apart. In total, they are 1197 + 1175 + 1249 = 3621
units apart.

What is the largest Manhattan distance between any two scanners?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2021.Day19
{
    public class Part2 : ITask<int>
    {
        private readonly BeaconsRepository beaconsRepository;

        private readonly Beacons beacons;

        public Part2()
        {
            beaconsRepository = new BeaconsRepository();
            beacons = new Beacons();
        }

        public int Solution(string input)
        {
            Dictionary<int, List<Position>> beaconsRelativePositions
                = beaconsRepository.GetBeaconsRelativePositions(input);

            int largestManhattanDistanceBetweenAnyTwoScanners =
                beacons.CalculateLargestManhattanDistanceBetweenAnyTwoScanners(beaconsRelativePositions);

            return largestManhattanDistanceBetweenAnyTwoScanners;
        }
    }
}
