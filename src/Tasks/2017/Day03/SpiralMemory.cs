using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day3
{
    public class SpiralMemory
    {
        private const int ACCESS_PORT = 1;

        public int CalculateManhattanDistanceFromSquareToAccessPort(int inputSquare)
        {
            int size = (int)Math.Ceiling(Math.Sqrt(inputSquare));
            if (size % 2 == 0)
            {
                size++;
            }

            int[,] spiralMemory = new int[size, size];

            IEnumerable<Direction> directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();

            int accessPortX = (int)((size - 1) / 2);
            int accessPortY = (int)((size - 1) / 2);
            int inputSquareX = 0;
            int inputSquareY = 0;
            int x = accessPortX;
            int y = accessPortY;

            int currentSquare = ACCESS_PORT;
            spiralMemory[x, y] = currentSquare;
            int sideSize = 1;

            while (spiralMemory[size - 1, size - 1] == 0)
            {
                // Step right to the outer square side
                x++;

                currentSquare++;
                if (currentSquare == inputSquare)
                {
                    inputSquareX = x;
                    inputSquareY = y;
                }

                spiralMemory[x, y] = currentSquare;
                sideSize += 2;

                foreach (Direction direction in directions)
                {
                    int sideSquares = sideSize - 1;
                    if (direction == Direction.UP)
                    {
                        sideSquares--;
                    }

                    for (int i = 0; i < sideSquares; i++)
                    {
                        switch (direction)
                        {
                            case Direction.UP:
                                y++;
                                break;
                            case Direction.LEFT:
                                x--;
                                break;
                            case Direction.DOWN:
                                y--;
                                break;
                            case Direction.RIGHT:
                                x++;
                                break;
                        }

                        currentSquare++;
                        if (currentSquare == inputSquare)
                        {
                            inputSquareX = x;
                            inputSquareY = y;
                        }

                        spiralMemory[x, y] = currentSquare;
                    }
                }
            }

            int manhattanDistance = Math.Abs(inputSquareX - accessPortX) + Math.Abs(inputSquareY - accessPortY);

            return manhattanDistance;
        }
    }
}
