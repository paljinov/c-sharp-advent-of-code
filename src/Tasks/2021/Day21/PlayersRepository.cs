using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2021.Day21
{
    public class PlayersRepository
    {
        public Dictionary<int, int> GetPlayersStartingPositions(string input)
        {
            Dictionary<int, int> playersStartingPositions = new Dictionary<int, int>();
            string[] playersStartingPositionsString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Regex playerStartingPositionRegex = new Regex(@"^Player\s(\d+)\sstarting\sposition:\s(\d+)$");

            for (int i = 0; i < playersStartingPositionsString.Length; i++)
            {
                Match playerStartingPositionMatch =
                    playerStartingPositionRegex.Match(playersStartingPositionsString[i]);
                GroupCollection playerStartingPositionGroups = playerStartingPositionMatch.Groups;

                int playerId = int.Parse(playerStartingPositionGroups[1].Value);
                int playerStartingPosition = int.Parse(playerStartingPositionGroups[2].Value);

                playersStartingPositions.Add(playerId, playerStartingPosition);
            }

            return playersStartingPositions;
        }
    }
}
