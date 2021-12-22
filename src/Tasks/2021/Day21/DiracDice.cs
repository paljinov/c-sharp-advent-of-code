using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day21
{
    public class DiracDice
    {
        private const int ROLL_TIMES = 3;

        private const int MAX_ROLL = 100;

        private const int WRAP_ROLL_TO = 1;

        private const int WRAP_AFTER = 10;

        public int CalculateProductOfLosingPlayerScoreMultipliedByNumberOfDieRolls(
            Dictionary<int, int> playersStartingPositions,
            int minimumWinnerScore
        )
        {
            Dictionary<int, (int, int)> playersScores = InitializePlayersScores(playersStartingPositions);

            bool winnerFound = false;
            int rollOutcome = 1;
            int totalRolls = 0;

            while (!winnerFound)
            {
                for (int playerId = 1; playerId <= playersScores.Count; playerId++)
                {
                    int space = playersScores[playerId].Item1;
                    int score = playersScores[playerId].Item2;

                    List<int> rollsOutcomes = new List<int>();
                    for (int j = 0; j < ROLL_TIMES; j++)
                    {
                        if (rollOutcome > MAX_ROLL)
                        {
                            rollOutcome = WRAP_ROLL_TO;
                        }

                        rollsOutcomes.Add(rollOutcome);
                        space += rollOutcome;
                        rollOutcome++;
                    }

                    if (space > WRAP_AFTER)
                    {
                        space %= WRAP_AFTER;
                        if (space == 0)
                        {
                            space = 10;
                        }
                    }

                    score += space;
                    playersScores[playerId] = (space, score);
                    totalRolls += ROLL_TIMES;

                    if (score >= minimumWinnerScore)
                    {
                        winnerFound = true;
                        break;
                    }
                }
            }

            int loserScore = playersScores.Select(ps => ps.Value.Item2).Min();
            int productOfLosingPlayerScoreMultipliedByNumberOfDieRolls = totalRolls * loserScore;

            return productOfLosingPlayerScoreMultipliedByNumberOfDieRolls;
        }

        public long CalculateNumberOfUniversesInWhichWinningPlayerWins(
            Dictionary<int, int> playersStartingPositions,
            int minimumWinnerScore
       )
        {
            return 0;
        }

        private Dictionary<int, (int, int)> InitializePlayersScores(
            Dictionary<int, int> playersStartingPositions
        )
        {
            Dictionary<int, (int, int)> playersScores = new Dictionary<int, (int, int)>();
            foreach (KeyValuePair<int, int> player in playersStartingPositions)
            {
                playersScores[player.Key] = (player.Value, 0);
            }

            return playersScores;
        }
    }
}
