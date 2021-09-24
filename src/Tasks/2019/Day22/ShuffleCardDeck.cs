using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day22
{
    public class ShuffleCardDeck
    {
        private readonly int totalCards = 10007;

        public int CalculateCardPositionAfterShufflingDeck(
            IShuffleTechnique[] shuffleTechniques,
            int wantedCard
        )
        {
            Dictionary<int, int> cards = InitializeCards();

            foreach (IShuffleTechnique shuffleTechnique in shuffleTechniques)
            {
                if (shuffleTechnique is DealIntoNewStack)
                {
                    cards = DealIntoNewStack(cards);
                }
                else if (shuffleTechnique is CutCards cutCards)
                {
                    cards = CutCards(cards, cutCards);
                }
                else
                {
                    cards = DealWithIncrement(cards, (DealWithIncrement)shuffleTechnique);
                }
            }

            int wantedCardPosition = cards.FirstOrDefault(c => c.Value == wantedCard).Key;

            return wantedCardPosition;
        }

        private Dictionary<int, int> InitializeCards()
        {
            Dictionary<int, int> cards = new Dictionary<int, int>();

            for (int i = 0; i < totalCards; i++)
            {
                cards[i] = i;
            }

            return cards;
        }

        private Dictionary<int, int> DealIntoNewStack(Dictionary<int, int> cards)
        {
            Dictionary<int, int> newStack = new Dictionary<int, int>();

            for (int i = 0; i < cards.Count; i++)
            {
                newStack[i] = cards[cards.Count - 1 - i];
            }

            return newStack;
        }

        private Dictionary<int, int> CutCards(Dictionary<int, int> cards, CutCards cutCards)
        {
            Dictionary<int, int> newStack = new Dictionary<int, int>();
            List<int> newStackList;

            if (cutCards.Cut > 0)
            {
                newStackList = cards.Values.TakeLast(cards.Count - cutCards.Cut)
                    .Concat(cards.Values.Take(cutCards.Cut)).ToList();
            }
            else
            {
                newStackList = cards.Values.TakeLast(Math.Abs(cutCards.Cut))
                    .Concat(cards.Values.Take(cards.Count - Math.Abs(cutCards.Cut))).ToList();
            }

            for (int i = 0; i < newStackList.Count; i++)
            {
                newStack[i] = newStackList[i];
            }

            return newStack;
        }

        private Dictionary<int, int> DealWithIncrement(Dictionary<int, int> cards, DealWithIncrement dealWithIncrement)
        {
            Dictionary<int, int> newStack = new Dictionary<int, int>();

            int newPosition = 0;
            int oldPosition = 0;
            while (oldPosition < cards.Count)
            {
                newStack[newPosition] = cards[oldPosition];

                oldPosition++;

                newPosition += dealWithIncrement.Increment;
                if (newPosition >= cards.Count)
                {
                    newPosition -= cards.Count;
                }
            }

            newStack = newStack.OrderBy(c => c.Key).ToDictionary(c => c.Key, c => c.Value);

            return newStack;
        }
    }
}
