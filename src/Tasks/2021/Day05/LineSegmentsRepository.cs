using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2021.Day5
{
    public class LineSegmentsRepository
    {
        public LineSegment[] GetLineSegments(string input)
        {
            string[] lineSegmentsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            LineSegment[] lineSegments = new LineSegment[lineSegmentsString.Length];

            Regex lineSegmentRegex = new Regex(@"^(\d+),(\d+)\s->\s(\d+),(\d+)$");

            for (int i = 0; i < lineSegmentsString.Length; i++)
            {
                Match lineSegmentMatch = lineSegmentRegex.Match(lineSegmentsString[i]);
                GroupCollection lineSegmentGroups = lineSegmentMatch.Groups;

                LineSegment lineSegment = new LineSegment
                {
                    Start = (int.Parse(lineSegmentGroups[1].Value), int.Parse(lineSegmentGroups[2].Value)),
                    End = (int.Parse(lineSegmentGroups[3].Value), int.Parse(lineSegmentGroups[4].Value)),
                };

                lineSegments[i] = lineSegment;
            }

            return lineSegments;
        }
    }
}
