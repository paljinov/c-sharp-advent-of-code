using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day10
{
    public class PointsRepository
    {
        public List<Point> GetPoints(string input)
        {
            List<Point> points = new List<Point>();

            string[] pointsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex pointRegex = new Regex(@"^position=<\s*(-?\d+),\s*(-?\d+)>\svelocity=<\s*(-?\d+),\s*(-?\d+)>$");

            for (int i = 0; i < pointsString.Length; i++)
            {
                Match pointMatch = pointRegex.Match(pointsString[i]);
                GroupCollection pointGroups = pointMatch.Groups;

                Point point = new Point
                {
                    Position = (int.Parse(pointGroups[1].Value), int.Parse(pointGroups[2].Value)),
                    Velocity = (int.Parse(pointGroups[3].Value), int.Parse(pointGroups[4].Value))
                };

                points.Add(point);
            }

            return points;
        }
    }
}
