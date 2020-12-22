using System;
using System.Collections.Generic;

namespace App.Tasks.Year2020.Day22
{
    public class PlayersStartingDecksRepository
    {
        public Queue<int> GetPlayerStartingDeck(string input, int player)
        {
            Queue<int> playerStartingDeck = new Queue<int>();

            string[] playersStartingDecksStrings = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries
            );

            string playerStartingDeckString = playersStartingDecksStrings[player - 1];
            string[] playerCards = playerStartingDeckString.Split(Environment.NewLine);

            for (int i = 1; i < playerCards.Length; i++)
            {
                playerStartingDeck.Enqueue(int.Parse(playerCards[i]));
            }

            return playerStartingDeck;
        }
    }
}
