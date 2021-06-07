using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day22
{
    public class CardGame
    {
        public int GetWinningPlayerScore(Queue<int> player1Deck, Queue<int> player2Deck)
        {
            while (player1Deck.Count > 0 && player2Deck.Count > 0)
            {
                PlayCardGameRound(player1Deck, player2Deck);
            }

            int winningPlayerScore;
            if (player1Deck.Count > 0)
            {
                winningPlayerScore = CalculateWinningPlayerScore(player1Deck);
            }
            else
            {
                winningPlayerScore = CalculateWinningPlayerScore(player2Deck);
            }

            return winningPlayerScore;
        }

        public int GetRecursiveCombatWinningPlayerScore(Queue<int> player1Deck, Queue<int> player2Deck)
        {
            (_, Queue<int> winnerDeck) = PlayRecursiveCardGame(player1Deck, player2Deck);
            int winningPlayerScore = CalculateWinningPlayerScore(winnerDeck);

            return winningPlayerScore;
        }

        private void PlayCardGameRound(Queue<int> player1Deck, Queue<int> player2Deck)
        {
            int player1TopCard = player1Deck.Dequeue();
            int player2TopCard = player2Deck.Dequeue();

            // If player 1 has higher card
            if (player1TopCard > player2TopCard)
            {
                player1Deck.Enqueue(player1TopCard);
                player1Deck.Enqueue(player2TopCard);
            }
            // If player 2 has higher card
            else
            {
                player2Deck.Enqueue(player2TopCard);
                player2Deck.Enqueue(player1TopCard);
            }
        }

        private (int, Queue<int>) PlayRecursiveCardGame(Queue<int> player1Deck, Queue<int> player2Deck)
        {
            List<Queue<int>> player1Decks = new List<Queue<int>>();
            List<Queue<int>> player2Decks = new List<Queue<int>>();

            while (player1Deck.Count > 0 && player2Deck.Count > 0)
            {
                // Before either player deals a card, if there was a previous
                // round in this game that had exactly the same cards in the
                // same order in the same players' decks, the game instantly
                // ends in a win for player 1
                foreach (Queue<int> player1OldDeck in player1Decks)
                {
                    if (player1OldDeck.Count == player1Deck.Count && player1OldDeck.SequenceEqual(player1Deck))
                    {
                        return (1, player1Deck);
                    }
                }
                foreach (Queue<int> player2OldDeck in player2Decks)
                {
                    if (player2OldDeck.Count == player2Deck.Count && player2OldDeck.SequenceEqual(player2Deck))
                    {
                        return (1, player1Deck);
                    }
                }

                player1Decks.Add(new Queue<int>(player1Deck));
                player2Decks.Add(new Queue<int>(player2Deck));

                PlayRecursiveCardGameRound(player1Deck, player2Deck);
            }

            if (player1Deck.Count > 0)
            {
                return (1, player1Deck);
            }
            else
            {
                return (2, player2Deck);
            }
        }

        private void PlayRecursiveCardGameRound(Queue<int> player1Deck, Queue<int> player2Deck)
        {
            // If both players have at least as many cards remaining in their
            // deck as the value of the card they just drew, the winner of the
            // round is determined by playing a new game of Recursive Combat
            if (player1Deck.Count - 1 >= player1Deck.Peek() && player2Deck.Count - 1 >= player2Deck.Peek())
            {
                int player1TopCard = player1Deck.Dequeue();
                int player2TopCard = player2Deck.Dequeue();

                // Play sub-game
                (int winner, _) = PlayRecursiveCardGame(
                    new Queue<int>(player1Deck.Take(player1TopCard)),
                    new Queue<int>(player2Deck.Take(player2TopCard))
                );

                if (winner == 1)
                {
                    player1Deck.Enqueue(player1TopCard);
                    player1Deck.Enqueue(player2TopCard);
                }
                else
                {
                    player2Deck.Enqueue(player2TopCard);
                    player2Deck.Enqueue(player1TopCard);
                }
            }
            // Play normal round
            else
            {
                PlayCardGameRound(player1Deck, player2Deck);
            }
        }

        private int CalculateWinningPlayerScore(Queue<int> playerDeck)
        {
            int winningPlayerScore = 0;
            for (int i = playerDeck.Count; i > 0; i--)
            {
                winningPlayerScore += i * playerDeck.Dequeue();
            }

            return winningPlayerScore;
        }
    }
}
