namespace App.Tasks.Year2019.Day2
{
    public class RunProgram
    {
        public int CalculateValueLeftAtFirstPositionAfterProgramHalts(int[] integers)
        {
            integers[1] = 12;
            integers[2] = 2;

            int i = 0;
            while (integers[i] != 99)
            {
                int operation = integers[i];
                int firstValue = integers[integers[i + 1]];
                int secondValue = integers[integers[i + 2]];
                int outputPosition = integers[i + 3];

                if (operation == 1)
                {
                    integers[outputPosition] = firstValue + secondValue;
                }
                else if (operation == 2)
                {
                    integers[outputPosition] = firstValue * secondValue;
                }

                i += 4;
            }

            return integers[0];
        }
    }
}
