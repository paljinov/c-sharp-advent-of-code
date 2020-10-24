using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day1
{
    class CityStreetGrid
    {
        public List<(int, int)> CalculateVisitedLocations(string[] instructions)
        {
            List<(int, int)> visitedLocations = new List<(int, int)>();
            (int x, int y) location = (0, 0);

            CardinalDirection faceDirection = CardinalDirection.North;

            Regex instructionRegex = new Regex(@"^([RL]{1})(\d+)$");

            foreach (string instruction in instructions)
            {
                Match match = instructionRegex.Match(instruction);
                GroupCollection groups = match.Groups;

                char turn = char.Parse(groups[1].Value);
                int blocks = int.Parse(groups[2].Value);

                switch (faceDirection)
                {
                    case CardinalDirection.North:
                        if (turn == 'R')
                        {
                            location.x += blocks;
                            faceDirection = CardinalDirection.East;
                        }
                        else
                        {
                            location.x -= blocks;
                            faceDirection = CardinalDirection.West;
                        }
                        break;
                    case CardinalDirection.East:
                        if (turn == 'R')
                        {
                            location.y -= blocks;
                            faceDirection = CardinalDirection.South;
                        }
                        else
                        {
                            location.y += blocks;
                            faceDirection = CardinalDirection.North;
                        }
                        break;
                    case CardinalDirection.South:
                        if (turn == 'R')
                        {
                            location.x -= blocks;
                            faceDirection = CardinalDirection.West;
                        }
                        else
                        {
                            location.x += blocks;
                            faceDirection = CardinalDirection.East;
                        }
                        break;
                    case CardinalDirection.West:
                        if (turn == 'R')
                        {
                            location.y += blocks;
                            faceDirection = CardinalDirection.North;
                        }
                        else
                        {
                            location.y -= blocks;
                            faceDirection = CardinalDirection.South;
                        }
                        break;
                }

                visitedLocations.Add(location);
            }

            return visitedLocations;
        }
    }
}
