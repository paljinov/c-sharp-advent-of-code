using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day25
{
    public class CodeRepository
    {
        public (int row, int column) GetCodeRowAndColumn(string input)
        {
            Regex codeRowAndColumnRegex = new Regex(@"row (\d+), column (\d+)");
            Match codeRowAndColumnMatch = codeRowAndColumnRegex.Match(input);

            int row = int.Parse(codeRowAndColumnMatch.Groups[1].Value);
            int column = int.Parse(codeRowAndColumnMatch.Groups[2].Value);

            return (row, column);
        }
    }
}
