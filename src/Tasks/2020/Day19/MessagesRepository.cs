using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day19
{
    public class MessagesRepository
    {
        public Dictionary<string, List<string>> GetRules(string input)
        {
            Dictionary<string, List<string>> rules = new Dictionary<string, List<string>>();

            string[] inputParts = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries
            );

            string[] rulesString = inputParts[0].Split(Environment.NewLine);

            Regex rulesRegex = new Regex(@"(\d+):\s(.+)");

            foreach (string ruleString in rulesString)
            {
                Match match = rulesRegex.Match(ruleString);
                GroupCollection groups = match.Groups;

                List<string> subRules = new List<string>();
                string rule = groups[2].Value;
                if (rule == "\"a\"")
                {
                    subRules.Add("a");
                }
                else if (rule == "\"b\"")
                {
                    subRules.Add("b");
                }
                else if (rule.Contains('|'))
                {
                    subRules = rule.Split(" | ").ToList();
                }
                else
                {
                    subRules.Add(rule);
                }

                rules.Add(groups[1].Value, subRules);
            }

            return rules;
        }

        public string[] GetReceivedMessages(string input)
        {
            string[] inputParts = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries
            );

            string[] receivedMessages = inputParts[1].Split(Environment.NewLine);

            return receivedMessages;
        }
    }
}
