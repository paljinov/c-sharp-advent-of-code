using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2022.Day5
{
    public class CratesStacksAndRearrangementProcedureRepository
    {
        public Dictionary<int, Stack<char>> GetCratesStacks(string input)
        {
            string[] inputParts = ParseInput(input);

            Dictionary<int, Stack<char>> cratesStacks = new Dictionary<int, Stack<char>>();

            string[] cratesStacksString =
                inputParts[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // Iterate columns
            for (int j = 1; j < cratesStacksString[0].Length; j += 4)
            {
                int stack = (int)char.GetNumericValue(cratesStacksString[^1][j]);
                Stack<char> crates = new Stack<char>();

                // Iterate rows from bottom crate up
                for (int i = cratesStacksString.Length - 2; i >= 0; i--)
                {
                    char crate = cratesStacksString[i][j];
                    if (crate != ' ')
                    {
                        crates.Push(crate);
                    }
                }

                cratesStacks[stack] = crates;
            }

            return cratesStacks;
        }

        public Step[] GetRearrangementProcedure(string input)
        {
            string[] inputParts = ParseInput(input);

            string[] stepsString = inputParts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Step[] steps = new Step[stepsString.Length];

            Regex stepsRegex = new Regex(@"^move\s(\d+)\sfrom\s(\d+)\sto\s(\d+)$$");

            for (int i = 0; i < stepsString.Length; i++)
            {
                Match stepsMatch = stepsRegex.Match(stepsString[i]);
                GroupCollection stepsGroups = stepsMatch.Groups;

                Step step = new Step
                {
                    Quantity = int.Parse(stepsGroups[1].Value),
                    FromStack = int.Parse(stepsGroups[2].Value),
                    ToStack = int.Parse(stepsGroups[3].Value),
                };

                steps[i] = step;
            }

            return steps;
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
