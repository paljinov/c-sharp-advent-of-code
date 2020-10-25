using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day1
{
    class CityStreetGrid
    {
        public List<(int, int)> CalculateVisitedBlocksAfterMove(string[] instructions)
        {
            List<(int, int)> visitedBlocks = new List<(int, int)>();
            (int x, int y) currentBlock = (0, 0);
            visitedBlocks.Add(currentBlock);

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
                            currentBlock.x += blocks;
                            faceDirection = CardinalDirection.East;
                        }
                        else
                        {
                            currentBlock.x -= blocks;
                            faceDirection = CardinalDirection.West;
                        }
                        break;
                    case CardinalDirection.East:
                        if (turn == 'R')
                        {
                            currentBlock.y -= blocks;
                            faceDirection = CardinalDirection.South;
                        }
                        else
                        {
                            currentBlock.y += blocks;
                            faceDirection = CardinalDirection.North;
                        }
                        break;
                    case CardinalDirection.South:
                        if (turn == 'R')
                        {
                            currentBlock.x -= blocks;
                            faceDirection = CardinalDirection.West;
                        }
                        else
                        {
                            currentBlock.x += blocks;
                            faceDirection = CardinalDirection.East;
                        }
                        break;
                    case CardinalDirection.West:
                        if (turn == 'R')
                        {
                            currentBlock.y += blocks;
                            faceDirection = CardinalDirection.North;
                        }
                        else
                        {
                            currentBlock.y -= blocks;
                            faceDirection = CardinalDirection.South;
                        }
                        break;
                }

                visitedBlocks.Add(currentBlock);
            }

            return visitedBlocks;
        }

        public List<(int, int)> CalculateAllVisitedLocations(List<(int, int)> visitedBlocks)
        {
            List<(int, int)> allVisitedLocations = new List<(int, int)>()
            {
                visitedBlocks[0]
            };

            for (int i = 0; i < visitedBlocks.Count - 1; i++)
            {
                (int x, int y) currentBlock = (visitedBlocks[i].Item1, visitedBlocks[i].Item2);
                (int x, int y) nextBlock = (visitedBlocks[i + 1].Item1, visitedBlocks[i + 1].Item2);

                // If moving by x axis
                if (currentBlock.x != nextBlock.x)
                {
                    while (currentBlock.x != nextBlock.x)
                    {
                        if (nextBlock.x > currentBlock.x)
                        {
                            currentBlock.x++;
                        }
                        else
                        {
                            currentBlock.x--;
                        }

                        (int x, int y) visitedLocation = (currentBlock.x, nextBlock.y);
                        allVisitedLocations.Add(visitedLocation);
                    }
                }
                // If moving by y axis
                else
                {
                    while (currentBlock.y != nextBlock.y)
                    {
                        if (nextBlock.y > currentBlock.y)
                        {
                            currentBlock.y++;
                        }
                        else
                        {
                            currentBlock.y--;
                        }

                        (int x, int y) visitedLocation = (nextBlock.x, currentBlock.y);
                        allVisitedLocations.Add(visitedLocation);
                    }
                }
            }

            return allVisitedLocations;
        }
    }
}
