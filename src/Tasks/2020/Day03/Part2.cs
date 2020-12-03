/*
--- Part Two ---

Time to check the rest of the slopes - you need to minimize the probability of a
sudden arboreal stop, after all.

Determine the number of trees you would encounter if, for each of the following
slopes, you start at the top-left corner and traverse the map all the way to the
bottom:

Right 1, down 1.
Right 3, down 1. (This is the slope you already checked.)
Right 5, down 1.
Right 7, down 1.
Right 1, down 2.

In the above example, these slopes would find 2, 7, 3, 4, and 2 tree(s)
respectively; multiplied together, these produce the answer 336.

What do you get if you multiply together the number of trees encountered on each
of the listed slopes?
*/

using System;

namespace App.Tasks.Year2020.Day3
{
    public class Part2 : ITask<long>
    {
        private readonly int[,] slopes = new int[,] {
            { 1, 1 },
            { 3, 1 },
            { 5, 1 },
            { 7, 1 },
            { 1, 2 }
        };

        private readonly AreaMapRepository areaMapRepository;

        private readonly Trees trees;

        public Part2()
        {
            areaMapRepository = new AreaMapRepository();
            trees = new Trees();
        }

        public long Solution(string input)
        {
            long encounteredTreesMultiplication = 1;

            int largestAreaMapIndex = GetLargestAreaMapIndex();
            // Largest area map is enough for all calculations
            bool[,] areaMap = areaMapRepository.GetAreaMap(
                input,
                slopes[largestAreaMapIndex, 0],
                slopes[largestAreaMapIndex, 1]
            );

            for (int i = 0; i < slopes.GetLength(0); i++)
            {
                int right = slopes[i, 0];
                int down = slopes[i, 1];

                int encounteredTrees = trees.CalculateEncounteredTrees(areaMap, right, down);
                encounteredTreesMultiplication *= encounteredTrees;
            }

            return encounteredTreesMultiplication;
        }

        private int GetLargestAreaMapIndex()
        {
            double max = 0;
            int maxIndex = 0;

            for (int i = 0; i < slopes.GetLength(0); i++)
            {
                int right = slopes[i, 0];
                int down = slopes[i, 1];

                if (right / down > max)
                {
                    max = right / down;
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
    }
}
