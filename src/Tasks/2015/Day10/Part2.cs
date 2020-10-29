/*
--- Part Two ---

Neat, right? You might also enjoy hearing John Conway talking about this
sequence (that's Conway of Conway's Game of Life fame).

Now, starting again with the digits in your puzzle input, apply this process 50
times. What is the length of the new result?
*/

namespace App.Tasks.Year2015.Day10
{
    class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            string resultSequence = input;
            for (int i = 0; i < 50; i++)
            {
                resultSequence = LookAndSay.GenerateSequence(resultSequence);
            }

            return resultSequence.Length;
        }
    }
}
