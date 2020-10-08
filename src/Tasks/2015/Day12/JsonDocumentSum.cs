using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day12
{
    class JsonDocumentSum
    {
        public int Calculate(string input)
        {
            int sum = 0;

            Regex digitRegex = new Regex(@"(-?\d+)");
            MatchCollection digitMatches = digitRegex.Matches(input);

            foreach (Match digitMatch in digitMatches)
            {
                GroupCollection groups = digitMatch.Groups;
                sum += int.Parse(groups[1].Value);
            }

            return sum;
        }
    }
}
