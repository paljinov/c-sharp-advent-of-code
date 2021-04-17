using System.Collections.Generic;

namespace App.Tasks.Year2017.Day21
{
    public class Grid
    {
        private const char TURNED_ON_PIXEL = '#';

        public int CountTurnedOnPixels(char[,] gridOfPixels, Dictionary<char[,], char[,]> rules, int totalIterations)
        {
            for (int iteration = 0; iteration < totalIterations; iteration++)
            {
                int size = gridOfPixels.GetLength(0);

                int squaresPerSide;
                int smallSquareSize;
                if (size % 2 == 0)
                {
                    squaresPerSide = size / 2;
                    smallSquareSize = 2;
                }
                else
                {
                    squaresPerSide = size / 3;
                    smallSquareSize = 3;
                }

                char[,] enhancedGridOfPixels = new char[
                    squaresPerSide * (smallSquareSize + 1),
                    squaresPerSide * (smallSquareSize + 1)
                ];

                for (int i = 0; i < squaresPerSide; i++)
                {
                    for (int j = 0; j < squaresPerSide; j++)
                    {
                        char[,] smallSquare = new char[smallSquareSize, smallSquareSize];

                        int x = 0;
                        for (int k = i * smallSquareSize; k < i * smallSquareSize + smallSquareSize; k++)
                        {
                            int y = 0;
                            for (int h = j * smallSquareSize; h < j * smallSquareSize + smallSquareSize; h++)
                            {
                                smallSquare[x, y] = gridOfPixels[k, h];
                                y++;
                            }
                            x++;
                        }

                        char[,] smallSquareOutput = GetOutputAfterRule(smallSquare, rules);
                        int outputSquareSize = smallSquareOutput.GetLength(0);

                        for (x = 0; x < outputSquareSize; x++)
                        {
                            for (int y = 0; y < outputSquareSize; y++)
                            {
                                enhancedGridOfPixels[i * outputSquareSize + x, j * outputSquareSize + y] =
                                    smallSquareOutput[x, y];
                            }
                        }
                    }
                }

                gridOfPixels = enhancedGridOfPixels;
            }

            int turnedOnPixels = CountGridTurnedOnPixels(gridOfPixels);

            return turnedOnPixels;
        }

        private char[,] GetOutputAfterRule(char[,] input, Dictionary<char[,], char[,]> rules)
        {
            char[,] output;

            for (int i = 0; i < 4; i++)
            {
                output = GetOutputForMatchedRule(input, rules);
                if (output is not null)
                {
                    return output;
                }

                char[,] horizontallyFlippedGrid = FlipHorizontally(input);
                output = GetOutputForMatchedRule(horizontallyFlippedGrid, rules);
                if (output is not null)
                {
                    return output;
                }

                char[,] verticallyFlippedGrid = FlipVertically(input);
                output = GetOutputForMatchedRule(verticallyFlippedGrid, rules);
                if (output is not null)
                {
                    return output;
                }

                input = Rotate(input);
            }

            return null;
        }

        private char[,] GetOutputForMatchedRule(char[,] grid, Dictionary<char[,], char[,]> rules)
        {
            int size = grid.GetLength(0);

            foreach (KeyValuePair<char[,], char[,]> enhancementRule in rules)
            {
                int enhancementRuleSize = enhancementRule.Key.GetLength(0);

                if (size == enhancementRuleSize)
                {
                    bool ruleIsMatched = true;
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (grid[i, j] != enhancementRule.Key[i, j])
                            {
                                ruleIsMatched = false;
                                break;
                            }
                        }

                        if (!ruleIsMatched)
                        {
                            break;
                        }
                    }

                    if (ruleIsMatched)
                    {
                        return enhancementRule.Value;
                    }
                }
            }

            return null;
        }

        private char[,] FlipHorizontally(char[,] grid)
        {
            int size = grid.GetLength(0);

            char[,] horizontallyFlippedGrid = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = size - 1; j >= 0; j--)
                {
                    horizontallyFlippedGrid[i, size - 1 - j] = grid[i, j];
                }
            }

            return horizontallyFlippedGrid;
        }

        private char[,] FlipVertically(char[,] grid)
        {
            int size = grid.GetLength(0);

            char[,] verticallyFlippedGrid = new char[size, size];
            for (int i = 0; i < size / 2; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    verticallyFlippedGrid[i, j] = grid[size - 1 - i, j];
                    verticallyFlippedGrid[size - 1 - i, j] = grid[i, j];
                }
            }

            return verticallyFlippedGrid;
        }

        private char[,] Rotate(char[,] grid)
        {
            int size = grid.GetLength(0);

            char[,] rotatedGrid = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    rotatedGrid[i, j] = grid[size - j - 1, i];
                }
            }

            return rotatedGrid;
        }

        private int CountGridTurnedOnPixels(char[,] grid)
        {
            int turnedOnPixels = 0;

            int size = grid.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == TURNED_ON_PIXEL)
                    {
                        turnedOnPixels++;
                    }
                }
            }

            return turnedOnPixels;
        }
    }
}
