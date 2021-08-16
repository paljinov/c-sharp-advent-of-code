using System;
using System.Linq;

namespace App.Tasks.Year2019.Day12
{
    public class LcmHelper
    {
        public long CalculateLeastCommonMultiple(int[] integers)
        {
            if (integers.Length == 0)
            {
                return 0;
            }

            for (int i = 0; i < integers.Length; i++)
            {
                if (integers[i] == 0)
                {
                    return 0;
                }
            }

            long lcm = integers[0];
            for (int i = 1; i < integers.Length; i++)
            {
                lcm = CalculateLcm(lcm, integers[i]);
            }

            return lcm;
        }

        private long CalculateLcm(long a, long b)
        {
            return Math.Abs(a * b) / CalculateGreatestCommonDivisor(a, b);
        }

        private long CalculateGreatestCommonDivisor(long a, long b)
        {
            return b == 0 ? a : CalculateGreatestCommonDivisor(b, a % b);
        }
    }
}
