using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day2
{
    public class PasswordPolicyRepository
    {
        public List<PasswordPolicy> GetPasswordPolicies(string input)
        {
            List<PasswordPolicy> passwordPolicies = new List<PasswordPolicy>();

            Regex passwordPoliciesRegex = new Regex(@"^(\d+)\-(\d+)\s(\w):\s(\w+)$");

            string[] passwordPoliciesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string passwordPolicyString in passwordPoliciesString)
            {
                Match match = passwordPoliciesRegex.Match(passwordPolicyString);
                GroupCollection groups = match.Groups;

                passwordPolicies.Add(new PasswordPolicy
                {
                    Min = int.Parse(groups[1].Value),
                    Max = int.Parse(groups[2].Value),
                    Letter = groups[3].Value,
                    Password = groups[4].Value
                });
            }

            return passwordPolicies;
        }
    }
}
