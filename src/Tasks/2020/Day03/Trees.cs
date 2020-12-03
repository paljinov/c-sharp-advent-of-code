using System.Collections.Generic;

namespace App.Tasks.Year2020.Day3
{
    public class Trees
    {
        public int CalculateEncounteredTrees(List<int[]> areaMap, int right, int down)
        {
            int encounteredTrees = 0;

            int rightPosition = right;
            int downPosition = down;


            while (downPosition < areaMap.Count)
            {
                int[] row = areaMap[downPosition];
                if (row[rightPosition] == 1)
                {
                    encounteredTrees++;
                }

                rightPosition += right;
                downPosition += down;
            }

            return encounteredTrees;
        }
    }
}
