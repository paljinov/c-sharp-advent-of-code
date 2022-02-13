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

            HashSet<FixedPoint> remainingFixedPoints = fixedPoints.ToHashSet();
            while (remainingFixedPoints.Count > 0)
            {
                List<FixedPoint> constellation = FormConstellation(remainingFixedPoints);
                constellations.Add(constellation);
            }

            return constellations.Count;
        }

        private List<FixedPoint> FormConstellation(HashSet<FixedPoint> remainingFixedPoints)
        {
            int previousConstellationSize = 0;
            List<FixedPoint> constellation = new List<FixedPoint>() { remainingFixedPoints.First() };
            remainingFixedPoints.Remove(remainingFixedPoints.First());

            // Until constellation keeps increasing
            while (constellation.Count > previousConstellationSize)
            {
                previousConstellationSize = constellation.Count;

                foreach (FixedPoint fixedPoint in remainingFixedPoints)
                {
                    foreach (FixedPoint constellationFixedPoint in constellation)
                    {
                        int manhattanDistance = CalculateManhattanDistanceBetweenFixedPointsInSpacetime(
                            fixedPoint, constellationFixedPoint);

                        // If fixed point belongs to this constellation
                        if (manhattanDistance <= CONSTELLATION_MAX_DISTANCE)
                        {
                            constellation.Add(fixedPoint);
                            remainingFixedPoints.Remove(fixedPoint);
                            break;
                        }
                    };
                }
            }

            return constellation;
        }

        private int CalculateManhattanDistanceBetweenFixedPointsInSpacetime(FixedPoint a, FixedPoint b)
        {
            int manhattanDistance =
                Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z) + Math.Abs(a.W - b.W);

            return manhattanDistance;
        }
    }
}
