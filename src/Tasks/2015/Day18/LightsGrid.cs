namespace App.Tasks.Year2015.Day18
{
    public class LightsGrid
    {
        private readonly int steps = 100;

        public int CalculateLightsOnAfterAnimation(bool[,] lightsGrid, bool cornerLightsStuckOn = false)
        {
            lightsGrid = Animate(lightsGrid, cornerLightsStuckOn);

            // Count lights which are on
            int lightsOn = 0;

            for (int i = 0; i < lightsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < lightsGrid.GetLength(1); j++)
                {
                    bool light = lightsGrid[i, j];
                    if (light)
                    {
                        lightsOn++;
                    }
                }
            }

            return lightsOn;
        }

        private bool[,] Animate(bool[,] lightsGrid, bool cornerLightsStuckOn)
        {
            for (int i = 0; i < steps; i++)
            {
                lightsGrid = MakeAnimationStep(lightsGrid, cornerLightsStuckOn);
            }

            return lightsGrid;
        }

        private bool[,] MakeAnimationStep(bool[,] lightsGrid, bool cornerLightsStuckOn)
        {
            int rows = lightsGrid.GetLength(0);
            int columns = lightsGrid.GetLength(1);
            bool[,] lightsAfterStep = new bool[rows, columns];

            // Iterate lights grid
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (cornerLightsStuckOn)
                    {
                        // If it is corner light, it is stuck on
                        if ((i == 0 && j == 0) || (i == 0 && j == columns - 1)
                            || (i == rows - 1 && j == 0) || (i == rows - 1 && j == columns - 1))
                        {
                            lightsAfterStep[i, j] = true;
                            continue;
                        }
                    }

                    bool light = lightsGrid[i, j];
                    lightsAfterStep[i, j] = light;

                    int turnedOnNeighbors = CountTurnedOnNeighbors(i, j, lightsGrid);

                    // A light which is on stays on when 2 or 3 neighbors are on, and turns off otherwise
                    if (light && !(turnedOnNeighbors == 2 || turnedOnNeighbors == 3))
                    {
                        lightsAfterStep[i, j] = false;
                    }
                    // A light which is off turns on if exactly 3 neighbors are on, and stays off otherwise
                    else if (!light && turnedOnNeighbors == 3)
                    {
                        lightsAfterStep[i, j] = true;
                    }
                }
            }

            return lightsAfterStep;
        }

        private int CountTurnedOnNeighbors(int i, int j, bool[,] lightsGrid)
        {
            int rows = lightsGrid.GetLength(0);
            int columns = lightsGrid.GetLength(1);

            int turnedOnNeighbors = 0;

            // Count turned on neighbors
            for (int k = i - 1; k <= i + 1; k++)
            {
                for (int h = j - 1; h <= j + 1; h++)
                {
                    // If neighbor exists and is not current light itself
                    if (!(k == i && h == j) && k >= 0 && k <= (rows - 1) && h >= 0 && h <= (columns - 1))
                    {
                        if (lightsGrid[k, h])
                        {
                            turnedOnNeighbors++;
                        }
                    }
                }
            }

            return turnedOnNeighbors;
        }
    }
}
