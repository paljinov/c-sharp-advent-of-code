using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day4
{
    public class ValidPassports
    {
        public int GetValidPassportsWithExpectedFields(List<string> potentialPassports, string[] passportExpectedFields)
        {
            int validPassport = 0;

            foreach (var potentialPassport in potentialPassports)
            {
                int expectedFields = passportExpectedFields.Length;

                foreach (var passportExpectedField in passportExpectedFields)
                {
                    if (potentialPassport.Contains(passportExpectedField))
                    {
                        expectedFields--;
                    }
                    else
                    {
                        break;
                    }
                }

                if (expectedFields == 0)
                {
                    validPassport++;
                }
            }

            return validPassport;
        }

        public int GetValidPassportsWithExpectedFieldsAndValues(List<string> potentialPassports, string[] passportExpectedFields)
        {
            int validPassport = 0;

            foreach (var potentialPassport in potentialPassports)
            {
                int expectedFields = passportExpectedFields.Length;

                foreach (var passportExpectedField in passportExpectedFields)
                {
                    Regex passportRegex = new Regex($@"({passportExpectedField}):(\S+)");
                    Match match = passportRegex.Match(potentialPassport);
                    if (match.Success == false)
                    {
                        break;
                    }

                    GroupCollection groups = match.Groups;

                    string field = groups[1].Value;
                    string fieldValue = groups[2].Value;

                    switch (field)
                    {
                        case "byr":
                            if (int.Parse(fieldValue) >= 1920 && int.Parse(fieldValue) <= 2002)
                            {
                                expectedFields--;
                            }
                            break;
                        case "iyr":
                            if (int.Parse(fieldValue) >= 2010 && int.Parse(fieldValue) <= 2020)
                            {
                                expectedFields--;
                            }
                            break;
                        case "eyr":
                            if (int.Parse(fieldValue) >= 2020 && int.Parse(fieldValue) <= 2030)
                            {
                                expectedFields--;
                            }
                            break;
                        case "hgt":
                            if (fieldValue.Contains("cm"))
                            {
                                fieldValue = fieldValue.Replace("cm", "");
                                if (int.Parse(fieldValue) >= 150 && int.Parse(fieldValue) <= 193)
                                {
                                    expectedFields--;
                                }
                            }
                            else
                            {
                                fieldValue = fieldValue.Replace("in", "");
                                if (int.Parse(fieldValue) >= 59 && int.Parse(fieldValue) <= 76)
                                {
                                    expectedFields--;
                                }
                            }
                            break;
                        case "hcl":
                            Regex hairColorRegex = new Regex(@"^#[0-9a-f]{6}$");
                            if (hairColorRegex.Match(fieldValue).Success)
                            {
                                expectedFields--;
                            }
                            break;
                        case "ecl":
                            if (fieldValue.Contains("amb") || fieldValue.Contains("blu") || fieldValue.Contains("brn")
                                || fieldValue.Contains("gry") || fieldValue.Contains("grn")
                                || fieldValue.Contains("hzl") || fieldValue.Contains("oth"))
                            {
                                expectedFields--;
                            }
                            break;
                        case "pid":
                            Regex passportIdRegex = new Regex(@"^[0-9]{9}$");
                            if (passportIdRegex.Match(fieldValue).Success)
                            {
                                expectedFields--;
                            }
                            break;
                        default:
                            break;
                    }
                }

                if (expectedFields == 0)
                {
                    validPassport++;
                }
            }

            return validPassport;
        }
    }
}
