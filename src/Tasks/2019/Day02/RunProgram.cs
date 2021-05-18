using System.Linq;

namespace App.Tasks.Year2019.Day2
{
    public class RunProgram
    {
        private const int ADDITION = 1;

        private const int MULTIPLICATION = 2;

        private const int HALT = 99;

        private const int FIRST_POSITION_REPLACEMENT = 12;

        private const int SECOND_POSITION_REPLACEMENT = 2;

        public int CalculateValueLeftAtFirstPositionAfterProgramHalts(int[] integers, bool replacePositions)
        {
            if (replacePositions)
            {
                integers[1] = FIRST_POSITION_REPLACEMENT;
                integers[2] = SECOND_POSITION_REPLACEMENT;
            }

            int i = 0;
            while (integers[i] != HALT && i < integers.Length)
            {
                int operation = integers[i];
                int firstValue = integers[integers[i + 1]];
                int secondValue = integers[integers[i + 2]];
                int outputPosition = integers[i + 3];

                if (outputPosition >= integers.Length)
                {
                    break;
                }

                if (operation == ADDITION)
                {
                    integers[outputPosition] = firstValue + secondValue;
                }
                else if (operation == MULTIPLICATION)
                {
                    integers[outputPosition] = firstValue * secondValue;
                }

                i += 4;
            }

            return integers[0];
        }

        public int CalculateNounAndVerbResult(
            int[] integers,
            int output,
            int inputValuesFrom,
            int inputValuesTo
        )
        {
            int noun;
            int verb = 0;
            bool inputsFound = false;

            for (noun = inputValuesFrom; noun <= inputValuesTo; noun++)
            {
                for (verb = inputValuesFrom; verb <= inputValuesTo; verb++)
                {
                    integers[1] = noun;
                    integers[2] = verb;

                    int valueLeftAtFirstPositionAfterProgramHalts =
                        CalculateValueLeftAtFirstPositionAfterProgramHalts(integers.ToArray(), false);

                    if (valueLeftAtFirstPositionAfterProgramHalts == output)
                    {
                        inputsFound = true;
                        break;
                    }
                }

                if (inputsFound)
                {
                    break;
                }
            }

            int nounAndVerbResult = 100 * noun + verb;

            return nounAndVerbResult;
        }
    }
}
