using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day21
{
    public class RulesRepository
    {
        private readonly char[,] gridOfPixelsInitialState = new char[,] {
            { '.', '#', '.' },
            { '.', '.', '#' },
            { '#', '#', '#' }
        };

        public char[,] GetGridOfPixelsInitialState()
        {
            return gridOfPixelsInitialState;
        }

        public Dictionary<char[,], char[,]> GetRules(string input)
        {
            Dictionary<char[,], char[,]> rules = new Dictionary<char[,], char[,]>();

            string[] rulesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string enhancementRule in rulesString)
            {
                string[] patterns = enhancementRule.Split("=>");
                rules.Add(ConvertStringPatternToArray(patterns[0]), ConvertStringPatternToArray(patterns[1]));
            }

            return rules;
        }

        private char[,] ConvertStringPatternToArray(string patternString)
        {
            string[] rows = patternString.Trim().Split("/");
            int size = rows.Length;

            char[,] pattern = new char[size, size];

            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    pattern[i, j] = rows[i][j];
                }
            }

            return pattern;
        }
    }
}
