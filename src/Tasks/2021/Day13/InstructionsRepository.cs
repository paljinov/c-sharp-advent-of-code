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

        public (char, int)[] GetFoldInstructions(string input)
        {
            string[] inputParts = ParseInput(input);

            string[] foldInstructionsString =
                inputParts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            (char, int)[] foldInstructions = new (char, int)[foldInstructionsString.Length];

            Regex foldInstructionRegex = new Regex(@"^fold\salong\s(x|y)=(\d+)$");

            for (int i = 0; i < foldInstructionsString.Length; i++)
            {
                Match foldInstructionMatch = foldInstructionRegex.Match(foldInstructionsString[i]);
                GroupCollection foldInstructionGroups = foldInstructionMatch.Groups;

                foldInstructions[i] = (
                    foldInstructionGroups[1].Value[0],
                    int.Parse(foldInstructionGroups[2].Value)
                );
            }

            return foldInstructions;
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
