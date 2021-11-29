/*
--- Part Two ---

Now, you just need to figure out where to position yourself so that you're
actually teleported when the nanobots activate.

To increase the probability of success, you need to find the coordinate which
puts you in range of the largest number of nanobots. If there are multiple,
choose one closest to your position (0,0,0, measured by manhattan distance).

For example, given the following nanobot formation:

pos=<10,12,12>, r=2
pos=<12,14,12>, r=2
pos=<16,12,12>, r=4
pos=<14,14,14>, r=6
pos=<50,50,50>, r=200
pos=<10,10,10>, r=5

Many coordinates are in range of some of the nanobots in this formation.
However, only the coordinate 12,12,12 is in range of the most nanobots: it is in
range of the first five, but is not in range of the nanobot at 10,10,10. (All
other coordinates are in range of fewer than five nanobots.) This coordinate's
distance from 0,0,0 is 36.

Find the coordinates that are in range of the largest number of nanobots. What
is the shortest manhattan distance between any of those points and 0,0,0?
*/

namespace App.Tasks.Year2018.Day23
{
    public class Part2 : ITask<int>
    {
        private readonly Position myPosition = new Position
        {
            X = 0,
            Y = 0,
            Z = 0
        };

        private readonly NanobotsRepository nanobotsRepository;

        private readonly ExperimentalEmergencyTeleportation experimentalEmergencyTeleportation;

        public Part2()
        {
            nanobotsRepository = new NanobotsRepository();
            experimentalEmergencyTeleportation = new ExperimentalEmergencyTeleportation();
        }

        public int Solution(string input)
        {
            Nanobot[] nanobots = nanobotsRepository.GetNanobots(input);
            int shortestManhattanDistance = experimentalEmergencyTeleportation
                .CalculateShortestManhattanDistanceForPositionInRangeOfLargestNumberOfNanobots(nanobots, myPosition);

            return shortestManhattanDistance;
        }
    }
}
