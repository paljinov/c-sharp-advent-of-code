using System;

namespace App.Tasks.Year2020.Day18
{
    public class ExpressionsRepository
    {
        public string[] GetExpressions(string input)
        {
            string[] expressions = input.Split(Environment.NewLine);

            return expressions;
        }
    }
}
