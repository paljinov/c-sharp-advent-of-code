using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day4
{
    public class PassportsRepository
    {
        public List<Passport> GetPotentialPassports(string input)
        {
            List<Passport> potentialPassports = new List<Passport>();

            string[] potentialPassportsString = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries
            );

            Regex passportRegex = new Regex(@"(\w+):(\S+)");

            foreach (string potentialPassportString in potentialPassportsString)
            {
                MatchCollection passportMatches = passportRegex.Matches(
                    potentialPassportString.Replace(Environment.NewLine, " ")
                );

                if (passportMatches.Count > 0)
                {
                    Passport potentialPassport = new Passport();

                    foreach (Match passportMatch in passportMatches)
                    {
                        GroupCollection groups = passportMatch.Groups;

                        switch (groups[1].Value)
                        {
                            case "byr":
                                potentialPassport.BirthYear = groups[2].Value;
                                break;
                            case "iyr":
                                potentialPassport.IssueYear = groups[2].Value;
                                break;
                            case "eyr":
                                potentialPassport.ExpirationYear = groups[2].Value;
                                break;
                            case "hgt":
                                potentialPassport.Height = groups[2].Value;
                                break;
                            case "hcl":
                                potentialPassport.HairColor = groups[2].Value;
                                break;
                            case "ecl":
                                potentialPassport.EyeColor = groups[2].Value;
                                break;
                            case "pid":
                                potentialPassport.PassportId = groups[2].Value;
                                break;
                        }
                    }

                    potentialPassports.Add(potentialPassport);
                }
            }

            return potentialPassports;
        }
    }
}
