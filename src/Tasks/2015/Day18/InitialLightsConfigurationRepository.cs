using System;

namespace App.Tasks.Year2015.Day18
{
    class InitialLightsConfigurationRepository
    {
        public bool[,] ParseInput(string input)
        {
            string[] lightsGridString = input.Split(Environment.NewLine);

            bool[,] lightsGrid = new bool[lightsGridString.Length, lightsGridString[0].Length];
            for (int i = 0; i < lightsGridString.Length; i++)
            {
                for (int j = 0; j < lightsGridString[i].Length; j++)
                {
                    lightsGrid[i, j] = false;
                    if (lightsGridString[i][j] == '#')
                    {
                        lightsGrid[i, j] = true;
                    }
                }
            }

            return lightsGrid;
        }
    }
}
