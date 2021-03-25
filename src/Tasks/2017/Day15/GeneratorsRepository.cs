using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day15
{
    public class GeneratorsRepository
    {
        private const char GENERATOR_A = 'A';

        private const char GENERATOR_B = 'B';

        public (int generatorAStartValue, int generatorBStartValue) GetStartingValues(string input)
        {
            Dictionary<char, int> generatorsStartingValues = new Dictionary<char, int>();

            string[] generatorsStartingValuesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex generatorStartingValueRegex = new Regex(@"^Generator\s([A-B])\sstarts\swith\s(\d+)$");

            foreach (string generatorStartingValueString in generatorsStartingValuesString)
            {
                Match generatorStartingValueMatch = generatorStartingValueRegex.Match(generatorStartingValueString);
                GroupCollection generatorStartingValueGroups = generatorStartingValueMatch.Groups;

                char generator = generatorStartingValueGroups[1].Value[0];
                int startingValue = int.Parse(generatorStartingValueGroups[2].Value);

                generatorsStartingValues.Add(generator, startingValue);
            }

            return (generatorsStartingValues[GENERATOR_A], generatorsStartingValues[GENERATOR_B]);
        }
    }
}
