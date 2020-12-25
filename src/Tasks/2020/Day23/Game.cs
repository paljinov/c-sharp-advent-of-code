using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2020.Day23
{
    public class Game
    {
        private const int CRAB_PICKS_UP_CUPS = 3;

        private const int MOVES = 100;

        private const int CLOCKWISE_FROM = 1;

        public string GetCupsAfterMovesClockwiseFromOne(LinkedList<int> cups)
        {
            LinkedListNode<int> node = cups.First;
            int min = cups.Min();
            int max = cups.Max();

            for (int m = 0; m < MOVES; m++)
            {
                List<int> pickUp = PickUpCups(node, cups);
                int destination = GetDestination(node, min, max, cups);
                PutPickedUpCupsAfterDestination(destination, pickUp, cups);

                node = GetNextNode(node, cups.First);
            }


            string cupsClockwiseFromOne = GetCupsClockwiseFromOne(cups);

            return cupsClockwiseFromOne;
        }

        private int GetDestination(LinkedListNode<int> node, int min, int max, LinkedList<int> cups)
        {
            // The crab selects a destination cup: the cup with a label equal to the current cup's label minus one
            int destination = node.Value - 1;

            // If this would select one of the cups that was just picked up, the crab will
            // keep subtracting one until it finds a cup that wasn't just picked up
            while (!cups.Contains(destination))
            {
                destination--;
                // If at any point in this process the value goes below the lowest value on any cup's label,
                // it wraps around to the highest value on any cup's label instead
                if (destination < min)
                {
                    destination = max;
                }
            }

            return destination;
        }

        private List<int> PickUpCups(LinkedListNode<int> node, LinkedList<int> cups)
        {
            List<int> pickUp = new List<int>();

            for (int i = 0; i < CRAB_PICKS_UP_CUPS; i++)
            {
                LinkedListNode<int> previousNode = node;

                node = GetNextNode(node, cups.First);

                pickUp.Add(node.Value);

                // Remove cup node
                cups.Remove(node);
                node = previousNode;
            }

            return pickUp;
        }

        private void PutPickedUpCupsAfterDestination(int destination, List<int> pickUp, LinkedList<int> cups)
        {
            LinkedListNode<int> destinationNode = cups.Find(destination);
            for (int i = pickUp.Count - 1; i >= 0; i--)
            {
                cups.AddAfter(destinationNode, new LinkedListNode<int>(pickUp[i]));
            }
        }

        private LinkedListNode<int> GetNextNode(LinkedListNode<int> node, LinkedListNode<int> firstNode)
        {
            if (node.Next != null)
            {
                node = node.Next;
            }
            else
            {
                node = firstNode;
            }

            return node;
        }

        private string GetCupsClockwiseFromOne(LinkedList<int> cups)
        {
            LinkedListNode<int> node = cups.Find(CLOCKWISE_FROM);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < cups.Count - 1; i++)
            {
                node = GetNextNode(node, cups.First);
                sb.Append(node.Value);
            }

            return sb.ToString();
        }
    }
}
