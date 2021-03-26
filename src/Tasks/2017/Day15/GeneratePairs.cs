using System;

namespace App.Tasks.Year2017.Day15
{
    public class GeneratePairs
    {
        private const int GENERATOR_A_FACTOR = 16807;

        private const int GENERATOR_B_FACTOR = 48271;

        private const int DIVIDE_BY = 2147483647;

        private const int MATCH_LOWEST_BITS = 16;

        public int CalculateJudgeFinalCount(int generatorAStartValue, int generatorBStartValue, int totalPairs)
        {
            int matchLowestBitsPairs = 0;

            long generatorAValue = generatorAStartValue;
            long generatorBValue = generatorBStartValue;

            for (int i = 0; i < totalPairs; i++)
            {
                generatorAValue = (generatorAValue * GENERATOR_A_FACTOR) % DIVIDE_BY;
                generatorBValue = (generatorBValue * GENERATOR_B_FACTOR) % DIVIDE_BY;

                string generatorAGetLowestBits = GetLowestBits(generatorAValue);
                string generatorBGetLowestBits = GetLowestBits(generatorBValue);

                if (generatorAGetLowestBits == generatorBGetLowestBits)
                {
                    matchLowestBitsPairs++;
                }
            }

            return matchLowestBitsPairs;
        }

        public int CalculateJudgeFinalCountForPickyCriteria(
            int generatorAStartValue,
            int generatorBStartValue,
            int totalPairs,
            int generatorAValueMultipleOf,
            int generatorBValueMultipleOf
        )
        {
            int matchLowestBitsPairs = 0;

            long generatorAValue = generatorAStartValue;
            long generatorBValue = generatorBStartValue;

            for (int i = 0; i < totalPairs; i++)
            {
                generatorAValue = (generatorAValue * GENERATOR_A_FACTOR) % DIVIDE_BY;
                while (generatorAValue % generatorAValueMultipleOf != 0)
                {
                    generatorAValue = (generatorAValue * GENERATOR_A_FACTOR) % DIVIDE_BY;
                }

                generatorBValue = (generatorBValue * GENERATOR_B_FACTOR) % DIVIDE_BY;
                while (generatorBValue % generatorBValueMultipleOf != 0)
                {
                    generatorBValue = (generatorBValue * GENERATOR_B_FACTOR) % DIVIDE_BY;
                }

                string generatorAGetLowestBits = GetLowestBits(generatorAValue);
                string generatorBGetLowestBits = GetLowestBits(generatorBValue);

                if (generatorAGetLowestBits == generatorBGetLowestBits)
                {
                    matchLowestBitsPairs++;
                }
            }

            return matchLowestBitsPairs;
        }

        private string GetLowestBits(long generatorValue)
        {
            // Convert to binary and prepend with zeros to satisfy minimal length
            string binary = Convert.ToString(generatorValue, 2).PadLeft(MATCH_LOWEST_BITS, '0');

            return binary[^MATCH_LOWEST_BITS..];
        }
    }
}
