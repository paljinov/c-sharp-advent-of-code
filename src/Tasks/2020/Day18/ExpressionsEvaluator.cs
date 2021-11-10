using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day18
{
    public class ExpressionsEvaluator
    {
        public long ResultingValuesSumForSamePrecedence(string[] expressions)
        {
            long totalResult = 0;

            foreach (string expression in expressions)
            {
                totalResult += CalculateResultForSamePrecedence(expression);
            }

            return totalResult;
        }

        public long ResultingValuesSumForAdditionBeforeMultiplication(string[] expressions)
        {
            long totalResult = 0;

            foreach (string expression in expressions)
            {
                totalResult += CalculateResultForAdditionBeforeMultiplication(expression);
            }

            return totalResult;
        }

        private long CalculateResultForSamePrecedence(string expression)
        {
            long result = 0;

            Regex nestedBracketsRegex = new Regex(@"\([\d+*\s]+?\)");
            Regex splitByOperationRegex = new Regex(@"(\s[\+\*]\s)");

            MatchCollection nestedBracketsMatches = nestedBracketsRegex.Matches(expression);
            // If there are nested brackets results in them are calculated first
            if (nestedBracketsMatches.Count > 0)
            {
                foreach (Match nestedBracketMatch in nestedBracketsMatches)
                {
                    string nestedBracket = nestedBracketMatch.Groups[0].Value;
                    long nestedBracketResult = CalculateResultForSamePrecedence(nestedBracket[1..^1]);
                    expression = expression.Replace(nestedBracket, nestedBracketResult.ToString());
                }

                return CalculateResultForSamePrecedence(expression);
            }
            // If there aren't any nested brackets
            else
            {
                string[] splittedExpression = splitByOperationRegex.Split(expression);

                for (int i = 0; i < splittedExpression.Length; i++)
                {
                    string current = splittedExpression[i];

                    bool isNumeric = int.TryParse(splittedExpression[i], out int integer);
                    if (isNumeric)
                    {
                        // If first number or operation is addition
                        if (i == 0 || splittedExpression[i - 1] == " + ")
                        {
                            result += integer;
                        }
                        // If operation is multiplication
                        else
                        {
                            result *= integer;
                        }
                    }
                }
            }

            return result;
        }

        private long CalculateResultForAdditionBeforeMultiplication(string expression)
        {
            long result = 1;

            Regex nestedBracketsRegex = new Regex(@"\([\d+*\s]+?\)");
            Regex additionRegex = new Regex(@"(\d+)\s\+\s(\d+)");

            MatchCollection nestedBracketsMatches = nestedBracketsRegex.Matches(expression);
            // If there are nested brackets results in them are calculated first
            if (nestedBracketsMatches.Count > 0)
            {
                foreach (Match nestedBracketMatch in nestedBracketsMatches)
                {
                    string nestedBracket = nestedBracketMatch.Groups[0].Value;
                    long nestedBracketResult = CalculateResultForAdditionBeforeMultiplication(nestedBracket[1..^1]);
                    expression = expression.Replace(nestedBracket, nestedBracketResult.ToString());
                }

                return CalculateResultForAdditionBeforeMultiplication(expression);
            }
            // If there aren't any nested brackets
            else
            {
                // If there are additions which needs to be done first
                while (expression.Contains('+'))
                {
                    Match additionMatch = additionRegex.Match(expression);
                    long sum = long.Parse(additionMatch.Groups[1].Value) + long.Parse(additionMatch.Groups[2].Value);
                    expression = additionRegex.Replace(expression, sum.ToString(), 1);
                }

                string[] integers = expression.Split(" * ");
                // Only multiplication operations are left
                foreach (string integer in integers)
                {
                    result *= long.Parse(integer);
                }
            }

            return result;
        }
    }
}
