using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day19
{
    public class ValidMessages
    {
        private const string BASE_LETTER_A = "a";

        private const string BASE_LETTER_B = "b";

        private const string RULE_ZERO = "0";

        public int CountMessagesWhichMatchRuleZero(Dictionary<string, List<string>> rules, string[] receivedMessages)
        {
            Dictionary<string, List<string>> letterRules = InitializeLetterRules(rules);

            while (!letterRules.ContainsKey(RULE_ZERO))
            {
                foreach (KeyValuePair<string, List<string>> rule in rules)
                {
                    if (!letterRules.ContainsKey(rule.Key))
                    {
                        GetLettersRules(rules, rule.Key, letterRules);
                    }
                }
            }

            int followRuleZero = CountMessagesThatMatchRuleZero(letterRules[RULE_ZERO], receivedMessages);

            return followRuleZero;
        }

        private Dictionary<string, List<string>> InitializeLetterRules(Dictionary<string, List<string>> rules)
        {
            Dictionary<string, List<string>> letterRules = new Dictionary<string, List<string>>();

            foreach (KeyValuePair<string, List<string>> rule in rules)
            {
                if (rule.Value.Count == 1)
                {
                    if (rule.Value[0] == BASE_LETTER_A)
                    {
                        letterRules.Add(rule.Key, rule.Value);
                    }
                    else if (rule.Value[0] == BASE_LETTER_B)
                    {
                        letterRules.Add(rule.Key, rule.Value);
                    }
                }
            }

            return letterRules;
        }

        private void GetLettersRules(
            Dictionary<string, List<string>> rules,
            string ruleKey,
            Dictionary<string, List<string>> letterRules
        )
        {
            bool areAllKeysKnown = AreAllKeysKnown(rules, ruleKey, letterRules);
            if (areAllKeysKnown)
            {
                // Permutations for all rules regarding current rule
                List<string> allPermutations = new List<string>();

                List<string> keyRules = rules[ruleKey];
                foreach (string keyRule in keyRules)
                {
                    // Permutations for this rule only
                    List<string> permutations = GetLettersPermutations(keyRule, letterRules);
                    allPermutations = allPermutations.Concat(permutations).ToList();
                }

                allPermutations = allPermutations.Distinct().ToList();
                letterRules.Add(ruleKey, allPermutations);
            }
        }

        private bool AreAllKeysKnown(
            Dictionary<string, List<string>> rules,
            string ruleKey,
            Dictionary<string, List<string>> letterRules
        )
        {
            string[] knownKeys = letterRules.Keys.ToArray();
            List<string> keyRules = rules[ruleKey];

            bool allKeysKnown = true;
            foreach (string rule in keyRules)
            {
                string[] keys = rule.Split(' ');
                foreach (string key in keys)
                {
                    if (!knownKeys.Contains(key))
                    {
                        allKeysKnown = false;
                        break;
                    }
                }

                if (!allKeysKnown)
                {
                    break;
                }
            }

            return allKeysKnown;
        }

        private List<string> GetLettersPermutations(string keyRule, Dictionary<string, List<string>> letterRules)
        {
            List<List<string>> permutationsForKey = new List<List<string>>();

            string[] keys = keyRule.Split(' ');
            foreach (string key in keys)
            {
                permutationsForKey.Add(letterRules[key]);
            }

            IEnumerable<string> permutations = new List<string> { null };
            foreach (var list in permutationsForKey)
            {
                // cross join the current result with each member of the next list
                permutations = permutations.SelectMany(o => list.Select(s => o + s));
            }

            return permutations.Distinct().ToList();
        }

        private int CountMessagesThatMatchRuleZero(List<string> ruleZero, string[] receivedMessages)
        {
            int followRule = 0;

            foreach (string receivedMessage in receivedMessages)
            {
                if (ruleZero.Contains(receivedMessage))
                {
                    followRule++;
                }
            }

            return followRule;
        }
    }
}
