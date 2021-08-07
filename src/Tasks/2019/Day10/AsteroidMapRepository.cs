using System;

namespace App.Tasks.Year2019.Day10
{
    public class AsteroidMapRepository
    {
        private const char ASTEROID = '#';

        public bool[,] GetAsteroidMap(string input)
        {
            string[] asteroidMapString = input.Split(Environment.NewLine);

            int rows = asteroidMapString.Length;
            int columns = asteroidMapString[0].Length;

            bool[,] asteroidMap = new bool[columns, rows];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (asteroidMapString[y][x] == ASTEROID)
                    {
                        asteroidMap[x, y] = true;
                    }
                }
            }

            return asteroidMap;
        }
    }
}
