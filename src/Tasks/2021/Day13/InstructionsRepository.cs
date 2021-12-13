using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2021.Day13
{
    public class InstructionsRepository
    {
        public (int, int)[] GetDots(string input)
        {
            string[] inputParts = ParseInput(input);

            string[] dotsString = inputParts[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            (int, int)[] dots = new (int, int)[dotsString.Length];

            for (int i = 0; i < dotsString.Length; i++)
            {
                string[] dotString = dotsString[i].Split(',');
                int x = int.Parse(dotString[0]);
                int y = int.Parse(dotString[1]);

                dots[i] = (x, y);
            }

            return dots;
        }

        public (int, int)[] GetFoldAlongCoordinates(string input)
        {
            string[] inputParts = ParseInput(input);

            string[] foldAlongCoordinatesString =
                inputParts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int foldAlongCoordinatesLength = foldAlongCoordinatesString.Length / 2;
            (int, int)[] foldAlongCoordinates = new (int, int)[foldAlongCoordinatesLength];

            Regex foldAlongCoordinateRegex = new Regex(@"^fold\salong\s(x|y)=(\d+)$");

            int x = 0;
            int y = 0;
            for (int i = 0; i < foldAlongCoordinatesLength; i += 2)
            {
                for (int j = 0; j < 2; j++)
                {
                    Match foldAlongCoordinateMatch = foldAlongCoordinateRegex.Match(foldAlongCoordinatesString[j]);
                    GroupCollection foldAlongCoordinateGroups = foldAlongCoordinateMatch.Groups;

                    if (foldAlongCoordinateGroups[1].Value == "x")
                    {
                        x = int.Parse(foldAlongCoordinateGroups[2].Value);
                    }
                    else
                    {
                        y = int.Parse(foldAlongCoordinateGroups[2].Value);
                    }

                    if (j > 0)
                    {
                        foldAlongCoordinates[i / 2] = (x, y);
                    }
                }
            }

            return foldAlongCoordinates;
        }

        private string[] ParseInput(string input)
        {
            string[] inputParts = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );

            return inputParts;
        }
    }
}
