using System.Collections.Generic;

namespace App.Tasks.Year2017.Day22
{
    public class VirusCarrier
    {
        private readonly int totalBurstsOfActivity = 10000;

        public int CountBurstsThatCausedAnInfection(Dictionary<(int, int), bool> infectedNodesMap)
        {
            int burstsThatCausedAnInfection = 0;

            Direction facingDirection = Direction.UP;
            int x = 0;
            int y = 0;

            for (int i = 0; i < totalBurstsOfActivity; i++)
            {
                // If node is not infected
                if (!infectedNodesMap.ContainsKey((x, y)) || !infectedNodesMap[(x, y)])
                {
                    infectedNodesMap[(x, y)] = true;

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
                    infectedNodesMap[(x, y)] = false;

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
    }
}
