using System.Collections.Generic;

namespace App.Tasks.Year2017.Day11
{
    public class PathRepository
    {
        private const string NORTHEAST = "ne";
        private const string NORTHWEST = "nw";
        private const string SOUTH = "s";
        private const string SOUTHEAST = "se";
        private const string SOUTHWEST = "sw";

        public List<Direction> GetPathDirections(string input)
        {
            List<Direction> pathsDirections = new List<Direction>();

            string[] pathsDirectionsString = input.Split(',');
            foreach (string directionString in pathsDirectionsString)
            {
                Direction direction;
                switch (directionString)
                {
                    case NORTHEAST:
                        direction = Direction.Northeast;
                        break;
                    case NORTHWEST:
                        direction = Direction.Northwest;
                        break;
                    case SOUTH:
                        direction = Direction.South;
                        break;
                    case SOUTHEAST:
                        direction = Direction.Southeast;
                        break;
                    case SOUTHWEST:
                        direction = Direction.Southwest;
                        break;
                    default:
                        direction = Direction.North;
                        break;
                }

                pathsDirections.Add(direction);
            }

            return pathsDirections;
        }
    }
}
