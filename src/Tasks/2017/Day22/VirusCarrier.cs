using System.Collections.Generic;

namespace App.Tasks.Year2017.Day22
{
    public class VirusCarrier
    {
        private const char INFECTED_NODE = '#';

        private const char CLEAN_NODE = '.';

        private const char WEAKENED_NODE = 'W';

        private const char FLAGGED_NODE = 'F';

        public int CountBurstsThatCausedAnInfection(Dictionary<(int, int), char> nodesMap, int totalBurstsOfActivity)
        {
            int burstsThatCausedAnInfection = 0;

            Direction facingDirection = Direction.UP;
            int x = 0;
            int y = 0;

            for (int i = 0; i < totalBurstsOfActivity; i++)
            {
                // If node is not infected
                if (!nodesMap.ContainsKey((x, y)) || nodesMap[(x, y)] == CLEAN_NODE)
                {
                    nodesMap[(x, y)] = INFECTED_NODE;

                    switch (facingDirection)
                    {
                        case Direction.UP:
                            facingDirection = Direction.LEFT;
                            x--;
                            break;
                        case Direction.DOWN:
                            facingDirection = Direction.RIGHT;
                            x++;
                            break;
                        case Direction.LEFT:
                            facingDirection = Direction.DOWN;
                            y--;
                            break;
                        case Direction.RIGHT:
                            facingDirection = Direction.UP;
                            y++;
                            break;
                    }

                    burstsThatCausedAnInfection++;
                }
                // If node is infected
                else
                {
                    nodesMap[(x, y)] = CLEAN_NODE;

                    switch (facingDirection)
                    {
                        case Direction.UP:
                            facingDirection = Direction.RIGHT;
                            x++;
                            break;
                        case Direction.DOWN:
                            facingDirection = Direction.LEFT;
                            x--;
                            break;
                        case Direction.LEFT:
                            facingDirection = Direction.UP;
                            y++;
                            break;
                        case Direction.RIGHT:
                            facingDirection = Direction.DOWN;
                            y--;
                            break;
                    }
                }
            }

            return burstsThatCausedAnInfection;
        }

        public int CountBurstsThatCausedAnInfectionForEvolvedVirus(
            Dictionary<(int, int), char> nodesMap,
            int totalBurstsOfActivity
        )
        {
            int burstsThatCausedAnInfection = 0;

            Direction facingDirection = Direction.UP;
            int x = 0;
            int y = 0;

            for (int i = 0; i < totalBurstsOfActivity; i++)
            {
                // If node is not infected
                if (!nodesMap.ContainsKey((x, y)) || nodesMap[(x, y)] == CLEAN_NODE)
                {
                    nodesMap[(x, y)] = WEAKENED_NODE;

                    switch (facingDirection)
                    {
                        case Direction.UP:
                            facingDirection = Direction.LEFT;
                            x--;
                            break;
                        case Direction.DOWN:
                            facingDirection = Direction.RIGHT;
                            x++;
                            break;
                        case Direction.LEFT:
                            facingDirection = Direction.DOWN;
                            y--;
                            break;
                        case Direction.RIGHT:
                            facingDirection = Direction.UP;
                            y++;
                            break;
                    }
                }
                // If node is weakened
                else if (nodesMap[(x, y)] == WEAKENED_NODE)
                {
                    nodesMap[(x, y)] = INFECTED_NODE;

                    switch (facingDirection)
                    {
                        case Direction.UP:
                            y++;
                            break;
                        case Direction.DOWN:
                            y--;
                            break;
                        case Direction.LEFT:
                            x--;
                            break;
                        case Direction.RIGHT:
                            x++;
                            break;
                    }

                    burstsThatCausedAnInfection++;
                }
                // If node is infected
                else if (nodesMap[(x, y)] == INFECTED_NODE)
                {
                    nodesMap[(x, y)] = FLAGGED_NODE;

                    switch (facingDirection)
                    {
                        case Direction.UP:
                            facingDirection = Direction.RIGHT;
                            x++;
                            break;
                        case Direction.DOWN:
                            facingDirection = Direction.LEFT;
                            x--;
                            break;
                        case Direction.LEFT:
                            facingDirection = Direction.UP;
                            y++;
                            break;
                        case Direction.RIGHT:
                            facingDirection = Direction.DOWN;
                            y--;
                            break;
                    }
                }
                // If node is flagged
                else
                {
                    nodesMap[(x, y)] = CLEAN_NODE;

                    switch (facingDirection)
                    {
                        case Direction.UP:
                            facingDirection = Direction.DOWN;
                            y--;
                            break;
                        case Direction.DOWN:
                            facingDirection = Direction.UP;
                            y++;
                            break;
                        case Direction.LEFT:
                            facingDirection = Direction.RIGHT;
                            x++;
                            break;
                        case Direction.RIGHT:
                            facingDirection = Direction.LEFT;
                            x--;
                            break;
                    }
                }
            }

            return burstsThatCausedAnInfection;
        }
    }
}
