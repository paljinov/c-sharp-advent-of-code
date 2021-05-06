using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day9
{
    public class MarblesGameRepository
    {
        public int GetPlayers(string input)
        {
            (int players, _) = ParseInput(input);
            return players;
        }

        public int GetLastMarble(string input)
        {
            (_, int lastMarble) = ParseInput(input);
            return lastMarble;
        }

        private (int, int) ParseInput(string input)
        {
            Regex gameInputRegex = new Regex(@"^(\d+)\splayers;\slast\smarble\sis\sworth\s(\d+)\spoints$");

            Match gameInputMatch = gameInputRegex.Match(input);
            GroupCollection gameInputGroups = gameInputMatch.Groups;

            int players = int.Parse(gameInputGroups[1].Value);
            int lastMarble = int.Parse(gameInputGroups[2].Value);

            return (players, lastMarble);
        }
    }
}
