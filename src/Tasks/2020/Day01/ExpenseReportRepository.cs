using System;
using System.Collections.Generic;

namespace App.Tasks.Year2020.Day1
{
    public class ExpenseReportRepository
    {
        public List<int> GetEntries(string input)
        {
            List<int> entries = new List<int>();

            string[] entriesString = input.Split(Environment.NewLine);
            foreach (string entryString in entriesString)
            {
                entries.Add(int.Parse(entryString));
            }

            return entries;
        }
    }
}
