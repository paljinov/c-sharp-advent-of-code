using System;

namespace App.Tasks.Year2015.Day9
{
    public class PossibleRoutesRepository
    {
        public string[] GetPossibleRoutes(string input)
        {
            string[] possibleRoutes = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return possibleRoutes;
        }
    }
}
