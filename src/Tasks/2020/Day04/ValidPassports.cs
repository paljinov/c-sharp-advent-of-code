using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day4
{
    public class ValidPassports
    {
        public int GetValidPassportsWithExpectedFields(List<Passport> potentialPassports)
        {
            int validPassport = 0;

            foreach (Passport potentialPassport in potentialPassports)
            {
                if (string.IsNullOrEmpty(potentialPassport.BirthYear))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(potentialPassport.IssueYear))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(potentialPassport.ExpirationYear))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(potentialPassport.Height))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(potentialPassport.HairColor))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(potentialPassport.EyeColor))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(potentialPassport.PassportId))
                {
                    continue;
                }

                validPassport++;
            }

            return validPassport;
        }

        public int GetValidPassportsWithExpectedFieldsAndValues(List<Passport> potentialPassports)
        {
            int validPassport = 0;

            Regex birthYearRegex = new Regex(@"^19[2-8][0-9]|199[0-9]|200[0-2]$");
            Regex issueYearRegex = new Regex(@"^201[0-9]|2020$");
            Regex expirationYearRegex = new Regex(@"^202[0-9]|2030$");
            Regex heightCmRegex = new Regex(@"^(1[5-8][0-9]|19[0-3])$");
            Regex heightInRegex = new Regex(@"^(59|6[0-9]|7[0-6])$");
            Regex hairColorRegex = new Regex(@"^#[0-9a-f]{6}$");
            Regex eyeColorRegex = new Regex(@"^amb|blu|brn|gry|grn|hzl|oth$");
            Regex passportIdRegex = new Regex(@"^[0-9]{9}$");

            foreach (Passport potentialPassport in potentialPassports)
            {
                if (!birthYearRegex.Match(potentialPassport.BirthYear).Success)
                {
                    continue;
                }

                if (!issueYearRegex.Match(potentialPassport.IssueYear).Success)
                {
                    continue;
                }

                if (!expirationYearRegex.Match(potentialPassport.ExpirationYear).Success)
                {
                    continue;
                }

                if (potentialPassport.Height.Contains("cm"))
                {
                    string height = potentialPassport.Height.Replace("cm", "");
                    if (!heightCmRegex.Match(height).Success)
                    {
                        continue;
                    }
                }
                else if (potentialPassport.Height.Contains("in"))
                {
                    string height = potentialPassport.Height.Replace("in", "");
                    if (!heightInRegex.Match(height).Success)
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }

                if (!hairColorRegex.Match(potentialPassport.HairColor).Success)
                {
                    continue;
                }

                if (!eyeColorRegex.Match(potentialPassport.EyeColor).Success)
                {
                    continue;
                }

                if (!passportIdRegex.Match(potentialPassport.PassportId).Success)
                {
                    continue;
                }

                validPassport++;
            }

            return validPassport;
        }
    }
}
