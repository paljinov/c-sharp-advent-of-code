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
    class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            int floor = 0;

            for (int i = 0; i <= input.Length; i++)
            {
                char parenthesis = input[i];

                if (parenthesis == '(')
                {
                    floor += 1;
                }
                else
                {
                    floor -= 1;
                }

                if (floor == -1)
                {
                    return i + 1;
                }
            }

            return 0;
        }
    }
}
