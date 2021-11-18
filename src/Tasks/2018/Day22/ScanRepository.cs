using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day22
{
    public class ScanRepository
    {
        public int GetDepth(string input)
        {
            int depth = 0;

            string[] scanString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex depthRegex = new Regex(@"^depth:\s(\d+)$");

            foreach (string row in scanString)
            {
                Match depthMatch = depthRegex.Match(row);
                if (depthMatch.Success)
                {
                    depth = int.Parse(depthMatch.Groups[1].Value);
                    break;
                }
            }

            return depth;
        }

        public (int X, int Y) GetTargetCoordinates(string input)
        {
            (int X, int Y) targetCoordinates = (0, 0);

            string[] scanString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex targetCoordinatesRegex = new Regex(@"^target:\s(\d+),(\d+)$");

            foreach (string row in scanString)
            {
                Match targetCoordinatesMatch = targetCoordinatesRegex.Match(row);
                if (targetCoordinatesMatch.Success)
                {
                    targetCoordinates.X = int.Parse(targetCoordinatesMatch.Groups[1].Value);
                    targetCoordinates.Y = int.Parse(targetCoordinatesMatch.Groups[2].Value);
                    break;
                }
            }

            return targetCoordinates;
        }
    }
}
