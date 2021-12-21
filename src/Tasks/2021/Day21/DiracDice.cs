using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day21
{
    public class DiracDice
    {
        private const int ROLL_TIMES = 3;

        private const int MAX_ROLL = 100;

        private const int WRAP_ROLL_TO = 1;

        private const int MINIMUM_WINNER_SCORE = 1000;

        private const int WRAP_AFTER = 10;

        public int CalculateProductOfLosingPlayerScoreMultipliedByNumberOfDieRolls(
            Dictionary<int, int> playersStartingPositions
        )
        {
            Dictionary<int, (int, int)> playersScores = InitializePlayersScores(playersStartingPositions);

            bool winnerFound = false;
            int dice = 1;
            int totalRolls = 0;

            while (!winnerFound)
            {
                for (int playerId = 1; playerId <= playersScores.Count; playerId++)
                {
                    int space = playersScores[playerId].Item1;
                    int score = playersScores[playerId].Item2;

                    List<int> rolls = new List<int>();
                    for (int j = 0; j < ROLL_TIMES; j++)
                    {
                        if (dice > MAX_ROLL)
                        {
                            dice = WRAP_ROLL_TO;
                        }

                        rolls.Add(dice);
                        dice++;
                    }

                    foreach (int roll in rolls)
                    {
                        space += roll;
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

                    if (score >= MINIMUM_WINNER_SCORE)
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
