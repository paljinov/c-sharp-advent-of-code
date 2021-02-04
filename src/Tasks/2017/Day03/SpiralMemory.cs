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
            int squareSideSize = CalculateSquareSideSize(inputSquare);

            int accessPortX, accessPortY;
            accessPortX = accessPortY = (squareSideSize - 1) / 2;
            int x = accessPortX;
            int y = accessPortY;
            int inputSquareX, inputSquareY;
            inputSquareX = inputSquareY = -1;

            int sideSize = 1;
            IEnumerable<Direction> directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();

            int currentSquare = ACCESS_PORT;
            if (currentSquare == inputSquare)
            {
                inputSquareX = x;
                inputSquareY = y;
            }

            while (inputSquareX == -1 && inputSquareY == -1)
            {
                // Step right to the outer square side
                x++;
                sideSize += 2;

                currentSquare++;
                if (currentSquare == inputSquare)
                {
                    inputSquareX = x;
                    inputSquareY = y;
                }

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
                    }
                }
            }

            int manhattanDistance = Math.Abs(inputSquareX - accessPortX) + Math.Abs(inputSquareY - accessPortY);

            return manhattanDistance;
        }

        public int CalculateFirstValueLargerThanInputSquare(int inputSquare)
        {
            int squareSideSize = CalculateSquareSideSize(inputSquare);
            int[,] spiralMemory = new int[squareSideSize, squareSideSize];

            int accessPortX, accessPortY;
            accessPortX = accessPortY = (squareSideSize - 1) / 2;
            int x = accessPortX;
            int y = accessPortY;

            int sideSize = 1;
            IEnumerable<Direction> directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();

            spiralMemory[x, y] = ACCESS_PORT;
            int firstValueLargerThanInputSquare = -1;

            while (firstValueLargerThanInputSquare == -1)
            {
                // Step right to the outer square side
                x++;
                sideSize += 2;

                spiralMemory[x, y] = CalculateSquareValueBasedOnNeighbours(x, y, spiralMemory);
                if (firstValueLargerThanInputSquare == -1 && spiralMemory[x, y] > inputSquare)
                {
                    firstValueLargerThanInputSquare = spiralMemory[x, y];
                }

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

                        spiralMemory[x, y] = CalculateSquareValueBasedOnNeighbours(x, y, spiralMemory);
                        if (firstValueLargerThanInputSquare == -1 && spiralMemory[x, y] > inputSquare)
                        {
                            firstValueLargerThanInputSquare = spiralMemory[x, y];
                        }
                    }
                }
            }

            return firstValueLargerThanInputSquare;
        }

        private int CalculateSquareSideSize(int inputSquare)
        {
            int size = (int)Math.Ceiling(Math.Sqrt(inputSquare));
            if (size % 2 == 0)
            {
                size++;
            }

            return size;
        }

        private int CalculateSquareValueBasedOnNeighbours(int x, int y, int[,] spiralMemory)
        {
            int sum = 0;
            int squareSideSize = spiralMemory.GetLength(0);

            // Right
            if (x + 1 < squareSideSize)
            {
                sum += spiralMemory[x + 1, y];
            }
            // Top-right
            if (x + 1 < squareSideSize && y - 1 >= 0)
            {
                sum += spiralMemory[x + 1, y - 1];
            }
            // Top
            if (y - 1 >= 0)
            {
                sum += spiralMemory[x, y - 1];
            }
            // Top-left
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                sum += spiralMemory[x - 1, y - 1];
            }
            // Left
            if (x - 1 >= 0)
            {
                sum += spiralMemory[x - 1, y];
            }
            // Down-left
            if (x - 1 >= 0 && y + 1 < squareSideSize)
            {
                sum += spiralMemory[x - 1, y + 1];
            }
            // Down
            if (y + 1 < squareSideSize)
            {
                sum += spiralMemory[x, y + 1];
            }
            // Down-right
            if (x + 1 < squareSideSize && y + 1 < squareSideSize)
            {
                sum += spiralMemory[x + 1, y + 1];
            }

            return sum;
        }
    }
}
