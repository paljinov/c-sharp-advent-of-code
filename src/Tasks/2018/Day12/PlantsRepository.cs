using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day12
{
    public class PlantsRepository
    {
        public string GetPots(string input)
        {
            string[] inputArray = ParseInput(input);
            Regex initialStateRegex = new Regex(@"^initial\sstate:\s([#\.]+)$");

            Match initialStateMatch = initialStateRegex.Match(inputArray[0]);
            GroupCollection initialStateGroups = initialStateMatch.Groups;

            string initialState = initialStateGroups[1].Value;

            return initialState;
        }

        public Dictionary<string, char> GetSpreadNotes(string input)
        {
            string[] inputArray = ParseInput(input);

            Dictionary<string, char> spreadNotes = new Dictionary<string, char>();

            for (int i = 1; i < inputArray.Length; i++)
            {
                string[] spreadNotesRow = inputArray[i].Split("=>");
                spreadNotes.Add(spreadNotesRow[0].Trim(), spreadNotesRow[1].Trim()[0]);
            }

            return spreadNotes;
        }

        private string[] ParseInput(string input)
        {
            string[] inputArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return inputArray;
        }
    }
}
