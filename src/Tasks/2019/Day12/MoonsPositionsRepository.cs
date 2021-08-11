using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2019.Day12
{
    public class MoonsPositionsRepository
    {
        public List<Position> GetMoonsPositions(string input)
        {
            List<Position> positions = new List<Position>();

            string[] positionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex positionRegex = new Regex(@"^<x=(-?\d+),\sy=(-?\d+),\sz=(-?\d+)>$");

            for (int i = 0; i < positionsString.Length; i++)
            {
                Match positionMatch = positionRegex.Match(positionsString[i]);
                GroupCollection positionGroups = positionMatch.Groups;

                Position position = new Position
                {
                    X = int.Parse(positionGroups[1].Value),
                    Y = int.Parse(positionGroups[2].Value),
                    Z = int.Parse(positionGroups[3].Value)
                };

                positions.Add(position);
            }

            return positions;
        }
    }
}
