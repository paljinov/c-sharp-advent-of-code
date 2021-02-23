using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2020.Day23
{
    public class Game
    {
        private const int CRAB_PICKS_UP_CUPS = 3;

        private const int CLOCKWISE_FROM = 1;

        public string GetCupsAfterMovesClockwiseFromOne(LinkedList<int> cups, int moves)
        {
            cups = GetCupsAfterMoves(cups, moves);

            string cupsClockwiseFromOne = GetCupsClockwiseFromOne(cups);

            return cupsClockwiseFromOne;
        }

        public long ProductOfTwoCupsClockwiseFromOne(LinkedList<int> cups, int moves)
        {
            cups = GetCupsAfterMoves(cups, moves);

            LinkedListNode<int> node = cups.Find(CLOCKWISE_FROM);
            LinkedListNode<int> first = GetNextNode(node, cups);
            LinkedListNode<int> second = GetNextNode(first, cups);

            long productOfTwoCupsClockwiseFromOne = (long)first.Value * (long)second.Value;

            return productOfTwoCupsClockwiseFromOne;
        }

        private LinkedList<int> GetCupsAfterMoves(LinkedList<int> cups, int moves)
        {
            LinkedListNode<int> node = cups.First;
            int min = cups.Min();
            int max = cups.Max();

            // Cache dictionary which stores nodes as values where value is key
            Dictionary<int, LinkedListNode<int>> nodesForValues = new Dictionary<int, LinkedListNode<int>>();
            while (node != null)
            {
                nodesForValues[node.Value] = node;
                node = node.Next;
            }

            node = cups.First;

            for (int m = 0; m < moves; m++)
            {
                List<int> pickedUp = PickUpCups(node, cups, nodesForValues);
                int destination = GetDestination(node, min, max, pickedUp);
                PutPickedUpCupsAfterDestination(destination, pickedUp, cups, nodesForValues);

                node = GetNextNode(node, cups);
            }

            return cups;
        }

        private int GetDestination(LinkedListNode<int> node, int min, int max, List<int> pickedUp)
        {
            // The crab selects a destination cup, the cup with a label equal to the current cup's label minus one
            int destination = node.Value - 1;
            // If at any point in this process the value goes below the lowest value on any cup's label,
            // it wraps around to the highest value on any cup's label instead
            if (destination < min)
            {
                destination = max;
            }

            // If this would select one of the cups that was just picked up, the crab will
            // keep subtracting one until it finds a cup that wasn't just picked up
            while (pickedUp.Contains(destination))
            {
                destination--;
                if (destination < min)
                {
                    destination = max;
                }
            }

            return destination;
        }

        private List<int> PickUpCups(
            LinkedListNode<int> node,
            LinkedList<int> cups,
            Dictionary<int, LinkedListNode<int>> nodesForValues
        )
        {
            List<int> pickedUp = new List<int>();

            for (int i = 0; i < CRAB_PICKS_UP_CUPS; i++)
            {
                LinkedListNode<int> previousNode = node;

                node = GetNextNode(node, cups);

                pickedUp.Add(node.Value);

                // Remove cup node
                nodesForValues.Remove(node.Value);
                cups.Remove(node);
                node = previousNode;
            }

            return pickedUp;
        }

        private void PutPickedUpCupsAfterDestination(
            int destination,
            List<int> pickedUp,
            LinkedList<int> cups,
            Dictionary<int, LinkedListNode<int>> nodesForValues
        )
        {
            LinkedListNode<int> destinationNode = nodesForValues[destination];
            for (int i = pickedUp.Count - 1; i >= 0; i--)
            {
                LinkedListNode<int> pickedUpCup = new LinkedListNode<int>(pickedUp[i]);
                cups.AddAfter(destinationNode, pickedUpCup);
                nodesForValues[pickedUp[i]] = pickedUpCup;
            }
        }

        private LinkedListNode<int> GetNextNode(LinkedListNode<int> node, LinkedList<int> cups)
        {
            if (node.Next != null)
            {
                node = node.Next;
            }
            else
            {
                node = cups.First;
            }

            return node;
        }

        private string GetCupsClockwiseFromOne(LinkedList<int> cups)
        {
            LinkedListNode<int> node = cups.Find(CLOCKWISE_FROM);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < cups.Count - 1; i++)
            {
                node = GetNextNode(node, cups);
                sb.Append(node.Value);
            }

            return sb.ToString();
        }
    }
}
