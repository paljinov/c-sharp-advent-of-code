using System;

namespace App.Tasks.Year2017.Day1
{
    public class Captcha
    {
        public int Solve(string digitsSequence, bool matchHalfway = false)
        {
            int sum = 0;

            int offset = 1;
            if (matchHalfway)
            {
                offset = (int)Math.Floor((double)(digitsSequence.Length / 2));
            }

            for (int i = 0; i < digitsSequence.Length; i++)
            {
                int nextIndex = i + offset;
                if (nextIndex >= digitsSequence.Length)
                {
                    nextIndex %= digitsSequence.Length;
                }

                int current = (int)char.GetNumericValue(digitsSequence[i]);
                int next = (int)char.GetNumericValue(digitsSequence[nextIndex]);

                if (current == next)
                {
                    sum += current;
                }
            }

            return sum;
        }
    }
}
