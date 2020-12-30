/*
--- Part Two ---

Neat, right? You might also enjoy hearing John Conway talking about this
sequence (that's Conway of Conway's Game of Life fame).

Now, starting again with the digits in your puzzle input, apply this process 50
times. What is the length of the new result?
*/

namespace App.Tasks.Year2015.Day10
{
    public class Part2 : ITask<int>
    {
        private const int REPETITIONS = 50;

        private readonly LookAndSay lookAndSay;

        public Part2()
        {
            lookAndSay = new LookAndSay();
        }

        public int Solution(string input)
        {
            int resultSequenceLength = lookAndSay.ResultSequenceLength(input, REPETITIONS);

            return resultSequenceLength;
        }
    }
}
