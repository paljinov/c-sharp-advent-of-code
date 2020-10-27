namespace App.Tasks.Year2015.Day18
{
    class LightsGrid
    {
        public const int Steps = 100;

        public bool[,] Animate(bool[,] lightsGrid)
        {
            for (int i = 0; i < Steps; i++)
            {
                lightsGrid = MakeStep(lightsGrid);
            }

            return lightsGrid;
        }

        private bool[,] MakeStep(bool[,] lightsGrid)
        {
            int rows = lightsGrid.GetLength(0);
            int columns = lightsGrid.GetLength(1);
            bool[,] lightsAfterStep = new bool[rows, columns];

            // Iterate lights grid
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    bool light = lightsGrid[i, j];
                    lightsAfterStep[i, j] = light;

                    int onNeighbors = 0;
                    int offNeighbors = 0;

                    // Count neighbors with on and off state
                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int h = j - 1; h <= j + 1; h++)
                        {
                            // If neighbor exists and is not current light itself
                            if (!(k == i && h == j) && k >= 0 && k <= (rows - 1) && h >= 0 && h <= (columns - 1))
                            {
                                if (lightsGrid[k, h])
                                {
                                    onNeighbors++;
                                }
                                else
                                {
                                    offNeighbors++;
                                }
                            }
                        }
                    }

                    // A light which is on stays on when 2 or 3 neighbors are on, and turns off otherwise
                    if (light && !(onNeighbors == 2 || onNeighbors == 3))
                    {
                        lightsAfterStep[i, j] = false;
                    }
                    // A light which is off turns on if exactly 3 neighbors are on, and stays off otherwise
                    else if (!light && onNeighbors == 3)
                    {
                        lightsAfterStep[i, j] = true;
                    }
                }
            }

            return lightsAfterStep;
        }
    }
}
