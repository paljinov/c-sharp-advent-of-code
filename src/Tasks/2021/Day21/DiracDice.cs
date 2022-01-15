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
            Dictionary<int, long> playersWins = playersStartingPositions.Keys.ToDictionary(p => p, p => (long)0);

            List<List<int>> diracDiceNumbers = GetDiracDiceNumbers(dieMaxNumber, 0, new List<int>());
            Dictionary<int, int> diracDiceNumbersSumsOccurrences = GetDiracDiceNumbersSumsOccurrences(diracDiceNumbers);

            DoCalculatePlayerWinsInMultipleUniverses(
                1,
                diracDiceNumbersSumsOccurrences,
                minimumWinnerScore,
                1,
                playersScores,
                playersWins
            );

            long numberOfUniversesInWhichWinningPlayerWins = playersWins.Select(ps => ps.Value).Max();

            return numberOfUniversesInWhichWinningPlayerWins;
        }

        private void DoCalculatePlayerWinsInMultipleUniverses(
            int playerIdTurn,
            Dictionary<int, int> diracDiceNumbersSumsOccurrences,
            int minimumWinnerScore,
            long universes,
            Dictionary<int, (int Space, int Score)> playersScores,
            Dictionary<int, long> playersWins
        )
        {
            int nextPlayerIdTurn = playersScores.ContainsKey(playerIdTurn + 1) ? playerIdTurn + 1 : 1;
            Dictionary<int, (int Space, int Score)> playersScoresCopy =
                new Dictionary<int, (int Space, int Score)>(playersScores);

            foreach (KeyValuePair<int, int> diracDieNumbersSum in diracDiceNumbersSumsOccurrences)
            {
                int space = playersScores[playerIdTurn].Space + diracDieNumbersSum.Key;
                int score = playersScores[playerIdTurn].Score;
                long universesAfterSplit = universes * diracDieNumbersSum.Value;

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

                // Check if player won and end the game
                if (score >= minimumWinnerScore)
                {
                    playersWins[playerIdTurn] += universesAfterSplit;
                }
                else
                {
                    playersScoresCopy[playerIdTurn] = (space, score);
                    DoCalculatePlayerWinsInMultipleUniverses(
                        nextPlayerIdTurn,
                        diracDiceNumbersSumsOccurrences,
                        minimumWinnerScore,
                        universesAfterSplit,
                        playersScoresCopy,
                        playersWins
                    );
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

        private List<List<int>> GetDiracDiceNumbers(int dieMaxNumber, int depth, List<int> diracDieNumbers)
        {
            List<List<int>> diracDiceNumbers = new List<List<int>>();

            if (depth >= TOTAL_NUMBER_OF_DIE_ROLLS_PER_TURN)
            {
                diracDiceNumbers.Add(diracDieNumbers);
            }
            else
            {
                for (int dieNumber = 1; dieNumber <= dieMaxNumber; dieNumber++)
                {
                    List<int> diracDieNumbersCopy = diracDieNumbers.ToList();
                    diracDieNumbersCopy.Add(dieNumber);

                    if (depth < TOTAL_NUMBER_OF_DIE_ROLLS_PER_TURN)
                    {
                        diracDiceNumbers.AddRange(GetDiracDiceNumbers(dieMaxNumber, depth + 1, diracDieNumbersCopy));
                    }
                }
            }

            return diracDiceNumbers;
        }

        private Dictionary<int, int> GetDiracDiceNumbersSumsOccurrences(List<List<int>> diracDiceNumbers)
        {
            Dictionary<int, int> diracDiceNumbersSumsOccurrences = new Dictionary<int, int>();

            foreach (List<int> diracDieNumbers in diracDiceNumbers)
            {
                int sum = diracDieNumbers.Sum();
                if (!diracDiceNumbersSumsOccurrences.ContainsKey(sum))
                {
                    diracDiceNumbersSumsOccurrences[sum] = 1;
                }
                else
                {
                    diracDiceNumbersSumsOccurrences[sum]++;
                }
            }

            return diracDiceNumbersSumsOccurrences;
        }
    }
}
