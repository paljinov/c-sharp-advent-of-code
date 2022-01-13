using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day21
{
    public class DiracDice
    {
        private const int TOTAL_NUMBER_OF_DIE_ROLLS_PER_TURN = 3;

        private const int DIE_MIN_NUMBER = 1;

        private const int WRAP_AFTER = 10;

        public int CalculateProductOfLosingPlayerScoreMultipliedByNumberOfDieRolls(
            Dictionary<int, int> playersStartingPositions,
            int dieMaxNumber,
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
                    for (int i = 0; i < TOTAL_NUMBER_OF_DIE_ROLLS_PER_TURN; i++)
                    {
                        if (dieNumber > dieMaxNumber)
                        {
                            dieNumber = DIE_MIN_NUMBER;
                        }

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
            int dieMaxNumber,
            int minimumWinnerScore
        )
        {
            Dictionary<int, (int Space, int Score)> playersScores = InitializePlayersScores(playersStartingPositions);
            Dictionary<int, long> playersWins = new Dictionary<int, long>();
            foreach (int playerId in playersScores.Keys)
            {
                playersWins[playerId] = 0;
            }
            List<int> diracDiceNumbersTurnSums = GetDiracDiceNumbersTurnSums(dieMaxNumber);

            DoCalculatePlayerWinsInMultipleUniverses(1, diracDiceNumbersTurnSums, minimumWinnerScore, playersScores, playersWins);

            long numberOfUniversesInWhichWinningPlayerWins = playersWins.Select(ps => ps.Value).Max();

            return numberOfUniversesInWhichWinningPlayerWins;
        }

        private void DoCalculatePlayerWinsInMultipleUniverses(
            int playerIdTurn,
            List<int> diracDiceNumbersTurnSums,
            int minimumWinnerScore,
            Dictionary<int, (int Space, int Score)> playersScores,
            Dictionary<int, long> playersWins
        )
        {
            int nextPlayerIdTurn = playersScores.ContainsKey(playerIdTurn + 1) ? playerIdTurn + 1 : 1;

            foreach (int diracDiceNumbersTurnSum in diracDiceNumbersTurnSums)
            {
                Dictionary<int, (int Space, int Score)> playersScoresCopy =
                    playersScores.ToDictionary(ps => ps.Key, ps => ps.Value);

                int space = playersScoresCopy[playerIdTurn].Space;
                int score = playersScoresCopy[playerIdTurn].Score;

                space += diracDiceNumbersTurnSum;

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
                playersScoresCopy[playerIdTurn] = (space, score);

                // Check if player won and end the game
                if (score >= minimumWinnerScore)
                {
                    playersWins[playerIdTurn] += 1;
                    if (playersWins[playerIdTurn] % 10000000 == 0)
                    {
                        System.Console.WriteLine($"Player {playerIdTurn} wins: {playersWins[playerIdTurn]}");
                    }
                }
                else
                {
                    DoCalculatePlayerWinsInMultipleUniverses(
                        nextPlayerIdTurn, diracDiceNumbersTurnSums, minimumWinnerScore, playersScoresCopy, playersWins);
                }
            }
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

        private List<int> GetDiracDiceNumbersTurnSums(int dieMaxNumber)
        {
            List<int> diracDiceNumbers = new List<int>()
            {
                (new List<int>(){1,1,1}).Sum(),
                (new List<int>(){1,1,2}).Sum(),
                (new List<int>(){1,1,3}).Sum(),
                (new List<int>(){1,2,1}).Sum(),
                (new List<int>(){1,2,2}).Sum(),
                (new List<int>(){1,2,3}).Sum(),
                (new List<int>(){1,3,1}).Sum(),
                (new List<int>(){1,3,2}).Sum(),
                (new List<int>(){1,3,3}).Sum(),
                (new List<int>(){2,1,1}).Sum(),
                (new List<int>(){2,1,2}).Sum(),
                (new List<int>(){2,1,3}).Sum(),
                (new List<int>(){2,2,1}).Sum(),
                (new List<int>(){2,2,2}).Sum(),
                (new List<int>(){2,2,3}).Sum(),
                (new List<int>(){2,3,1}).Sum(),
                (new List<int>(){2,3,2}).Sum(),
                (new List<int>(){2,3,3}).Sum(),
                (new List<int>(){3,1,1}).Sum(),
                (new List<int>(){3,1,2}).Sum(),
                (new List<int>(){3,1,3}).Sum(),
                (new List<int>(){3,2,1}).Sum(),
                (new List<int>(){3,2,2}).Sum(),
                (new List<int>(){3,2,3}).Sum(),
                (new List<int>(){3,3,1}).Sum(),
                (new List<int>(){3,3,2}).Sum(),
                (new List<int>(){3,3,3}).Sum()
            };

            return diracDiceNumbers;
        }
    }
}
