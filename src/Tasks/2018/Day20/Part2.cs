/*
--- Part Two ---

Okay, so the facility is big.

How many rooms have a shortest path from your current location that pass through
at least 1000 doors?
*/

namespace App.Tasks.Year2018.Day20
{
    public class Part2 : ITask<int>
    {
        private readonly int minDoors = 1000;

        private readonly RegularMap regularMap;

        public Part2()
        {
            regularMap = new RegularMap();
        }

        public int Solution(string input)
        {
            int roomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoors =
                regularMap.CountRoomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoors(
                    input, minDoors);

            return roomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoors;
        }
    }
}
