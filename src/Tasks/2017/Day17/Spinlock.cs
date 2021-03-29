using System.Collections.Generic;

namespace App.Tasks.Year2017.Day17
{
    public class Spinlock
    {
        private const int INITIAL_CIRCULAR_BUFFER_STATE = 0;

        public int CalculateValueAfterTotalRepetitions(int steps, int totalRepetitions, int valueAfter)
        {
            LinkedList<int> circularBuffer = new LinkedList<int>();
            circularBuffer.AddLast(INITIAL_CIRCULAR_BUFFER_STATE);

            int insertValue = 1;

            LinkedListNode<int> node = circularBuffer.First;
            while (insertValue <= totalRepetitions)
            {
                for (int i = 0; i < steps; i++)
                {
                    node = GetNextNode(node, circularBuffer);
                }

                circularBuffer.AddAfter(node, insertValue);
                node = GetNextNode(node, circularBuffer);

                insertValue++;
            }

            int valueAfterTotalRepetitions = circularBuffer.Find(valueAfter).Next.Value;

            return valueAfterTotalRepetitions;
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
