using System;

namespace App.Tasks.Year2021.Day1
{
    public class DepthsRepository
    {
        public int[] GetDepths(string input)
        {
            string[] depthsString = input.Split(Environment.NewLine);
            int[] depths = new int[depthsString.Length];

            for (int i = 0; i < depthsString.Length; i++)
            {
                int depth = int.Parse(depthsString[i]);
                depths[i] = depth;
            }

            return depths;
        }
    }
}
