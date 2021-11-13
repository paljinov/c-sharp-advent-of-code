using System.Collections.Generic;

namespace App.Tasks.Year2018.Day18
{
    public class Resources
    {
        private const char OPEN_GROUND = '.';

        private const char TREES = '|';

        private const char LUMBERYARD = '#';

        public int CalculateTotalResourceValueOfTheLumberCollectionAreaAfterMinutes(char[,] area, int totalMinutes)
        {
            for (int minute = 0; minute < totalMinutes; minute++)
            {
                area = ChangeArea(area);
            }

            Dictionary<char, int> acres = CountAcres(area);
            int totalResourceValue = acres[TREES] * acres[LUMBERYARD];

            return totalResourceValue;
        }

        private char[,] ChangeArea(char[,] area)
        {
            char[,] changedArea = new char[area.GetLength(0), area.GetLength(0)];

            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area.GetLength(1); j++)
                {
                    Dictionary<char, int> adjacentAcres = CountAdjacentAcres((i, j), area);
                    changedArea[i, j] = area[i, j];

                    switch (changedArea[i, j])
                    {
                        case OPEN_GROUND:
                            if (adjacentAcres[TREES] >= 3)
                            {
                                changedArea[i, j] = TREES;
                            }
                            break;
                        case TREES:
                            if (adjacentAcres[LUMBERYARD] >= 3)
                            {
                                changedArea[i, j] = LUMBERYARD;
                            }
                            break;
                        case LUMBERYARD:
                            if (adjacentAcres[TREES] < 1 || adjacentAcres[LUMBERYARD] < 1)
                            {
                                changedArea[i, j] = OPEN_GROUND;
                            }
                            break;
                    }
                }
            }

            return changedArea;
        }

        private Dictionary<char, int> CountAdjacentAcres((int X, int Y) acre, char[,] area)
        {
            Dictionary<char, int> adjacentAcres = new Dictionary<char, int>
            {
                { OPEN_GROUND, 0 },
                { TREES, 0 },
                { LUMBERYARD, 0 }
            };

            // Top left
            if (acre.X - 1 >= 0 && acre.Y - 1 >= 0)
            {
                adjacentAcres[area[acre.X - 1, acre.Y - 1]]++;
            }
            // Top
            if (acre.X - 1 >= 0)
            {
                adjacentAcres[area[acre.X - 1, acre.Y]]++;
            }
            // Top right
            if (acre.X - 1 >= 0 && acre.Y + 1 < area.GetLength(1))
            {
                adjacentAcres[area[acre.X - 1, acre.Y + 1]]++;
            }
            // Right
            if (acre.Y + 1 < area.GetLength(1))
            {
                adjacentAcres[area[acre.X, acre.Y + 1]]++;
            }
            // Bottom right
            if (acre.X + 1 < area.GetLength(0) && acre.Y + 1 < area.GetLength(1))
            {
                adjacentAcres[area[acre.X + 1, acre.Y + 1]]++;
            }
            // Bottom
            if (acre.X + 1 < area.GetLength(0))
            {
                adjacentAcres[area[acre.X + 1, acre.Y]]++;
            }
            // Bottom left
            if (acre.X + 1 < area.GetLength(0) && acre.Y - 1 >= 0)
            {
                adjacentAcres[area[acre.X + 1, acre.Y - 1]]++;
            }
            // Left
            if (acre.Y - 1 >= 0)
            {
                adjacentAcres[area[acre.X, acre.Y - 1]]++;
            }

            return adjacentAcres;
        }

        private Dictionary<char, int> CountAcres(char[,] area)
        {
            Dictionary<char, int> acres = new Dictionary<char, int>
            {
                { OPEN_GROUND, 0 },
                { TREES, 0 },
                { LUMBERYARD, 0 }
            };

            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area.GetLength(1); j++)
                {
                    acres[area[i, j]]++;
                }
            }

            return acres;
        }
    }
}
