using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day24
{
    public class TilesRepository
    {
        private const string EAST = "e";
        private const string SOUTHEAST = "se";
        private const string NORTHEAST = "ne";
        private const string WEST = "w";
        private const string SOUTHWEST = "sw";
        private const string NORTHWEST = "nw";

        public List<List<Direction>> GetTilesDirections(string input)
        {
            List<List<Direction>> tilesDirections = new List<List<Direction>>();

            string[] tilesDirectionsString = input.Split(Environment.NewLine);
            Regex tileDirectionsRegex = new Regex($@"{EAST}|{SOUTHEAST}|{NORTHEAST}|{WEST}|{SOUTHWEST}|{NORTHWEST}");

            foreach (string directionString in tilesDirectionsString)
            {
                List<Direction> tileDirections = new List<Direction>();

                MatchCollection tileDirectionsMatches = tileDirectionsRegex.Matches(directionString);
                foreach (Match tileDirectionMatch in tileDirectionsMatches)
                {
                    Direction direction = Direction.East;
                    switch (tileDirectionMatch.Groups[0].Value)
                    {
                        case SOUTHEAST:
                            direction = Direction.Southeast;
                            break;
                        case NORTHEAST:
                            direction = Direction.Northeast;
                            break;
                        case WEST:
                            direction = Direction.West;
                            break;
                        case SOUTHWEST:
                            direction = Direction.Southwest;
                            break;
                        case NORTHWEST:
                            direction = Direction.Northwest;
                            break;
                    }

                    tileDirections.Add(direction);
                }

                tilesDirections.Add(tileDirections);
            }

            return tilesDirections;
        }
    }
}
