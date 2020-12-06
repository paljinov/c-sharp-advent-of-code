namespace App.Tasks.Year2020.Day3
{
    public class Trees
    {
        public int CalculateEncounteredTrees(bool[,] areaMap, int rightStep, int downStep)
        {
            int encounteredTrees = 0;

            int areaMapRows = areaMap.GetLength(0);
            int areaMapColumns = areaMap.GetLength(1);

            int rightPosition = rightStep;
            int downPosition = downStep;

            while (downPosition < areaMapRows)
            {
                if (areaMap[downPosition, rightPosition])
                {
                    encounteredTrees++;
                }

                rightPosition += rightStep;
                // If "out of boundaries" the same pattern repeats to the right many times
                if (rightPosition >= areaMapColumns)
                {
                    rightPosition -= areaMapColumns;
                }

                downPosition += downStep;
            }

            return encounteredTrees;
        }
    }
}
