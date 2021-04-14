using System.Collections.Generic;

namespace App.Tasks.Year2017.Day19
{
    public class FollowPath
    {
        private const char EMPTY = ' ';

        private const char VERTICAL_BAR = '|';

        public (string, int) FindSeenLettersAndCountStepsMadeByLittlePacket(char[,] routingDiagram)
        {
            List<char> letters = new List<char>();
            List<char> allLetters = GetAllLetters(routingDiagram);

            int steps = 1;
            (int i, int j) = FindStartCoordinate(routingDiagram);
            Direction direction = Direction.DOWN;

            while (letters.Count < allLetters.Count)
            {
                bool moveHorizontally = false;
                bool moveVertically = false;

                switch (direction)
                {
                    case Direction.UP:
                        if (i > 0 && routingDiagram[i - 1, j] != EMPTY)
                        {
                            i--;
                        }
                        else
                        {
                            moveHorizontally = true;
                        }
                        break;
                    case Direction.DOWN:
                        if (i < routingDiagram.GetLength(0) - 1 && routingDiagram[i + 1, j] != EMPTY)
                        {
                            i++;
                        }
                        else
                        {
                            moveHorizontally = true;
                        }
                        break;
                    case Direction.LEFT:
                        if (j > 0 && routingDiagram[i, j - 1] != EMPTY)
                        {
                            j--;
                        }
                        else
                        {
                            moveVertically = true;
                        }
                        break;
                    case Direction.RIGHT:
                        if (j < routingDiagram.GetLength(1) - 1 && routingDiagram[i, j + 1] != EMPTY)
                        {
                            j++;
                        }
                        else
                        {
                            moveVertically = true;
                        }
                        break;
                }

                if (moveHorizontally)
                {
                    if (routingDiagram[i, j - 1] != EMPTY)
                    {
                        j--;
                        direction = Direction.LEFT;
                    }
                    else
                    {
                        j++;
                        direction = Direction.RIGHT;
                    }
                }
                else if (moveVertically)
                {
                    if (routingDiagram[i - 1, j] != EMPTY)
                    {
                        i--;
                        direction = Direction.UP;
                    }
                    else
                    {
                        i++;
                        direction = Direction.DOWN;
                    }
                }

                if (allLetters.Contains(routingDiagram[i, j]))
                {
                    letters.Add(routingDiagram[i, j]);
                }

                steps++;
            }

            return (string.Join("", letters), steps);
        }

        private List<char> GetAllLetters(char[,] routingDiagram)
        {
            List<char> letters = new List<char>();

            for (int i = 0; i < routingDiagram.GetLength(0); i++)
            {
                for (int j = 0; j < routingDiagram.GetLength(1); j++)
                {
                    if (char.IsLetter(routingDiagram[i, j]))
                    {
                        letters.Add(routingDiagram[i, j]);
                    }
                }
            }

            return letters;
        }

        private (int, int) FindStartCoordinate(char[,] routingDiagram)
        {
            int x = 0;
            int y = 0;

            for (int j = 0; j < routingDiagram.GetLength(0); j++)
            {
                if (routingDiagram[x, j] == VERTICAL_BAR)
                {
                    y = j;
                    break;
                }
            }

            return (x, y);
        }
    }
}
