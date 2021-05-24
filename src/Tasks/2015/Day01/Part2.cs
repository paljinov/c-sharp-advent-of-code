/*
--- Part Two ---

Now, given the same instructions, find the position of the first character that
causes him to enter the basement (floor -1). The first character in the
instructions has position 1, the second character has position 2, and so on.

For example:

- ) causes him to enter the basement at character position 1.
- ()()) causes him to enter the basement at character position 5.

What is the position of the character that causes Santa to first enter the
basement?
*/

namespace App.Tasks.Year2015.Day1
{

    public class Part2 : ITask<int>
    {
        private readonly Floor floor;

        public Part2()
        {
            floor = new Floor();
        }

        public int Solution(string input)
        {
            (_, int stepsToEnterBasementFirstTime) = floor.FindResultFloorAndStepsToEnterBasementFirstTime(input);

            return stepsToEnterBasementFirstTime;
        }
    }
}
