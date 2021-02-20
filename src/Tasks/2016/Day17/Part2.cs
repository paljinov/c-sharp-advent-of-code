/*
--- Part Two ---

You're curious how robust this security solution really is, and so you decide to
find longer and longer paths which still provide access to the vault. You
remember that paths always end the first time they reach the bottom-right room
(that is, they can never pass through it, only end in it).

For example:

- If your passcode were ihgpwlah, the longest path would take 370 steps.
- With kglvqrro, the longest path would be 492 steps long.
- With ulqzkmiv, the longest path would be 830 steps long.

What is the length of the longest path that reaches the vault?
*/

namespace App.Tasks.Year2016.Day17
{
    public class Part2 : ITask<int>
    {
        private readonly Path path;

        public Part2()
        {
            path = new Path();
        }

        public int Solution(string input)
        {
            int lengthOfLongestPathToVault = path.FindLengthOfLongestPathToVault(input);

            return lengthOfLongestPathToVault;
        }
    }
}
