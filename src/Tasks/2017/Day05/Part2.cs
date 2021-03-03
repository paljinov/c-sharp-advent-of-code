/*
--- Part Two ---

Now, the jumps are even stranger: after each jump, if the offset was three or
more, instead decrease it by 1. Otherwise, increase it by 1 as before.

Using this rule with the above example, the process now takes 10 steps, and the
offset values after finding the exit are left as 2 3 2 3 -1.

How many steps does it now take to reach the exit?
*/

namespace App.Tasks.Year2017.Day5
{
    public class Part2 : ITask<int>
    {
        private readonly JumpOffsetsRepository jumpOffsetsRepository;

        private readonly Jump jump;

        public Part2()
        {
            jumpOffsetsRepository = new JumpOffsetsRepository();
            jump = new Jump();
        }

        public int Solution(string input)
        {
            int[] jumpOffsets = jumpOffsetsRepository.GetJumpOffsets(input);
            int steps = jump.CountStepsToReachExitForDecreaseCondition(jumpOffsets);

            return steps;
        }
    }
}
