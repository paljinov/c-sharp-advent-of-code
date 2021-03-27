using System;

namespace App.Tasks.Year2017.Day16
{
    public class DanceMovesRepository
    {
        public string[] GetDanceMoves(string input)
        {
            string[] danceMoves = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return danceMoves;
        }
    }
}
