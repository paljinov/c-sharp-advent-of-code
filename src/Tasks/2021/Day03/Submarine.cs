using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day3
{
    public class Submarine
    {
        public int CalculateSubmarinePowerConsumption(string[] diagnosticReport)
        {
            int binaryNumbersHalf = diagnosticReport.Length / 2;
            int[] bitsEqualToOneCount = CountBitsEqualToOne(diagnosticReport);

            string gammaRate = CalculateGammaRate(bitsEqualToOneCount, binaryNumbersHalf);
            string epsilonRate = InvertBinaryNumber(gammaRate);

            int submarinePowerConsumption = Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);

            return submarinePowerConsumption;
        }

        public int CalculateSubmarineLifeSupportRating(string[] diagnosticReport)
        {
            string oxygenGeneratorRating = PruneRatings(diagnosticReport, true);
            string carbonDioxideScrubberRating = PruneRatings(diagnosticReport, false);

            int submarineLifeSupportRating =
                Convert.ToInt32(oxygenGeneratorRating, 2) * Convert.ToInt32(carbonDioxideScrubberRating, 2);

            return submarineLifeSupportRating;
        }

        private int[] CountBitsEqualToOne(string[] diagnosticReport)
        {
            int binaryNumberLength = diagnosticReport[0].Length;

            int[] bitsEqualToOne = new int[binaryNumberLength];

            for (int position = 0; position < binaryNumberLength; position++)
            {
                bitsEqualToOne[position] = CountBitsEqualToOneForPosition(diagnosticReport, position);
            }

            return bitsEqualToOne;
        }

        private int CountBitsEqualToOneForPosition(string[] binaryNumbers, int position)
        {
            int bitsEqualToOne = 0;

            for (int i = 0; i < binaryNumbers.Length; i++)
            {
                string binaryBumber = binaryNumbers[i];
                if (binaryBumber[position] == '1')
                {
                    bitsEqualToOne++;
                }
            }

            return bitsEqualToOne;
        }

        private string CalculateGammaRate(int[] bitsEqualToOneCount, int binaryNumbersHalf)
        {
            char[] gammaRate = new char[bitsEqualToOneCount.Length];

            for (int i = 0; i < bitsEqualToOneCount.Length; i++)
            {
                if (bitsEqualToOneCount[i] > binaryNumbersHalf)
                {
                    gammaRate[i] = '1';
                }
                else
                {
                    gammaRate[i] = '0';
                }
            }

            return new string(gammaRate);
        }

        private string InvertBinaryNumber(string binaryNumber)
        {
            char[] invertedBinaryNumber = new char[binaryNumber.Length];

            for (int i = 0; i < binaryNumber.Length; i++)
            {
                invertedBinaryNumber[i] = '0';
                if (binaryNumber[i] == '0')
                {
                    invertedBinaryNumber[i] = '1';
                }
            }

            return new string(invertedBinaryNumber);
        }

        private string PruneRatings(string[] diagnosticReport, bool mostCommonValue)
        {
            int binaryNumberLength = diagnosticReport[0].Length;

            Dictionary<int, string> ratings = diagnosticReport.ToDictionary(x => Array.IndexOf(diagnosticReport, x));

            for (int position = 0; position < binaryNumberLength; position++)
            {
                int bitsEqualToOneCount = CountBitsEqualToOneForPosition(ratings.Values.ToArray(), position);
                var halfRatings = (float)ratings.Count / 2;

                if (bitsEqualToOneCount >= halfRatings)
                {
                    char removeBit = mostCommonValue ? '0' : '1';

                    foreach (int i in ratings.Keys)
                    {
                        if (ratings[i][position] == removeBit)
                        {
                            ratings.Remove(i);
                        }
                    }
                }
                else
                {
                    char removeBit = mostCommonValue ? '1' : '0';

                    foreach (int i in ratings.Keys)
                    {
                        if (ratings[i][position] == removeBit)
                        {
                            ratings.Remove(i);
                        }
                    }
                }

                if (ratings.Count == 1)
                {
                    break;
                }
            }

            return ratings.First().Value;
        }
    }
}
