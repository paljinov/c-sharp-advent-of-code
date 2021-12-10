using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day10
{
    public class SyntaxScore
    {
        private const int INCOMPLETE_LINES_SCORE_MULTIPLICATORV = 5;

        private readonly Dictionary<BracketType, (char Opening, char Closing)> brackets =
            new Dictionary<BracketType, (char Opening, char Closing)>()
        {
            { BracketType.RoundBracket, ('(', ')')},
            { BracketType.SquareBracket, ('[', ']')},
            { BracketType.CurlyBracket, ('{', '}')},
            { BracketType.AngleBracket, ('<', '>')}
        };

        private readonly Dictionary<BracketType, int> corruptedLinesSyntaxErrorScore =
            new Dictionary<BracketType, int>()
            {
                { BracketType.RoundBracket, 3},
                { BracketType.SquareBracket, 57},
                { BracketType.CurlyBracket, 1197},
                { BracketType.AngleBracket, 25137}
            };

        private readonly Dictionary<BracketType, int> incompleteLinesScore = new Dictionary<BracketType, int>()
        {
            { BracketType.RoundBracket, 1},
            { BracketType.SquareBracket, 2},
            { BracketType.CurlyBracket, 3},
            { BracketType.AngleBracket, 4}
        };


        public int CalculateTotalErrorsSyntaxScore(string[] navigationSubsystem)
        {
            int totalErrorsSyntaxScore = 0;

            Dictionary<int, BracketType> corruptedLines = GetCorruptedLines(navigationSubsystem);

            foreach (KeyValuePair<int, BracketType> corruptedLine in corruptedLines)
            {
                totalErrorsSyntaxScore += corruptedLinesSyntaxErrorScore[corruptedLine.Value];
            }

            return totalErrorsSyntaxScore;
        }

        public long CalculateAutocompleteToolsMiddleScore(string[] navigationSubsystem)
        {
            List<string> incompleteLines = GetIncompleteLines(navigationSubsystem);
            char[] openingBrackets = brackets.Values.Select(b => b.Opening).ToArray();

            List<long> incompleteLinesScores = new List<long>();

            foreach (string incompleteLine in incompleteLines)
            {
                Stack<BracketType> lineBrackets = new Stack<BracketType>();
                foreach (char bracket in incompleteLine)
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
                        lineBrackets.Pop();
                    }
                }

                long incompleteLineScore = CalculateIncompleteLineScore(lineBrackets);
                incompleteLinesScores.Add(incompleteLineScore);
            }

            incompleteLinesScores.Sort();
            long autocompleteToolsMiddleScore = incompleteLinesScores[incompleteLinesScores.Count / 2];

            return autocompleteToolsMiddleScore;
        }

        private Dictionary<int, BracketType> GetCorruptedLines(string[] navigationSubsystem)
        {
            Dictionary<int, BracketType> corruptedLines = new Dictionary<int, BracketType>();

            char[] openingBrackets = brackets.Values.Select(b => b.Opening).ToArray();

            for (int i = 0; i < navigationSubsystem.Length; i++)
            {
                string line = navigationSubsystem[i];

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

                            corruptedLines.Add(i, illegalBracketType);

                            // Stop at the first incorrect closing character on each corrupted line
                            break;
                        }
                    }
                }
            }

            return corruptedLines;
        }

        private List<string> GetIncompleteLines(string[] navigationSubsystem)
        {
            List<string> incompleteLines = new List<string>();

            Dictionary<int, BracketType> corruptedLines = GetCorruptedLines(navigationSubsystem);

            for (int i = 0; i < navigationSubsystem.Length; i++)
            {
                if (!corruptedLines.ContainsKey(i))
                {
                    incompleteLines.Add(navigationSubsystem[i]);
                }
            }

            return incompleteLines;
        }

        private long CalculateIncompleteLineScore(Stack<BracketType> lineBrackets)
        {
            long incompleteLineScore = 0;

            foreach (BracketType bracket in lineBrackets)
            {
                incompleteLineScore *= INCOMPLETE_LINES_SCORE_MULTIPLICATORV;
                incompleteLineScore += incompleteLinesScore[bracket];
            }

            return incompleteLineScore;
        }
    }
}
