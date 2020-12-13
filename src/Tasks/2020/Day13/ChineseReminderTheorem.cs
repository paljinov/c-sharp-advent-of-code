using System.Linq;

namespace App.Tasks.Year2020.Day13
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Chinese_remainder_theorem
    /// https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
    /// </summary>
    public class ChineseReminderTheorem
    {
        public long Algorithm(long[] n, long[] a)
        {
            long prod = n.Aggregate((i, j) => i * j);
            long p;
            long sm = 0;
            for (int i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }

            return sm % prod;
        }

        private long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }

            return 1;
        }
    }
}
