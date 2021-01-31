using System.Linq;

namespace App.Tasks.Year2017.Day2
{
    public class Checksum
    {
        public int CalculateSpreadsheetChecksum(int[][] spreadsheetNumbers)
        {
            int checksum = 0;

            foreach (int[] row in spreadsheetNumbers)
            {
                int diff = row.Max() - row.Min();
                checksum += diff;
            }

            return checksum;
        }
    }
}
