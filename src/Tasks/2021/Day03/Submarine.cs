using System;

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

        private int[] CountBitsEqualToOne(string[] diagnosticReport)
        {
            int[] bitsEqualToOne = new int[diagnosticReport[0].Length];

            for (int i = 0; i < diagnosticReport.Length; i++)
            {
                string binaryBumber = diagnosticReport[i];
                for (int j = 0; j < binaryBumber.Length; j++)
                {
                    if (binaryBumber[j] == '1')
                    {
                        bitsEqualToOne[j]++;
                    }
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
    }
}
