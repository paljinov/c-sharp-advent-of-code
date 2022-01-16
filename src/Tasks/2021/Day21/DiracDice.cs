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
            Dictionary<int, (int Space, int Score)> players = InitializePlayers(playersStartingPositions);

            bool gameEnd = false;
            int dieNumber = 1;
            int totalNumberOfDieRolls = 0;

            while (!gameEnd)
            {
                for (int playerId = 1; playerId <= players.Count; playerId++)
                {
                    int space = players[playerId].Space;
                    int score = players[playerId].Score;

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

                    space = WrapBackIfNeeded(space);
                    score += space;

                    players[playerId] = (space, score);
                    totalNumberOfDieRolls += TOTAL_NUMBER_OF_DIE_ROLLS_PER_TURN;

                    // Check if player won and end the game
                    if (score >= minimumWinnerScore)
                    {
                        gameEnd = true;
                        break;
                    }
                }
            }

            int losingPlayerScore = players.Select(ps => ps.Value.Score).Min();
            int productOfLosingPlayerScoreMultipliedByNumberOfDieRolls = losingPlayerScore * totalNumberOfDieRolls;

            return productOfLosingPlayerScoreMultipliedByNumberOfDieRolls;
        }

        public long CalculateNumberOfUniversesInWhichWinningPlayerWins(
            Dictionary<int, int> playersStartingPositions,
            int dieMaxNumber,
            int minimumWinnerScore
        )
        {
            Dictionary<int, (int Space, int Score)> players = InitializePlayers(playersStartingPositions);

            List<List<int>> diracDiceNumbers = GetDiracDiceNumbers(dieMaxNumber, 0, new List<int>());
            Dictionary<int, int> diracDiceNumbersSumsOccurrences = GetDiracDiceNumbersSumsOccurrences(diracDiceNumbers);

            Dictionary<string, Dictionary<int, long>> playersWinsCache =
                new Dictionary<string, Dictionary<int, long>>();

            Dictionary<int, long> playersWins = DoCalculatePlayersWinsInMultipleUniverses(
                1,
                diracDiceNumbersSumsOccurrences,
                minimumWinnerScore,
                players,
                playersWinsCache
            );

            long numberOfUniversesInWhichWinningPlayerWins = playersWins.Select(ps => ps.Value).Max();

            return numberOfUniversesInWhichWinningPlayerWins;
        }

        private Dictionary<int, long> DoCalculatePlayersWinsInMultipleUniverses(
            int playerIdTurn,
            Dictionary<int, int> diracDiceNumbersSumsOccurrences,
            int minimumWinnerScore,
            Dictionary<int, (int Space, int Score)> players,
            Dictionary<string, Dictionary<int, long>> playersWinsCache
        )
        {
            Dictionary<int, long> playersWins = players.Keys.ToDictionary(p => p, p => (long)0);

            string playersWinsCacheKey = StringifyPlayers(players, playerIdTurn);
            // If players wins are found in cache
            if (playersWinsCache.ContainsKey(playersWinsCacheKey))
            {
                playersWins = playersWinsCache[playersWinsCacheKey];
            }
            // If players wins are not found in cache
            else
            {
                int nextPlayerIdTurn = players.ContainsKey(playerIdTurn + 1) ? playerIdTurn + 1 : 1;

                foreach (KeyValuePair<int, int> diracDieNumbersSum in diracDiceNumbersSumsOccurrences)
                {
                    Dictionary<int, (int Space, int Score)> updatedPlayers =
                        new Dictionary<int, (int Space, int Score)>(players);

                    int space = WrapBackIfNeeded(players[playerIdTurn].Space + diracDieNumbersSum.Key);
                    int score = players[playerIdTurn].Score + space;
                    long universes = diracDieNumbersSum.Value;

                    // Check if player won and end the game
                    if (score >= minimumWinnerScore)
                    {
                        playersWins[playerIdTurn] += universes;
                    }
                    else
                    {
                        updatedPlayers[playerIdTurn] = (space, score);
                        playersWinsCacheKey = StringifyPlayers(updatedPlayers, nextPlayerIdTurn);

                        // Cache stores players wins starting from single universe
                        playersWinsCache[playersWinsCacheKey] = DoCalculatePlayersWinsInMultipleUniverses(
                            nextPlayerIdTurn,
                            diracDiceNumbersSumsOccurrences,
                            minimumWinnerScore,
                            updatedPlayers,
                            playersWinsCache
                        );

                        playersWins = playersWins
                            // Multiply wins by number of universes
                            .Concat(playersWinsCache[playersWinsCacheKey]
                                .ToDictionary(pw => pw.Key, pw => universes * pw.Value))
                            .GroupBy(pw => pw.Key)
                            .ToDictionary(pw => pw.Key, pw => pw.Sum(pw => pw.Value));
                    }
                }
            }

            return playersWins;
        }

        private Dictionary<int, (int Space, int Score)> InitializePlayers(Dictionary<int, int> playersStartingPositions)
        {
            Dictionary<int, (int Space, int Score)> players = new Dictionary<int, (int Space, int Score)>();
            foreach (KeyValuePair<int, int> player in playersStartingPositions)
            {
                players[player.Key] = (player.Value, 0);
            }

            return players;
        }

        private int WrapBackIfNeeded(int space)
        {
            // Check if wrap back is needed
            if (space > WRAP_AFTER)
            {
                space %= WRAP_AFTER;
                if (space == 0)
                {
                    space = 10;
                }
            }

            return space;
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

        private string StringifyPlayers(Dictionary<int, (int Space, int Score)> players, int playerTurn)
        {
            string playersString = string.Empty;
            foreach (KeyValuePair<int, (int Space, int Score)> player in players)
            {
                playersString += $"{player.Key},{player.Value.Space},{player.Value.Score};";
            }
            playersString += playerTurn;

            return playersString;
        }
    }
}
