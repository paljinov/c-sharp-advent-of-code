using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day5
{
    public class Lines
    {
        public int CountPointsWhereAtLeastTwoLinesOverlap(LineSegment[] lineSegments)
        {
            Dictionary<(int X, int Y), int> pointsTimesCovered = CountPointsTimesCovered(lineSegments, false);

            int pointsWhereAtLeastTwoLinesOverlap = pointsTimesCovered.Count(p => p.Value > 1);

            return pointsWhereAtLeastTwoLinesOverlap;
        }

        public int CountPointsWhereAtLeastTwoLinesOverlapWhenConsideringDiagonalLines(LineSegment[] lineSegments)
        {
            Dictionary<(int X, int Y), int> pointsTimesCovered = CountPointsTimesCovered(lineSegments, true);

            int pointsWhereAtLeastTwoLinesOverlap = pointsTimesCovered.Count(p => p.Value > 1);

            return pointsWhereAtLeastTwoLinesOverlap;
        }

        private Dictionary<(int X, int Y), int> CountPointsTimesCovered(
            LineSegment[] lineSegments,
            bool considerDiagonalLines
        )
        {
            Dictionary<(int X, int Y), int> pointsTimesCovered = new Dictionary<(int X, int Y), int>();

            foreach (LineSegment lineSegment in lineSegments)
            {
                bool isLineDiagonal = false;
                if (lineSegment.Start.X != lineSegment.End.X && lineSegment.Start.Y != lineSegment.End.Y)
                {
                    isLineDiagonal = true;
                }

                bool isAngleFortyFiveDegrees = Math.Abs(lineSegment.End.X - lineSegment.Start.X)
                    == Math.Abs(lineSegment.End.Y - lineSegment.Start.Y);

                int x = lineSegment.Start.X;
                int y = lineSegment.Start.Y;
                int xIncrement = lineSegment.Start.X < lineSegment.End.X ? 1 : -1;
                int yIncrement = lineSegment.Start.Y < lineSegment.End.Y ? 1 : -1;

                // Count for horizontal and vertical lines
                if (!isLineDiagonal)
                {
                    while ((xIncrement == 1 && x <= lineSegment.End.X)
                        || (xIncrement == -1 && x >= lineSegment.End.X))
                    {
                        while ((yIncrement == 1 && y <= lineSegment.End.Y)
                            || (yIncrement == -1 && y >= lineSegment.End.Y))
                        {
                            if (pointsTimesCovered.ContainsKey((x, y)))
                            {
                                pointsTimesCovered[(x, y)]++;
                            }
                            else
                            {
                                pointsTimesCovered[(x, y)] = 1;
                            }

                            y += yIncrement;
                        }

                        y = lineSegment.Start.Y;
                        x += xIncrement;
                    }
                }
                // Count for diagonal lines where angle is exactly 45 degrees
                else if (considerDiagonalLines && isAngleFortyFiveDegrees)
                {
                    while ((xIncrement == 1 && x <= lineSegment.End.X) || (xIncrement == -1 && x >= lineSegment.End.X)
                        || (yIncrement == 1 && y <= lineSegment.End.Y) || (yIncrement == -1 && y >= lineSegment.End.Y))
                    {
                        if (pointsTimesCovered.ContainsKey((x, y)))
                        {
                            pointsTimesCovered[(x, y)]++;
                        }
                        else
                        {
                            pointsTimesCovered[(x, y)] = 1;
                        }

                        x += xIncrement;
                        y += yIncrement;
                    }
                }
            }

            return pointsTimesCovered;
        }
    }
}
