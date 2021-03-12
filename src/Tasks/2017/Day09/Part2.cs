/*
--- Part Two ---

Now, you're ready to remove the garbage.

To prove you've removed it, you need to count all of the characters within the
garbage. The leading and trailing < and > don't count, nor do any canceled
characters or the ! doing the canceling.

- <>, 0 characters.
- <random characters>, 17 characters.
- <<<<>, 3 characters.
- <{!>}>, 2 characters.
- <!!>, 0 characters.
- <!!!>>, 0 characters.
- <{o"i!a,<{i<a>, 10 characters.

How many non-canceled characters are within the garbage in your puzzle input?
*/

namespace App.Tasks.Year2017.Day9
{
    public class Part2 : ITask<int>
    {
        private readonly ProcessStream processStream;

        public Part2()
        {
            processStream = new ProcessStream();
        }

        public int Solution(string input)
        {
            int nonCanceledCharactersWithinGarbage = processStream.CalculateNonCanceledCharactersWithinGarbage(input);

            return nonCanceledCharactersWithinGarbage;
        }
    }
}
