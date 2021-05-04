using System.Collections.Generic;

namespace App.Tasks.Year2018.Day8
{
    public class NavigationSystem
    {
        public int CalculateSumOfAllMetadataEntries(Queue<int> numbers)
        {
            int sumOfAllMetadataEntries = 0;

            int childNodes = numbers.Dequeue();
            int metadataEntries = numbers.Dequeue();

            for (int i = 0; i < childNodes; i++)
            {
                sumOfAllMetadataEntries += CalculateSumOfAllMetadataEntries(numbers);
            }

            for (int i = 0; i < metadataEntries; i++)
            {
                sumOfAllMetadataEntries += numbers.Dequeue();
            }

            return sumOfAllMetadataEntries;
        }
    }
}
