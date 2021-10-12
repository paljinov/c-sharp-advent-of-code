using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day20
{
    public class MazeMapRepository
    {
        private const char EMPTY_SPACE = ' ';

        private const char OPEN_PASSAGE = '.';

        private const char SOLID_WALL = '#';

        public MazeElement[,] GetMazeMap(string input)
        {
            char[] mapMazeCharacters = { EMPTY_SPACE, OPEN_PASSAGE, SOLID_WALL };
            string[] mazeMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<(int, int), MazeElement> mazeMapDictionary = new Dictionary<(int, int), MazeElement>();

            int x = 0;
            for (int i = 0; i < mazeMapString.Length; i++)
            {
                int y = 0;
                bool hasPassageOrWall = false;
                for (int j = 0; j < mazeMapString[i].Length; j++)
                {
                    if (!hasPassageOrWall && (mazeMapString[i][j] == OPEN_PASSAGE || mazeMapString[i][j] == SOLID_WALL))
                    {
                        hasPassageOrWall = true;
                    }

                    if (hasPassageOrWall && (mapMazeCharacters.Contains(mazeMapString[i][j])
                        || char.IsLetter(mazeMapString[i][j])))
                    {
                        switch (mazeMapString[i][j])
                        {
                            case OPEN_PASSAGE:
                                mazeMapDictionary[(x, y)] = MazeElement.OpenPassage;
                                break;
                            case SOLID_WALL:
                                mazeMapDictionary[(x, y)] = MazeElement.SolidWall;
                                break;
                            default:
                                mazeMapDictionary[(x, y)] = MazeElement.EmptySpace;
                                break;
                        }

                        y++;
                    }
                }

                if (y > 0)
                {
                    y--;
                    // Remove trailing empty spaces
                    while (mazeMapDictionary[(x, y)] == MazeElement.EmptySpace)
                    {
                        mazeMapDictionary.Remove((x, y));
                        y--;
                    }
                }

                if (mazeMapDictionary.Count > 0)
                {
                    x++;
                }
            }

            int rows = mazeMapDictionary.Max(m => m.Key.Item1) + 1;
            int columns = mazeMapDictionary.Max(m => m.Key.Item2) + 1;

            MazeElement[,] mazeMap = new MazeElement[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    mazeMap[i, j] = mazeMapDictionary.ContainsKey((i, j))
                        ? mazeMapDictionary[(i, j)] : MazeElement.EmptySpace;
                }
            }

            return mazeMap;
        }

        public Dictionary<(int x, int y), string> GetPortals(string input)
        {
            Dictionary<(int x, int y), string> portals = new Dictionary<(int x, int y), string>();

            string[] mazeMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            MazeElement[,] mazeMap = GetMazeMap(input);

            string firstRow = string.Empty;
            for (int j = 0; j < mazeMapString[0].Length; j++)
            {
                firstRow += mazeMapString[0][j];
            }

            string firstColumn = string.Empty;
            for (int i = 0; i < mazeMapString[0].Length; i++)
            {
                firstColumn += mazeMapString[i][0];
            }

            string firstColumnMazeMap = string.Empty;
            for (int i = 0; i < mazeMap.GetLength(0); i++)
            {
                firstColumnMazeMap += mazeMap[i, 0];
            }

            int leftOffset = firstColumn != firstColumnMazeMap ? 2 : 0;
            int topOffset = mazeMapString[0].Contains(firstRow) ? 2 : 0;

            for (int i = 0; i < mazeMapString.Length; i++)
            {
                for (int j = 0; j < mazeMapString[i].Length; j++)
                {
                    if (char.IsLetter(mazeMapString[i][j]))
                    {
                        string portal = mazeMapString[i][j].ToString();
                        int x;
                        int y;

                        // Right
                        if (j + 1 < mazeMapString[i].Length && char.IsLetter(mazeMapString[i][j + 1]))
                        {
                            portal += mazeMapString[i][j + 1];
                            x = i - topOffset;
                            y = j == 0 ? 0 : j + 2 - leftOffset;
                        }
                        // Bottom
                        else if (i + 1 < mazeMapString.Length && j < mazeMapString[i + 1].Length
                            && char.IsLetter(mazeMapString[i + 1][j]))
                        {
                            portal += mazeMapString[i + 1][j];
                            // If open space is below portal
                            if (i + 2 < mazeMapString.Length && mazeMapString[i + 2][j] == OPEN_PASSAGE)
                            {
                                x = i == 0 ? 0 : i + 2 - topOffset;
                            }
                            // If open space is above portal
                            else
                            {
                                x = i == 0 ? 0 : i - 1 - topOffset;
                            }

                            y = j - leftOffset;
                        }
                        // If it is not valid portal
                        else
                        {
                            continue;
                        }

                        portals.Add((x, y), portal);
                    }
                }
            }

            return portals;
        }
    }
}
