using System;

namespace App.Tasks.Year2019.Day22
{
    public class ShuffleTechniquesRepository
    {
        public string[] GetShuffleTechniques(string input)
        {
            string[] shuffleTechniques = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return shuffleTechniques;
        }
    }
}
