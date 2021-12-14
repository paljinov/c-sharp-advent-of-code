using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day14
{
    public class Polymer
    {
        public long CalculateDifferenceBetweenMostAndLeastCommonElement(
            string polymerTemplate,
            Dictionary<string, char> pairInsertionRules,
            int totalSteps
        )
        {
            Dictionary<string, long> pairsOccurences = InitializePairsOccurences(polymerTemplate);
            for (int step = 1; step <= totalSteps; step++)
            {
                pairsOccurences = DoStep(pairsOccurences, pairInsertionRules);
            }

            Dictionary<char, long> elementOccurences = CountElementsOccurencesInPolymer(pairsOccurences);
            long mostCommonElementOccurences = elementOccurences.Values.Max();
            long leastCommonElementOccurences = elementOccurences.Values.Min();

            long differenceBetweenMostAndLeastCommonElement = mostCommonElementOccurences - leastCommonElementOccurences;

            return differenceBetweenMostAndLeastCommonElement;
        }

        private Dictionary<string, long> InitializePairsOccurences(string polymer)
        {
            Dictionary<string, long> pairsOccurences = new Dictionary<string, long>();

            for (int i = 0; i < polymer.Length - 1; i++)
            {
                string pair = new string(new char[] { polymer[i], polymer[i + 1] });
                if (pairsOccurences.ContainsKey(pair))
                {
                    pairsOccurences[pair]++;
                }
                else
                {
                    pairsOccurences[pair] = 1;
                }
            }

            return pairsOccurences;
        }

        private Dictionary<string, long> DoStep(
            Dictionary<string, long> pairsOccurences,
            Dictionary<string, char> pairInsertionRules
        )
        {
            Dictionary<string, long> newPairs = new Dictionary<string, long>();

            foreach (KeyValuePair<string, long> pair in pairsOccurences)
            {
                string elementsPair = pair.Key;
                long pairOccurences = pair.Value;

                if (pairInsertionRules.ContainsKey(elementsPair))
                {
                    for (int i = 0; i < elementsPair.Length; i++)
                    {
                        string newPair;
                        if (i == 0)
                        {
                            newPair = new string(new char[] { elementsPair[i], pairInsertionRules[elementsPair] });
                        }
                        else
                        {
                            newPair = new string(new char[] { pairInsertionRules[elementsPair], elementsPair[i] });
                        }

                        if (newPairs.ContainsKey(newPair))
                        {
                            newPairs[newPair] += pairOccurences;
                        }
                        else
                        {
                            newPairs[newPair] = pairOccurences;
                        }
                    }
                }
            }

            return newPairs;
        }

        private Dictionary<char, long> CountElementsOccurencesInPolymer(Dictionary<string, long> pairsOccurences)
        {
            Dictionary<char, long> elementOccurences = new Dictionary<char, long>();

            foreach (KeyValuePair<string, long> pair in pairsOccurences)
            {
                for (int i = 0; i < pair.Key.Length; i++)
                {
                    char element = pair.Key[i];

                    if (elementOccurences.ContainsKey(element))
                    {
                        elementOccurences[element] += pair.Value;
                    }
                    else
                    {
                        elementOccurences[element] = pair.Value;
                    }
                }
            }

            // Because elements are in pairs, one element exists in two pairs
            foreach (KeyValuePair<char, long> elementOccurence in elementOccurences)
            {
                elementOccurences[elementOccurence.Key] = (long)Math.Ceiling((double)elementOccurence.Value / 2);
            }

            return elementOccurences;
        }
    }
}
