using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day19
{
    public class ValidMessages
    {
        private const string BASE_LETTER_A = "a";

        private const string BASE_LETTER_B = "b";

        private const string RULE_ZERO = "0";

        private const string RULE_FORTY_TWO = "42";

        private const string RULE_THIRTY_ONE = "31";

        public int CountMessagesWhichMatchRuleZero(Dictionary<string, List<string>> rules, string[] receivedMessages)
        {
            int followRuleZero;

            Dictionary<string, List<string>> letterRules = InitializeLetterRules(rules);

            while (!letterRules.ContainsKey(RULE_ZERO))
            {
                int letterRulesCount = letterRules.Count;
                foreach (KeyValuePair<string, List<string>> rule in rules)
                {
                    if (!letterRules.ContainsKey(rule.Key))
                    {
                        GetLettersRules(rules, rule.Key, letterRules);
                    }
                }

                // If new letter rules weren't discover loop happened
                if (letterRulesCount == letterRules.Count)
                {
                    break;
                }
            }

            // If there are no loops in the rules
            if (rules.Count == letterRules.Count)
            {
                followRuleZero = CountMessagesThatMatchRuleZero(letterRules[RULE_ZERO], receivedMessages);
            }
            // If there are loops in the rules
            else
            {
                followRuleZero = CountMessagesThatMatchRuleZeroForLoops(letterRules, receivedMessages);
            }

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
            foreach (List<string> list in permutationsForKey)
            {
                // Cross join the current result with each member of the next list
                permutations = permutations.SelectMany(l => list.Select(p => l + p));
            }

            return permutations.Distinct().ToList();
        }

        private int CountMessagesThatMatchRuleZero(List<string> ruleZero, string[] receivedMessages)
        {
            int followRuleZero = 0;

            foreach (string receivedMessage in receivedMessages)
            {
                if (ruleZero.Contains(receivedMessage))
                {
                    followRuleZero++;
                }
            }

            return followRuleZero;
        }

        /// <summary>
        /// Rule 8: 42 | 42 8 => 42 | 42 42 | 42 42 42 | ...
        /// Rule 11: 42 31 | 42 11 31 => 42 31 | 42 42 31 31 | 42 42 42 31 31 31 | ...
        /// Rule 0: 8 11 => 42{m} 31{n}, where m > n
        /// </summary>
        /// <param name="letterRules"></param>
        /// <param name="receivedMessages"></param>
        /// <returns></returns>
        private int CountMessagesThatMatchRuleZeroForLoops(
            Dictionary<string, List<string>> letterRules,
            string[] receivedMessages
        )
        {
            int followRuleZero = 0;
            int ruleFortyTwoLength = letterRules[RULE_FORTY_TWO][0].Length;
            int ruleThirtyOneLength = letterRules[RULE_THIRTY_ONE][0].Length;

            for (int i = 0; i < receivedMessages.Length; i++)
            {
                string receivedMessage = receivedMessages[i];

                int ruleThirtyOneCount = 0;
                int ruleFortyTwoCount = 0;

                while (receivedMessage.Length >= ruleThirtyOneLength
                    && letterRules[RULE_THIRTY_ONE].Contains(receivedMessage[^ruleThirtyOneLength..]))
                {
                    receivedMessage = receivedMessage[..^ruleThirtyOneLength];
                    ruleThirtyOneCount++;
                }

                // If rule "31" is satisfied
                if (ruleThirtyOneCount > 0)
                {
                    while (receivedMessage.Length >= ruleFortyTwoLength
                        && letterRules[RULE_FORTY_TWO].Contains(receivedMessage[..ruleFortyTwoLength]))
                    {
                        receivedMessage = receivedMessage[ruleFortyTwoLength..];
                        ruleFortyTwoCount++;
                    }

                    // If rule "42" is satisfied and repeats more than rule "31", and whole received message matches
                    if (ruleFortyTwoCount >= ruleThirtyOneCount + 1 && receivedMessage.Length == 0)
                    {
                        followRuleZero++;
                    }
                }
            }

            return followRuleZero;
        }
    }
}
