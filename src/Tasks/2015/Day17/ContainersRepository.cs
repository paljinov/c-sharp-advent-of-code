using System;

namespace App.Tasks.Year2015.Day17
{
    public class ContainersRepository
    {
        public int[] ParseInput(string input)
        {
            string[] containersString = input.Split(Environment.NewLine);

            int[] containers = new int[containersString.Length];
            for (int i = 0; i < containersString.Length; i++)
            {
                containers[i] = int.Parse(containersString[i]);
            }

            return containers;
        }
    }
}
