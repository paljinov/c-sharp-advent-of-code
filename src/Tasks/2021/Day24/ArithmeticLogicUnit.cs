using System;
using System.Linq;

namespace App.Tasks.Year2021.Day24
{
    public class ArithmeticLogicUnit
    {
        private const int SUBMARINE_MODEL_NUMBERS_LENGTH = 14;

        private const int OPTYPE_INDEX = 4;

        private const int CORRECTION_INDEX = 5;

        private const int OFFSET_INDEX = 15;

        private const int INSTRUCTIONS_BATCH = 18;

        private const int SKIP = 6;

        public long CalculateLargestModelNumberAcceptedByMonad(string[] instructions)
        {
            (long largestModelNumberAcceptedByMonad, _) = CalculateModelNumberAcceptedByMonad(instructions);

            return largestModelNumberAcceptedByMonad;
        }

        public long CalculateSmallestModelNumberAcceptedByMonad(string[] instructions)
        {
            (_, long smallestModelNumberAcceptedByMonad) = CalculateModelNumberAcceptedByMonad(instructions);

            return smallestModelNumberAcceptedByMonad;
        }

        public (long LargestNumber, long SmallestNumber) CalculateModelNumberAcceptedByMonad(string[] instructions)
        {
            long largestModelNumber = long.MinValue;
            long smallestModelNumber = long.MaxValue;

            Bits[] parameters = GetParameters(instructions);

            long minNumber = MinWithXDigits(SUBMARINE_MODEL_NUMBERS_LENGTH);
            long maxNumber = MaxWithXDigits(SUBMARINE_MODEL_NUMBERS_LENGTH);
            for (long number = minNumber; number <= maxNumber; number++)
            {
                int[] digits = GetDigits(number);
                long z = 0;

                for (int i = 0; i < parameters.Length; i++)
                {
                    Bits bits = parameters[i];

                    int w = digits[i];
                    bool corrected = (z % 26) + bits.Correction == w;

                    if (w != 0 && bits.Optype == 26 && corrected)
                    {
                        z /= bits.Optype;
                    }
                    else if (w != 0 && bits.Optype == 1 && !corrected)
                    {
                        z = 26 * (z / bits.Optype) + w + bits.Offset;
                    }
                    else
                    {
                        number += (long)Math.Pow(10, SUBMARINE_MODEL_NUMBERS_LENGTH - (i + 1)) - 1;
                        break;
                    }
                }

                if (z == 0)
                {
                    largestModelNumber = Math.Max(number, largestModelNumber);
                    smallestModelNumber = Math.Min(number, smallestModelNumber);
                }
            }

            return (largestModelNumber, smallestModelNumber);
        }

        private Bits[] GetParameters(string[] instructions)
        {
            Bits[] parameters = new Bits[SUBMARINE_MODEL_NUMBERS_LENGTH];
            for (int i = 0; i < SUBMARINE_MODEL_NUMBERS_LENGTH; i++)
            {
                Bits bits = new Bits
                {
                    Optype = int.Parse(new string(instructions[OPTYPE_INDEX + i * INSTRUCTIONS_BATCH])
                        .Skip(SKIP).ToArray()),
                    Correction = int.Parse(new string(instructions[CORRECTION_INDEX + i * INSTRUCTIONS_BATCH])
                        .Skip(SKIP).ToArray()),
                    Offset = int.Parse(new string(instructions[OFFSET_INDEX + i * INSTRUCTIONS_BATCH])
                        .Skip(SKIP).ToArray())
                };

                parameters[i] = bits;
            }

            return parameters;
        }

        private long MaxWithXDigits(int digits)
        {
            return Convert.ToInt64(Math.Pow(10, digits) - 1);
        }

        private long MinWithXDigits(int digits)
        {
            return Convert.ToInt64(Math.Pow(10, digits - 1));
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
