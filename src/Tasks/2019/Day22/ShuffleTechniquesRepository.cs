using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2019.Day22
{
    public class ShuffleTechniquesRepository
    {
        public IShuffleTechnique[] GetShuffleTechniques(string input)
        {
            string[] shuffleTechniquesStrings = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            IShuffleTechnique[] shuffleTechniques = new IShuffleTechnique[shuffleTechniquesStrings.Length];

            Regex cutCardsRegex = new Regex(@"^cut\s(\d+)$");
            Regex dealWithIncrementRegex = new Regex(@"^deal\swith\sincrement\s(\d+)$");

            for (int i = 0; i < shuffleTechniquesStrings.Length; i++)
            {
                Match cutCardsMatch = cutCardsRegex.Match(shuffleTechniquesStrings[i]);
                Match dealWithIncrementMatch = dealWithIncrementRegex.Match(shuffleTechniquesStrings[i]);

                IShuffleTechnique shuffleTechnique;

                if (cutCardsMatch.Success)
                {
                    shuffleTechnique = new CutCards
                    {
                        Cut = int.Parse(cutCardsMatch.Groups[1].Value)
                    };
                }
                else if (dealWithIncrementMatch.Success)
                {
                    shuffleTechnique = new DealWithIncrement
                    {
                        Increment = int.Parse(dealWithIncrementMatch.Groups[1].Value)
                    };
                }
                else
                {
                    shuffleTechnique = new DealIntoNewStack { };
                }

                shuffleTechniques[i] = shuffleTechnique;
            }

            return shuffleTechniques;
        }
    }
}
