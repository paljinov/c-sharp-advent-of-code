using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day16
{
    public class FlawedFrequencyTransmissionAlgorithm
    {
        private const int PHASES = 100;

        private const int EIGHT_DIGITS = 8;

        private readonly int[] basePattern = new int[] { 0, 1, 0, -1 };

        public string CalculateFirstEightDigitsInTheFinalOutputList(int[] inputSignal)
        {
            int[] outputList = CalculateFinalOutputList(inputSignal);

            string firstEightDigitsInTheFinalOutputList = string.Empty;
            for (int i = 0; i < EIGHT_DIGITS; i++)
            {
                firstEightDigitsInTheFinalOutputList += outputList[i];
            }

            return firstEightDigitsInTheFinalOutputList;
        }

        public string CalculateEightDigitMessageEmbeddedInTheFinalOutputList(
            int[] inputSignal,
            int inputSignalRepetitions,
            int messageOffset
        )
        {
            List<int> realInputSignal = new List<int>();

            for (int i = 0; i < inputSignalRepetitions; i++)
            {
                realInputSignal = realInputSignal.Concat(inputSignal.ToList()).ToList();
            }

            int[] outputList = CalculateFinalOutputList(realInputSignal.ToArray());

            string eightDigitMessageEmbeddedInTheFinalOutputList = string.Empty;
            for (int i = messageOffset; i < messageOffset + EIGHT_DIGITS; i++)
            {
                eightDigitMessageEmbeddedInTheFinalOutputList += outputList[i];
            }

            return eightDigitMessageEmbeddedInTheFinalOutputList;
        }

        private int[] CalculateFinalOutputList(int[] inputSignal)
        {
            int[] outputList = new int[inputSignal.Length];

            int[][] patternForEachPosition = GetPatternForEachPosition(inputSignal.Length);

            for (int phase = 1; phase <= PHASES; phase++)
            {
                // Iteration for each digit
                for (int position = 0; position < inputSignal.Length; position++)
                {
                    // Only the ones digit is kept
                    outputList[position] = CalculateOutputDigit(inputSignal, patternForEachPosition[position]);
                }

                inputSignal = outputList;
            }

            return outputList;
        }

        private int[][] GetPatternForEachPosition(int inputSignalLength)
        {
            int[][] patternForEachPosition = new int[inputSignalLength][];

            for (int position = 0; position < inputSignalLength; position++)
            {
                int[] pattern = GetPattern(position + 1, inputSignalLength);
                patternForEachPosition[position] = pattern;
            }

            return patternForEachPosition;
        }


        private int[] GetPattern(int repetitions, int inputSignalLength)
        {
            bool firstValue = true;
            int patternIndex = 0;
            int basePatternIndex = 0;
            int[] pattern = new int[inputSignalLength];

            while (patternIndex < inputSignalLength)
            {
                for (int i = 0; i < repetitions; i++)
                {
                    // Skip the very first value exactly once
                    if (firstValue)
                    {
                        firstValue = false;
                        continue;
                    }

                    // If last position is calculated
                    if (patternIndex >= inputSignalLength)
                    {
                        return pattern;
                    }

                    pattern[patternIndex] = basePattern[basePatternIndex];
                    patternIndex++;
                }

                basePatternIndex++;
                if (basePatternIndex == basePattern.Length)
                {
                    basePatternIndex = 0;
                }
            }

            return pattern;
        }

        private int CalculateOutputDigit(int[] inputSignal, int[] pattern)
        {
            int result = 0;
            for (int i = 0; i < inputSignal.Length; i++)
            {
                result += inputSignal[i] * pattern[i];
            }

            // Only the ones digit is kept
            int digit = Math.Abs(result % 10);

            return digit;
        }
    }
}
