using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day4
{
    public class PassportsRepository
    {
        public readonly string[] PassportExpectedFields = {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid"
            // "cid"
        };

        public List<string> GetPotentialPassports(string input)
        {
            List<string> potentialPassports = new List<string>();

            string[] potentialPassportsString = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries
            );

            foreach (string potentialPassportString in potentialPassportsString)
            {
                potentialPassports.Add(potentialPassportString.Replace(Environment.NewLine, " "));
            }

            return potentialPassports;
        }
    }
}
