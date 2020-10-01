using System;
using System.Collections.Generic;

namespace App.Tasks.Year2015.Day5
{
    class StringsRepository
    {
        public static string[] GetStrings(string input)
        {
            string[] strings = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return strings;
        }
    }
}
