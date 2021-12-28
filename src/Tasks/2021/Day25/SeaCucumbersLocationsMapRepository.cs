using System;

namespace App.Tasks.Year2021.Day25
{
    public class SeaCucumbersLocationsMapRepository
    {
        private const char MOVES_EAST = '>';

        private const char MOVES_SOUTH = 'v';

        public SeaCucumberLocation[,] GetSeaCucumbersLocationsMap(string input)
        {
            string[] seaCucumbersLocationsMapString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = seaCucumbersLocationsMapString.Length;
            int columns = seaCucumbersLocationsMapString[0].Length;
            SeaCucumberLocation[,] seaCucumbersLocationsMap = new SeaCucumberLocation[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    SeaCucumberLocation seaCucumberLocation = SeaCucumberLocation.Empty;
                    switch (seaCucumbersLocationsMapString[i][j])
                    {
                        case MOVES_EAST:
                            seaCucumberLocation = SeaCucumberLocation.MovesEast;
                            break;
                        case MOVES_SOUTH:
                            seaCucumberLocation = SeaCucumberLocation.MovesSouth;
                            break;
                    }

                    seaCucumbersLocationsMap[i, j] = seaCucumberLocation;
                }
            }

            return seaCucumbersLocationsMap;
        }
    }
}
