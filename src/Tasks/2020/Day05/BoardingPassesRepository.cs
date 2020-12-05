using System;

namespace App.Tasks.Year2020.Day5
{
    public class BoardingPassesRepository
    {
        public string[] GetBoardingPasses(string input)
        {
            string[] boardingPasses = input.Split(Environment.NewLine);
            return boardingPasses;
        }
    }
}
