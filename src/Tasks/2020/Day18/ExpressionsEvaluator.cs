using System.Text;

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

        private long CalculateResultForSamePrecedence(string expression)
        {
            long result = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsDigit(expression[i]))
                {
                    int integer = (int)char.GetNumericValue(expression[i]);
                    result = CalculateNewResultForSamePrecedence(result, integer, expression, i);
                }
                else if (expression[i] == '(')
                {
                    (string bracketsExpression, int endPosition) = GetExpressionsInMatchingBrackets(expression, i);

                    long bracketsResult = CalculateResultForSamePrecedence(bracketsExpression);
                    result = CalculateNewResultForSamePrecedence(result, bracketsResult, expression, i);

                    i = endPosition + 1;
                }
            }

            return result;
        }

        private long CalculateNewResultForSamePrecedence(long result, long integer, string expression, int i)
        {
            if (i - 2 < 0 || expression[i - 2] == '+')
            {
                result += integer;
            }
            else if (expression[i - 2] == '*')
            {
                result *= integer;
            }

            return result;
        }

        private (string bracketExpression, int endPosition) GetExpressionsInMatchingBrackets(string expression, int i)
        {
            int openedBrackets = 0;
            int endPosition = 0;

            StringBuilder bracketExpression = new StringBuilder();

            for (int j = i; j < expression.Length; j++)
            {
                if (expression[j] == '(')
                {
                    openedBrackets++;
                    if (openedBrackets == 1)
                    {
                        continue;
                    }
                }
                else if (expression[j] == ')')
                {
                    openedBrackets--;
                    if (openedBrackets == 0)
                    {
                        endPosition = j;
                        break;
                    }
                }

                bracketExpression.Append(expression[j]);
            }

            return (bracketExpression.ToString(), endPosition);
        }
    }
}
