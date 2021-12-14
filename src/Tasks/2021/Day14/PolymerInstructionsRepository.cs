using System;
using System.Collections.Generic;

namespace App.Tasks.Year2021.Day14
{
    public class PolymerInstructionsRepository
    {
        public string GetPolymerTemplate(string input)
        {
            string polymerTemplate = ParseInput(input)[0];

            return polymerTemplate;
        }

        public Dictionary<string, char> GetPairInsertionRules(string input)
        {
            string[] inputParts = ParseInput(input);

            string[] pairInsertionRulesString =
                inputParts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, char> pairInsertionRules = new Dictionary<string, char>();

            for (int i = 0; i < pairInsertionRulesString.Length; i++)
            {
                string[] pairInsertionRuleString = pairInsertionRulesString[i].Split(" -> ");
                pairInsertionRules.Add(pairInsertionRuleString[0], pairInsertionRuleString[1][0]);
            }

            return pairInsertionRules;
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
