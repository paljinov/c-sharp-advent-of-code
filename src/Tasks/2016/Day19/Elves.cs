using System.Collections.Generic;

namespace App.Tasks.Year2016.Day19
{
    public class Elves
    {
        public int FindElfWhichGetsAllThePresents(int totalElves)
        {
            LinkedList<int> elves = new LinkedList<int>();
            for (int elf = 1; elf <= totalElves; elf++)
            {
                elves.AddLast(elf);
            }

            LinkedListNode<int> node = elves.First;

            // While only one elf has all the presents
            while (elves.Count > 1)
            {
                LinkedListNode<int> nextNode = GetNextNode(node, elves);
                // Steal all the presents from the elf on the left
                elves.Remove(nextNode);

                node = GetNextNode(node, elves);
            }

            int elfWhichGetsAllThePresents = elves.First.Value;

            return elfWhichGetsAllThePresents;
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
