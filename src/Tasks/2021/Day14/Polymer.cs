using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2021.Day14
{
    public class Polymer
    {
        public int CalculateDifferenceBetweenMostAndLeastCommonElement(
            string polymerTemplate,
            Dictionary<string, char> pairInsertionRules,
            int totalSteps
        )
        {
            string polymer = polymerTemplate.ToString();

            for (int step = 1; step <= totalSteps; step++)
            {
                polymer = DoStep(polymer, pairInsertionRules);
            }

            var letterOccurences = polymer.GroupBy(l => l).OrderByDescending(l => l.Count());

            int differenceBetweenMostAndLeastCommonElement =
                letterOccurences.First().Count() - letterOccurences.Last().Count();

            return differenceBetweenMostAndLeastCommonElement;
        }

        private string DoStep(string polymer, Dictionary<string, char> pairInsertionRules)
        {
            StringBuilder newPolymer = new StringBuilder();

            for (int i = 0; i < polymer.Length - 1; i++)
            {
                newPolymer.Append(polymer[i]);

                string pair = new string(new char[] { polymer[i], polymer[i + 1] });
                if (pairInsertionRules.ContainsKey(pair))
                {
                    newPolymer.Append(pairInsertionRules[pair]);
                }
            }
            newPolymer.Append(polymer[^1]);

            return newPolymer.ToString();
        }
    }
}
