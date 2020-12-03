using System.Collections.Generic;

namespace App.Tasks.Year2020.Day3
{
    public class Trees
    {
        public int CalculateEncounteredTrees(bool[,] areaMap, int right, int down)
        {
            int encounteredTrees = 0;

            int rightPosition = right;
            int downPosition = down;

            while (downPosition < areaMap.GetLength(0))
            {
                if (areaMap[downPosition, rightPosition])
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
