using System;
using System.Collections.Generic;

namespace App.Tasks.Year2018.Day6
{
    public class Area
    {
        public int CalculateSizeOfTheLargestNonInfiniteArea(List<(int, int)> coordinates)
        {
            int sizeOfTheLargestNonInfiniteArea = 0;

            List<(int, int)> surroundedPoints = GetSurroundedPoints(coordinates);
            (int leftX, int rightX, int topY, int bottomY) = GetEdgeCoordinates(coordinates);

            foreach ((int, int) surroundedPoint in surroundedPoints)
            {
                int areaSize =
                    CalculateAreaForSurroundedPoint(surroundedPoint, coordinates, leftX, rightX, topY, bottomY);
                sizeOfTheLargestNonInfiniteArea = Math.Max(areaSize, sizeOfTheLargestNonInfiniteArea);
            }

            return sizeOfTheLargestNonInfiniteArea;
        }

        private (int leftX, int rightX, int topY, int bottomY) GetEdgeCoordinates(List<(int, int)> coordinates)
        {
            int leftX = int.MaxValue;
            int rightX = 0;
            int topY = int.MaxValue;
            int bottomY = 0;

            foreach ((int x, int y) coordinate in coordinates)
            {
                leftX = Math.Min(leftX, coordinate.x);
                rightX = Math.Max(rightX, coordinate.x);
                topY = Math.Min(topY, coordinate.y);
                bottomY = Math.Max(bottomY, coordinate.y);
            }

            return (leftX, rightX, topY, bottomY);
        }

        private List<(int, int)> GetSurroundedPoints(List<(int x, int y)> coordinates)
        {
            List<(int, int)> surroundedPoints = new List<(int, int)>();

            for (int i = 0; i < coordinates.Count; i++)
            {
                bool topLeft = false;
                bool bottomLeft = false;
                bool topRight = false;
                bool bottomRight = false;

                for (int j = 0; j < coordinates.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (coordinates[j].x < coordinates[i].x && coordinates[j].y < coordinates[i].y)
                    {
                        topLeft = true;
                    }

                    if (coordinates[j].x < coordinates[i].x && coordinates[j].y > coordinates[i].y)
                    {
                        bottomLeft = true;
                    }

                    if (coordinates[j].x > coordinates[i].x && coordinates[j].y < coordinates[i].y)
                    {
                        topRight = true;
                    }

                    if (coordinates[j].x > coordinates[i].x && coordinates[j].y > coordinates[i].y)
                    {
                        bottomRight = true;
                    }
                }

                if (topLeft && bottomLeft && topRight && bottomRight)
                {
                    surroundedPoints.Add(coordinates[i]);
                }
            }

            return surroundedPoints;
        }


        private int CalculateAreaForSurroundedPoint(
            (int x, int y) surroundedPoint,
            List<(int, int)> coordinates,
            int leftX,
            int rightX,
            int topY,
            int bottomY
        )
        {
            int areaSize = 0;

            for (int i = leftX; i <= rightX; i++)
            {
                for (int j = topY; j <= bottomY; j++)
                {
                    bool isAnySurroundingPointCloser = false;
                    foreach ((int x, int y) coordinate in coordinates)
                    {
                        if (surroundedPoint != coordinate
                            && !IsFirstPointCloserToReferencePoint((i, j), surroundedPoint, coordinate))
                        {
                            isAnySurroundingPointCloser = true;
                            break;
                        }
                    }

                    if (!isAnySurroundingPointCloser)
                    {
                        areaSize++;
                    }
                }
            }

            return areaSize;
        }

        private bool IsFirstPointCloserToReferencePoint(
            (int, int) referencePoint,
            (int, int) firstPoint,
            (int, int) secondPoint
        )
        {
            int distanceFromFirstPoint = CalculateManhattanDistance(referencePoint, firstPoint);
            int distanceFromSecondPoint = CalculateManhattanDistance(referencePoint, secondPoint);

            if (distanceFromFirstPoint < distanceFromSecondPoint)
            {
                return true;
            }

            return false;
        }

        private int CalculateManhattanDistance((int x, int y) point1, (int x, int y) point2)
        {
            int manhattanDistance = Math.Abs(point1.x - point2.x) + Math.Abs(point1.y - point2.y);
            return manhattanDistance;
        }
    }
}
