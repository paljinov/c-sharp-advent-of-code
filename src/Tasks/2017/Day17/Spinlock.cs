using System.Collections.Generic;

namespace App.Tasks.Year2017.Day17
{
    public class Spinlock
    {
        private const int INITIAL_CIRCULAR_BUFFER_STATE = 0;

        public int CalculateValueAfterLastInserted(int steps, int lastInserted)
        {
            LinkedList<int> circularBuffer = new LinkedList<int>();
            circularBuffer.AddLast(INITIAL_CIRCULAR_BUFFER_STATE);

            LinkedListNode<int> node = circularBuffer.First;

            for (int insertValue = 1; insertValue <= lastInserted; insertValue++)
            {
                for (int i = 0; i < steps; i++)
                {
                    node = GetNextNode(node, circularBuffer);
                }

                circularBuffer.AddAfter(node, insertValue);
                node = GetNextNode(node, circularBuffer);
            }

            int valueAfterLastInserted = circularBuffer.Find(lastInserted).Next.Value;

            return valueAfterLastInserted;
        }

        public int CalculateValueAfterZeroWhenLastInserted(int steps, int lastInserted)
        {
            int valueAfterZero = 0;

            int currentPosition = 0;
            for (int insertValue = 1; insertValue <= lastInserted; insertValue++)
            {
                currentPosition = (currentPosition + steps) % insertValue + 1;
                // If inserting after zero
                if (currentPosition == 1)
                {
                    valueAfterZero = insertValue;
                }
            }

            return valueAfterZero;
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
