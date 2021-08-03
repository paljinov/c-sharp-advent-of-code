using System;

namespace App.Tasks.Year2019.Day10
{
    public class AsteroidMapRepository
    {
        private const char ASTEROID = '#';

        public bool[,] GetAsteroidMap(string input)
        {
            string[] asteroidMapString = input.Split(Environment.NewLine);

            bool[,] asteroidMap = new bool[asteroidMapString.Length, asteroidMapString[0].Length];

            for (int x = 0; x < asteroidMapString.Length; x++)
            {
                for (int y = 0; y < asteroidMapString[x].Length; y++)
                {
                    if (asteroidMapString[x][y] == ASTEROID)
                    {
                        asteroidMap[x, y] = true;
                    }
                }
            }

            return asteroidMap;
        }
    }
}
