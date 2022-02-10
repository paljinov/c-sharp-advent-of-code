using System;

namespace App.Tasks.Year2018.Day25
{
    public class FixedPointsRepository
    {
        public FixedPoint[] GetFixedPointsInSpacetime(string input)
        {
            string[] fixedPointsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            FixedPoint[] fixedPoints = new FixedPoint[fixedPointsString.Length];

            for (int i = 0; i < fixedPointsString.Length; i++)
            {
                string[] fixedPointDimensions = fixedPointsString[i].Split(',');
                fixedPoints[i] = new FixedPoint
                {
                    X = int.Parse(fixedPointDimensions[0]),
                    Y = int.Parse(fixedPointDimensions[1]),
                    Z = int.Parse(fixedPointDimensions[2]),
                    W = int.Parse(fixedPointDimensions[3])
                };
            }

            return fixedPoints;
        }
    }
}
