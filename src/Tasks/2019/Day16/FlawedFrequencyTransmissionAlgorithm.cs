using System;

namespace App.Tasks.Year2019.Day16
{
    public class FlawedFrequencyTransmissionAlgorithm
    {
        private const int EIGHT_DIGITS = 8;

        private readonly int[] basePattern = new int[] { 0, 1, 0, -1 };

        public string CalculateFirstEightDigitsInTheFinalOutputList(int[] inputSignal, int phases)
        {
            int[] outputList = new int[inputSignal.Length];

            for (int phase = 1; phase <= phases; phase++)
            {
                // Iteration for each digit
                for (int position = 0; position < inputSignal.Length; position++)
                {
                    // Only the ones digit is kept
                    outputList[position] = CalculateOutputDigit(inputSignal, position);
                }

                inputSignal = outputList;
            }

            string firstEightDigitsInTheFinalOutputList = string.Empty;
            for (int i = 0; i < EIGHT_DIGITS; i++)
            {
                firstEightDigitsInTheFinalOutputList += outputList[i];
            }

            return firstEightDigitsInTheFinalOutputList;
        }

        private int CalculateOutputDigit(int[] inputSignal, int position)
        {
            int[] pattern = GetPattern(position + 1);
            // Skip the very first value exactly once
            int patternIndex = 1;

            int result = 0;
            for (int i = 0; i < inputSignal.Length; i++)
            {
                if (patternIndex == pattern.Length)
                {
                    patternIndex = 0;
                }

                result += inputSignal[i] * pattern[patternIndex];
                patternIndex++;
            }

            // Only the ones digit is kept
            int digit = Math.Abs(result % 10);

            return digit;
        }

        private int[] GetPattern(int repetitions)
        {
            int[] pattern = new int[basePattern.Length * repetitions];
            for (int i = 0; i < basePattern.Length; i++)
            {
                for (int j = 0; j < repetitions; j++)
                {
                    pattern[i * repetitions + j] = basePattern[i];
                }
            }

            return pattern;
        }
    }
}
