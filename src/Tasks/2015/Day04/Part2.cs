/*
--- Part Two ---

Now find one that starts with six zeroes.
*/

namespace App.Tasks.Year2015.Day4
{
    public class Part2 : ITask<int>
    {
        private const string HashStartsWithPrefix = "000000";

        private readonly PrefixHashSolution prefixHashSolution;

        public Part2()
        {
            prefixHashSolution = new PrefixHashSolution();
        }

        public int Solution(string secretKey)
        {
            return prefixHashSolution.FindIntegerWhichGivesMd5HashWithPrefix(secretKey, HashStartsWithPrefix);
        }
    }
}
