using System;

namespace App.Tasks.Year2015.Day5
{
    public class StringsRepository
    {
        public string[] GetStrings(string input)
        {
            string[] strings = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return strings;
        }
    }
}
