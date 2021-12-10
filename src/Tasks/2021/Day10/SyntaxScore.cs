using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day10
{
    public class SyntaxScore
    {
        private readonly Dictionary<BracketType, (char Opening, char Closing)> brackets =
            new Dictionary<BracketType, (char Opening, char Closing)>()
        {
            { BracketType.RoundBracket, ('(', ')')},
            { BracketType.SquareBracket, ('[', ']')},
            { BracketType.CurlyBracket, ('{', '}')},
            { BracketType.AngleBracket, ('<', '>')}
        };

        private readonly Dictionary<BracketType, int> bracketsSyntaxErrorScore = new Dictionary<BracketType, int>()
        {
            { BracketType.RoundBracket, 3},
            { BracketType.SquareBracket, 57},
            { BracketType.CurlyBracket, 1197},
            { BracketType.AngleBracket, 25137}
        };

        public int CalculateTotalErrorsSyntaxScore(string[] navigationSubsystem)
        {
            int totalErrorsSyntaxScore = 0;

            char[] openingBrackets = brackets.Values.Select(b => b.Opening).ToArray();

            foreach (string line in navigationSubsystem)
            {
                Stack<BracketType> lineBrackets = new Stack<BracketType>();
                foreach (char bracket in line)
                {
                    // Opening bracket
                    if (openingBrackets.Contains(bracket))
                    {
                        BracketType bracketType = brackets
                            .Where(b => b.Value.Opening == bracket)
                            .Select(b => b.Key).First();

                        lineBrackets.Push(bracketType);
                    }
                    // Closing bracket
                    else
                    {
                        char closingBracket = brackets[lineBrackets.Peek()].Closing;
                        // If closing bracket is expected
                        if (bracket == closingBracket)
                        {
                            lineBrackets.Pop();
                        }
                        // If syntax error
                        else
                        {
                            BracketType illegalBracketType = brackets
                                .Where(b => b.Value.Closing == bracket)
                                .Select(b => b.Key).First();

                            totalErrorsSyntaxScore += bracketsSyntaxErrorScore[illegalBracketType];
                            // Stop at the first incorrect closing character on each corrupted line
                            break;
                        }
                    }
                }
            }

            return totalErrorsSyntaxScore;
        }
    }
}
