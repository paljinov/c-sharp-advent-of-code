using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day20
{
    public class MazeMapRepository
    {
        public char[,] GetMazeMap(string input)
        {
            string[] mazeMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = mazeMapString.Length;
            int columns = mazeMapString.OrderByDescending(s => s.Length).First().Length;

            char[,] mazeMap = new char[rows, columns];

            for (int i = 0; i < mazeMapString.Length; i++)
            {
                for (int j = 0; j < mazeMapString[i].Length; j++)
                {
                    mazeMap[i, j] = mazeMapString[i][j];
                }
            }

            return mazeMap;
        }

        public Dictionary<string, PortalPair> GetPortalPairs(string input)
        {
            Dictionary<string, PortalPair> portalPairs = new Dictionary<string, PortalPair>();

            string[] mazeMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < mazeMapString.Length; i++)
            {
                for (int j = 0; j < mazeMapString[i].Length; j++)
                {
                    if (char.IsLetter(mazeMapString[i][j]))
                    {
                        string portal = mazeMapString[i][j].ToString();
                        bool outer = false;
                        int x;
                        int y;

                        // Right
                        if (j + 1 < mazeMapString[i].Length && char.IsLetter(mazeMapString[i][j + 1]))
                        {
                            portal += mazeMapString[i][j + 1];
                            x = i;
                            y = j + 2;

                            if (j - 1 < 0)
                            {
                                outer = true;
                            }
                        }
                        // Bottom
                        else if (i + 1 < mazeMapString.Length && char.IsLetter(mazeMapString[i + 1][j]))
                        {
                            portal += mazeMapString[i + 1][j];
                            x = i + 2;
                            y = j;

                            if (i - 1 < 0)
                            {
                                outer = true;
                            }
                        }
                        // If it is not valid portal
                        else
                        {
                            continue;
                        }

                        if (!portalPairs.ContainsKey(portal))
                        {
                            portalPairs.Add(portal, new PortalPair
                            {
                                Inner = (int.MinValue, int.MinValue),
                                Outer = (int.MinValue, int.MinValue)
                            });
                        }

                        if (!outer)
                        {
                            portalPairs[portal].Inner = (x, y);
                        }
                        else
                        {
                            portalPairs[portal].Outer = (x, y);
                        }
                    }
                }
            }

            return portalPairs;
        }

        private HashSet<(int, int)> GetPortalLocations(string input)
        {
            HashSet<(int, int)> portalLocations = new HashSet<(int, int)>();

            Dictionary<string, PortalPair> portalPairs = GetPortalPairs(input);
            foreach (KeyValuePair<string, PortalPair> portalPair in portalPairs)
            {
                if (portalPair.Value.Inner.X != int.MinValue)
                {
                    portalLocations.Add((portalPair.Value.Inner.X, portalPair.Value.Inner.Y));
                }
                if (portalPair.Value.Outer.X != int.MinValue)
                {
                    portalLocations.Add((portalPair.Value.Outer.X, portalPair.Value.Outer.Y));
                }
            }

            return portalLocations;
        }
    }
}
