using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day25
{
    public class Constellations
    {
        private const int CONSTELLATION_MAX_DISTANCE = 3;

        public int CountFormedConstellationsByTheFixedPointsInSpacetime(FixedPoint[] fixedPoints)
        {
            List<List<FixedPoint>> constellations = new List<List<FixedPoint>>();

            while (fixedPoints.Length > 0)
            {
                (fixedPoints, List<FixedPoint> constellation) = FormConstellation(fixedPoints.ToHashSet());
                constellations.Add(constellation);
            }

            return constellations.Count;
        }

        private (FixedPoint[] RemainingFixedPoints, List<FixedPoint> Constellation) FormConstellation(
            HashSet<FixedPoint> fixedPoints
        )
        {
            int previousConstellationSize = 0;
            List<FixedPoint> constellation = new List<FixedPoint>() { fixedPoints.First() };

            // Until constellation keeps increasing
            while (constellation.Count > previousConstellationSize)
            {
                previousConstellationSize = constellation.Count;

                foreach (FixedPoint fixedPoint in fixedPoints)
                {
                    foreach (FixedPoint constellationFixedPoint in constellation.ToList())
                    {
                        int manhattanDistance = CalculateManhattanDistanceBetweenFixedPointsInSpacetime(
                            fixedPoint, constellationFixedPoint);

                        if (manhattanDistance <= CONSTELLATION_MAX_DISTANCE)
                        {
                            constellation.Add(fixedPoint);
                            fixedPoints.Remove(fixedPoint);
                        }
                    };
                }
            }

            return (fixedPoints.ToArray(), constellation);
        }

        private int CalculateManhattanDistanceBetweenFixedPointsInSpacetime(FixedPoint a, FixedPoint b)
        {
            int manhattanDistance =
                Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z) + Math.Abs(a.W - b.W);

            return manhattanDistance;
        }
    }
}
