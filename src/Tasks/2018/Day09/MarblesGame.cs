using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day9
{
    public class MarblesGame
    {
        public long CalculateWinningElfScore(int players, int lastMarble)
        {
            long[] playersScores = new long[players];

            LinkedList<int> marblesCircle = new LinkedList<int>();
            marblesCircle.AddLast(0);
            LinkedListNode<int> currentMarbleNode = marblesCircle.First;

            for (int marble = 1; marble <= lastMarble; marble++)
            {
                if (marble % 23 != 0)
                {
                    currentMarbleNode = GetNextNode(currentMarbleNode, marblesCircle);
                    currentMarbleNode = marblesCircle.AddAfter(currentMarbleNode, marble);
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                    {
                        currentMarbleNode = GetPreviousNode(currentMarbleNode, marblesCircle);
                    }

                    int player = marble % players;
                    playersScores[player] += marble + currentMarbleNode.Value;

                    currentMarbleNode = GetNextNode(currentMarbleNode, marblesCircle);
                    marblesCircle.Remove(GetPreviousNode(currentMarbleNode, marblesCircle));
                }
            }

            return playersScores.Max();
        }

        private LinkedListNode<int> GetPreviousNode(LinkedListNode<int> node, LinkedList<int> linkedList)
        {
            if (node.Previous != null)
            {
                node = node.Previous;
            }
            else
            {
                node = linkedList.Last;
            }

            return node;
        }

        private LinkedListNode<int> GetNextNode(LinkedListNode<int> node, LinkedList<int> linkedList)
        {
            if (node.Next != null)
            {
                node = node.Next;
            }
            else
            {
                node = linkedList.First;
            }

            return node;
        }
    }
}
