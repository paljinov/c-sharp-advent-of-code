using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day21
{
    public class DiracDice
    {
        private const int TOTAL_NUMBER_OF_DIE_ROLLS_PER_TURN = 3;

        private const int DIE_MAX_NUMBER = 100;

        private const int DIE_MIN_NUMBER = 1;

        private const int WRAP_AFTER = 10;

        public int CalculateProductOfLosingPlayerScoreMultipliedByNumberOfDieRolls(
            Dictionary<int, int> playersStartingPositions,
            int minimumWinnerScore
        )
        {
            Dictionary<int, (int Space, int Score)> playersScores = InitializePlayersScores(playersStartingPositions);

            bool gameEnd = false;
            int dieNumber = 1;
            int totalNumberOfDieRolls = 0;

            while (!gameEnd)
            {
                for (int playerId = 1; playerId <= playersScores.Count; playerId++)
                {
                    int space = playersScores[playerId].Space;
                    int score = playersScores[playerId].Score;

                    // The player rolls the die three times and adds up the results
                    List<int> numbers = new List<int>();
                    for (int i = 0; i < TOTAL_NUMBER_OF_DIE_ROLLS_PER_TURN; i++)
                    {
                        if (dieNumber > DIE_MAX_NUMBER)
                        {
                            dieNumber = DIE_MIN_NUMBER;
                        }

                        numbers.Add(dieNumber);
                        space += dieNumber;
                        dieNumber++;
                    }

                    // Check wrap back
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
                    totalNumberOfDieRolls += TOTAL_NUMBER_OF_DIE_ROLLS_PER_TURN;

                    // Check if player won and end the game
                    if (score >= minimumWinnerScore)
                    {
                        gameEnd = true;
                        break;
                    }
                }
            }

            int losingPlayerScore = playersScores.Select(ps => ps.Value.Score).Min();
            int productOfLosingPlayerScoreMultipliedByNumberOfDieRolls = losingPlayerScore * totalNumberOfDieRolls;

            return productOfLosingPlayerScoreMultipliedByNumberOfDieRolls;
        }

        public long CalculateNumberOfUniversesInWhichWinningPlayerWins(
            Dictionary<int, int> playersStartingPositions,
            int minimumWinnerScore
       )
        {
            return 0;
        }

        private Dictionary<int, (int Space, int Score)> InitializePlayersScores(
            Dictionary<int, int> playersStartingPositions
        )
        {
            Dictionary<int, (int Space, int Score)> playersScores = new Dictionary<int, (int Space, int Score)>();
            foreach (KeyValuePair<int, int> player in playersStartingPositions)
            {
                playersScores[player.Key] = (player.Value, 0);
            }

            return playersScores;
        }
    }
}
