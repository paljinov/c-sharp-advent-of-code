using System.Collections.Generic;

namespace App.Tasks.Year2018.Day8
{
    public class NavigationSystem
    {
        public (int, int) CalculateSumOfAllMetadataEntriesAndRootNodeValue(Queue<int> numbers)
        {
            int sumOfAllMetadataEntries = 0;
            int rootNodeValue = 0;

            int childNodes = numbers.Dequeue();
            int metadataEntries = numbers.Dequeue();

            // Values of all child nodes, a metadata entry of 1 refers to the first child node, 2 to the second...
            Dictionary<int, int> childNodesValues = new Dictionary<int, int>();

            for (int i = 0; i < childNodes; i++)
            {
                (int sumOfChildNodesMetadataEntries, int childNodeValue) =
                    CalculateSumOfAllMetadataEntriesAndRootNodeValue(numbers);

                sumOfAllMetadataEntries += sumOfChildNodesMetadataEntries;
                childNodesValues.Add(i + 1, childNodeValue);
            }

            for (int i = 0; i < metadataEntries; i++)
            {
                int metadataEntry = numbers.Dequeue();

                sumOfAllMetadataEntries += metadataEntry;

                // If a node has no child nodes, its value is the sum of its metadata entries
                if (childNodes == 0)
                {
                    rootNodeValue += metadataEntry;
                }
                // If a node does have child nodes, the metadata entries become indexes which refer to those child nodes
                else if (childNodesValues.ContainsKey(metadataEntry))
                {
                    rootNodeValue += childNodesValues[metadataEntry];
                }
            }

            return (sumOfAllMetadataEntries, rootNodeValue);
        }
    }
}
