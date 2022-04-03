using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day24
{
    public class ArithmeticLogicUnit
    {
        private const int SUBMARINE_MODEL_NUMBERS_LENGTH = 14;

        public long CalculateLargestModelNumberAcceptedByMonad(string[] instructions)
        {
            (long largestModelNumberAcceptedByMonad, _) = CalculateModelNumberAcceptedByMonad(instructions);

            return largestModelNumberAcceptedByMonad;
        }

        public long CalculateSmallestModelNumberAcceptedByMonad(string[] instructions)
        {
            return instructions.Length;
        }

        public (long LargestNumber, long SmallestNumber) CalculateModelNumberAcceptedByMonad(string[] instructions)
        {
            Bits[] parameters = new Bits[SUBMARINE_MODEL_NUMBERS_LENGTH];
            for (int i = 0; i < SUBMARINE_MODEL_NUMBERS_LENGTH; i++)
            {
                Bits bits = new Bits
                {
                    Optype = int.Parse(new string(instructions[4 + i * 18]).Skip(6).ToArray()),
                    Correction = int.Parse(new string(instructions[5 + i * 18].Skip(6).ToArray())),
                    Offset = int.Parse(new string(instructions[15 + i * 18].Skip(6).ToArray()))
                };

                parameters[i] = bits;
            }

            (long smallestModelNumber, long largestModelNumber) = (long.MaxValue, long.MinValue);
            for (long number = 10000000000000; number <= 99999999999999; number++)
            {
                int[] digits = GetDigits(number);
                int step = 0;
                long z = 0;

                foreach (Bits bits in parameters)
                {
                    var w = digits[step];

                    if (w != 0 && bits.Optype == 26 && ((z % 26) + bits.Correction == w))
                    {
                        z /= bits.Optype;
                    }
                    else if (w != 0 && bits.Optype == 1 && ((z % 26) + bits.Correction != w))
                    {
                        z = 26 * (z / bits.Optype) + w + bits.Offset;
                    }
                    else
                    {
                        number += (long)Math.Pow(10, SUBMARINE_MODEL_NUMBERS_LENGTH - 1 - step) - 1;
                        break;
                    }

                    step++;
                }

                if (z == 0)
                {
                    (smallestModelNumber, largestModelNumber) = (
                        Math.Min(number, smallestModelNumber),
                        Math.Max(number, largestModelNumber)
                    );
                }
            }

            return (largestModelNumber, smallestModelNumber);
        }

        private int[] GetDigits(long number)
        {
            int[] digits = new int[SUBMARINE_MODEL_NUMBERS_LENGTH];

            string numberString = number.ToString();
            for (int i = 0; i < numberString.Length; i++)
            {
                digits[i] = (int)char.GetNumericValue(numberString[i]);
            }

            return digits;
        }
    }
}
