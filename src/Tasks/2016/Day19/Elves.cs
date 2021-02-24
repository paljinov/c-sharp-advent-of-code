using System;
using System.Collections.Generic;

namespace App.Tasks.Year2016.Day19
{
    public class Elves
    {
        public int FindElfWhichGetsAllThePresentsWhenStealingFromTheElfToTheLeft(int totalElves)
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

        public int FindElfWhichGetsAllThePresentsWhenStealingFromTheElfDirectlyAcrossTheCircle(int totalElves)
        {
            LinkedList<int> elves = new LinkedList<int>();
            for (int elf = 1; elf <= totalElves; elf++)
            {
                elves.AddLast(elf);
            }

            LinkedListNode<int> node = elves.First;

            // Node of elf which is directly across the circle
            LinkedListNode<int> elfDirectlyAcrossTheCircleNode = elves.First;
            for (int elf = 1; elf <= (int)Math.Floor((double)totalElves / 2); elf++)
            {
                elfDirectlyAcrossTheCircleNode = GetNextNode(elfDirectlyAcrossTheCircleNode, elves);
            }

            // While only one elf has all the presents
            while (elves.Count > 1)
            {
                LinkedListNode<int> removeElf = elfDirectlyAcrossTheCircleNode;
                // Get next elf which will be directly across the circle
                elfDirectlyAcrossTheCircleNode = GetNextNode(elfDirectlyAcrossTheCircleNode, elves);
                // Steal all the presents from the elf directly across the circle
                elves.Remove(removeElf);

                // If number of elves is even
                if (elves.Count % 2 == 0)
                {
                    elfDirectlyAcrossTheCircleNode = GetNextNode(elfDirectlyAcrossTheCircleNode, elves);
                }

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
