using System;

namespace App.Tasks.Year2017.Day5
{
    public class JumpOffsetsRepository
    {
        public int[] GetJumpOffsets(string input)
        {
            string[] jumpOffsetsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int[] jumpOffsets = new int[jumpOffsetsString.Length];

            for (int i = 0; i < jumpOffsetsString.Length; i++)
            {
                jumpOffsets[i] = int.Parse(jumpOffsetsString[i]);
            }

            return jumpOffsets;
        }
    }
}
