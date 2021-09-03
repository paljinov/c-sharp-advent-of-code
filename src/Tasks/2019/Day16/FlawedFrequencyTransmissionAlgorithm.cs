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
            Dictionary<int, int> outputList = CalculateFinalOutputList(inputSignal);
            int from = outputList.Keys.First();

            string firstEightDigitsInTheFinalOutputList = string.Empty;
            for (int i = from; i < from + EIGHT_DIGITS; i++)
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
            int offset = int.Parse(string.Join("", inputSignal[..messageOffset]));
            List<int> realInputSignal = new List<int>();

            for (int i = 0; i < inputSignalRepetitions; i++)
            {
                realInputSignal = realInputSignal.Concat(inputSignal.ToList()).ToList();
            }

            Dictionary<int, int> outputList = CalculateFinalOutputList(realInputSignal.ToArray(), offset);
            int from = outputList.Keys.First();

            string eightDigitMessageEmbeddedInTheFinalOutputList = string.Empty;
            for (int i = from; i < from + EIGHT_DIGITS; i++)
            {
                eightDigitMessageEmbeddedInTheFinalOutputList += outputList[i];
            }

            return eightDigitMessageEmbeddedInTheFinalOutputList;
        }

        private Dictionary<int, int> CalculateFinalOutputList(int[] inputSignal, int offset = 0)
        {
            Dictionary<int, int> outputList = InitializeOutputList(inputSignal, offset);

            if (inputSignal.Length > offset)
            {
                for (int phase = 1; phase <= PHASES; phase++)
                {
                    int result = 0;
                    Dictionary<int, int> currentOutputList = new Dictionary<int, int>(outputList);
                    // Iteration for each digit
                    for (int position = inputSignal.Length - 1; position >= offset; position--)
                    {
                        // Only the ones digit is kept
                        result = CalculateResult(currentOutputList, position, inputSignal.Length, result);
                        // Only the ones digit is kept
                        outputList[position] = Math.Abs(result % 10);
                    }
                }
            }

            return outputList;
        }

        private Dictionary<int, int> InitializeOutputList(int[] inputSignal, int offset)
        {
            Dictionary<int, int> outputList = new Dictionary<int, int>();

            for (int position = offset; position < inputSignal.Length; position++)
            {
                // Only the ones digit is kept
                outputList[position] = inputSignal[position];
            }

            return outputList;
        }

        private Dictionary<int, int> GetPattern(int repetitions, int inputSignalLength)
        {
            Dictionary<int, int> pattern = new Dictionary<int, int>();

            int patternIndex = 0;
            int basePatternIndex = 0;

            while (patternIndex <= inputSignalLength)
            {
                for (int i = 0; i < repetitions; i++)
                {
                    // If last position is calculated
                    if (patternIndex > inputSignalLength)
                    {
                        break;
                    }

                    if (patternIndex - 1 >= 0)
                    {
                        // Skip the very first value exactly once
                        pattern[patternIndex - 1] = basePattern[basePatternIndex];
                    }

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

        private int CalculateResult(
            Dictionary<int, int> outputList,
            int position,
            int inputSignalLength,
            int previousResult
        )
        {
            int result = 0;

            if (position >= inputSignalLength / 2)
            {
                result += previousResult + outputList[position] * basePattern[1];
            }
            else
            {
                int from = outputList.Keys.First();
                int to = outputList.Keys.Last();
                Dictionary<int, int> pattern = GetPattern(position + 1, inputSignalLength);

                for (int i = from; i <= to; i++)
                {
                    result += outputList[i] * pattern[i];
                }
            }

            return result;
        }
    }
}
