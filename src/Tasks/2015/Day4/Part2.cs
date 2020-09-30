/*
--- Part Two ---

Now find one that starts with six zeroes.
*/

namespace App.Tasks.Year2015.Day4
{
    class Part2 : ITask<int>
    {
        public int Solution(string secretKey)
        {
           return PrefixHashSolution.FindIntegerWhichGivesMd5HashWithPrefix(secretKey, "000000");
        }
    }
}
