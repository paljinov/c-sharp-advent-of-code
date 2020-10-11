using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day13
{
    class SittingCombinations
    {
        /// <summary>
        /// Parse input string to sittings happiness.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        /// [
        ///     Alice->Bob => 54,
        ///     Alice->Carol => - 79,
        ///     ...
        /// ]
        /// </returns>
        public Dictionary<string, int> Parse(string input)
        {
            Dictionary<string, int> sittingsHappiness = new Dictionary<string, int>();

            Regex sittingHappinessRegex = new Regex(@"^(\w+).+?(\w+)\s(\d+).+?(\w+)\.$");

            string[] sittingsHappinessStrings = input.Split(Environment.NewLine);
            foreach (string neighborsString in sittingsHappinessStrings)
            {
                Match sittingHappinessMatch = sittingHappinessRegex.Match(neighborsString);
                GroupCollection groups = sittingHappinessMatch.Groups;

                string neighbors = $"{groups[1].Value}->{groups[4].Value}";
                int happinessUnits = int.Parse(groups[3].Value);
                if (groups[2].Value == "lose")
                {
                    happinessUnits *= -1;
                }

                sittingsHappiness.Add(neighbors, happinessUnits);
            }

            return sittingsHappiness;
        }

        /// <summary>
        /// Get sitting combinations with happiness.
        /// </summary>
        /// <param name="sittingsHappiness"></param>
        /// <returns>
        /// [
        ///     1st combination => [
        ///         Alice->Bob => 54,
        ///         Alice->Carol => - 79,
        ///         ...
        ///     ],
        ///     ...
        /// ]
        /// </returns>
        public List<Dictionary<string, int>> GetSittingCombinations(Dictionary<string, int> sittingsHappiness)
        {
            List<Dictionary<string, int>> sittingCombinations = new List<Dictionary<string, int>>();

            List<string> personsBase = GetPersons(sittingsHappiness).ToList();
            var personPermutations = new List<List<string>>();
            GetPersonsPermuations(personsBase, 0, personsBase.Count - 1, personPermutations);

            foreach (List<string> persons in personPermutations)
            {
                Dictionary<string, int> sittingCombination = new Dictionary<string, int>();
                for (int i = 0; i < persons.Count; i++)
                {
                    int leftNeighborIndex = i == 0 ? persons.Count - 1 : i - 1;
                    int rightNeighborIndex = i == persons.Count - 1 ? 0 : i + 1;

                    string person = persons.ElementAt(i);
                    string leftNeighbor = persons.ElementAt(leftNeighborIndex);
                    string rightNeighbor = persons.ElementAt(rightNeighborIndex);

                    string[] neighbors = new string[] {
                        $"{person}->{leftNeighbor}",
                        $"{leftNeighbor}->{person}",
                        $"{person}->{rightNeighbor}",
                        $"{rightNeighbor}->{person}"
                    };

                    foreach (var n in neighbors)
                    {
                        sittingCombination.TryAdd(n, sittingsHappiness[n]);
                    }
                }

                sittingCombinations.Add(sittingCombination);
            }

            return sittingCombinations;
        }

        /// <summary>
        /// Calculate optimal (max) total change in happiness.
        /// </summary>
        /// <param name="sittingCombinations"></param>
        /// <returns></returns>
        public int CalculateOptimalTotalChangeInHappiness(List<Dictionary<string, int>> sittingCombinations)
        {
            int optimalTotalChangeInHappiness = 0;

            foreach (Dictionary<string, int> sittingCombination in sittingCombinations)
            {
                int happinessChange = 0;
                foreach (var neighbor in sittingCombination)
                {
                    happinessChange += neighbor.Value;
                }

                optimalTotalChangeInHappiness = Math.Max(optimalTotalChangeInHappiness, happinessChange);
            }

            return optimalTotalChangeInHappiness;
        }

        /// <summary>
        /// Get persons which sit around the table.
        /// </summary>
        /// <param name="sittingsHappiness"></param>
        /// <returns></returns>
        public HashSet<string> GetPersons(Dictionary<string, int> sittingsHappiness)
        {
            HashSet<string> persons = new HashSet<string>();
            foreach (KeyValuePair<string, int> sittingHappiness in sittingsHappiness)
            {
                string[] personsNames = sittingHappiness.Key.Split("->");
                foreach (string personName in personsNames)
                {
                    persons.Add(personName);
                }
            }

            return persons;
        }

        /// <summary>
        /// Get all possible sitting orders around the table.
        /// </summary>
        /// <param name="persons"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="personsPermutations"></param>
        /// <returns></returns>
        private List<List<string>> GetPersonsPermuations(
            List<string> persons,
            int start,
            int end,
            List<List<string>> personsPermutations
        )
        {
            if (start == end)
            {
                personsPermutations.Add(new List<string>(persons));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    SwapPersons(persons, start, i);
                    GetPersonsPermuations(persons, start + 1, end, personsPermutations);
                    SwapPersons(persons, start, i);
                }
            }

            return personsPermutations;
        }

        /// <summary>
        /// Swap two persons in a list.
        /// </summary>
        /// <param name="persons"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private void SwapPersons(List<string> persons, int a, int b)
        {
            string aPerson = persons[a];
            persons[a] = persons[b];
            persons[b] = aPerson;
        }
    }
}
