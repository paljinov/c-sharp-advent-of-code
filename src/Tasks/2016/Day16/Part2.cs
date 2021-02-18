/*
--- Part Two ---

The second disk you have to fill has length 35651584. Again using the initial
state in your puzzle input, what is the correct checksum for this disk?
*/

namespace App.Tasks.Year2016.Day16
{
    public class Part2 : ITask<string>
    {
        private readonly int diskLength = 35651584;

        private readonly Checksum checksum;

        public Part2()
        {
            checksum = new Checksum();
        }

        public string Solution(string input)
        {
            string checksum = this.checksum.CalculateChecksum(input, diskLength);

            return checksum;
        }
    }
}
