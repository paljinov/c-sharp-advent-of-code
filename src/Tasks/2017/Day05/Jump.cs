namespace App.Tasks.Year2017.Day5
{
    public class Jump
    {
        public int CountStepsToReachExit(int[] jumpOffsets, bool decreaseCondition = false)
        {
            int steps = 0;
            int instruction = 0;

            while (instruction < jumpOffsets.Length)
            {
                int currentInstruction = instruction;

                if (jumpOffsets[instruction] != 0)
                {
                    instruction += jumpOffsets[instruction];
                }

                if (decreaseCondition && jumpOffsets[currentInstruction] >= 3)
                {
                    jumpOffsets[currentInstruction]--;
                }
                else
                {
                    jumpOffsets[currentInstruction]++;
                }

                steps++;
            }

            return steps;
        }
    }
}
