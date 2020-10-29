/*
--- Part Two ---

Then, you notice the instructions continue on the back of the Recruiting
Document. Easter Bunny HQ is actually at the first location you visit twice.

For example, if your instructions are R8, R4, R4, R8, the first location you
visit twice is 4 blocks away, due East.

How many blocks away is the first location you visit twice?
*/

using System;
using System.Collections.Generic;

namespace App.Tasks.Year2016.Day1
{
    class Part2 : ITask<int>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly CityStreetGrid cityStreetGrid;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            cityStreetGrid = new CityStreetGrid();
        }

        public int Solution(string input)
        {
            string[] instructions = instructionsRepository.GetInstructions(input);
            List<(int, int)> visitedBlocks = cityStreetGrid.CalculateVisitedBlocksAfterMove(instructions);
            List<(int, int)> allVisitedLocations = cityStreetGrid.CalculateAllVisitedLocations(visitedBlocks);

            (int x, int y) firstRepeatedLocation = (0, 0);
            List<(int, int)> uniqueLocations = new List<(int, int)>();

            foreach ((int x, int y) visitedLocation in allVisitedLocations)
            {
                if (uniqueLocations.Contains(visitedLocation))
                {
                    firstRepeatedLocation = visitedLocation;
                    break;
                }

                uniqueLocations.Add(visitedLocation);
            }

            int blocksAway = Math.Abs(firstRepeatedLocation.x) + Math.Abs(firstRepeatedLocation.y);

            return blocksAway;
        }
    }
}
