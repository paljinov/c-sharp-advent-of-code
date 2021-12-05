using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day5
{
    public class Lines
    {
        public int CountPointsWhereAtLeastTwoLinesOverlap(LineSegment[] lineSegments)
        {
            Dictionary<(int X, int Y), int> pointsTimesCovered = CountPointsTimesCovered(lineSegments);

            int pointsWhereAtLeastTwoLinesOverlap = pointsTimesCovered.Count(p => p.Value > 1);

            return pointsWhereAtLeastTwoLinesOverlap;
        }

        private Dictionary<(int X, int Y), int> CountPointsTimesCovered(LineSegment[] lineSegments)
        {
            Dictionary<(int X, int Y), int> pointsTimesCovered = new Dictionary<(int X, int Y), int>();

            foreach (LineSegment lineSegment in lineSegments)
            {
                // Only consider horizontal and vertical lines
                if (lineSegment.Start.X == lineSegment.End.X || lineSegment.Start.Y == lineSegment.End.Y)
                {
                    int startX = Math.Min(lineSegment.Start.X, lineSegment.End.X);
                    int endX = Math.Max(lineSegment.Start.X, lineSegment.End.X);
                    int startY = Math.Min(lineSegment.Start.Y, lineSegment.End.Y);
                    int endY = Math.Max(lineSegment.Start.Y, lineSegment.End.Y);

                    for (int x = startX; x <= endX; x++)
                    {
                        for (int y = startY; y <= endY; y++)
                        {
                            if (pointsTimesCovered.ContainsKey((x, y)))
                            {
                                pointsTimesCovered[(x, y)]++;
                            }
                            else
                            {
                                pointsTimesCovered[(x, y)] = 1;
                            }
                        }
                    }
                }
            }

            return pointsTimesCovered;
        }
    }
}
