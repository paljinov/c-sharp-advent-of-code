using System;
using System.Linq;

namespace App.Tasks.Year2017.Day2
{
    public class Checksum
    {
        public int CalculateMaxMinSpreadsheetChecksum(int[][] spreadsheetNumbers)
        {
            int checksum = 0;

            foreach (int[] row in spreadsheetNumbers)
            {
                checksum += row.Max() - row.Min();
            }

            return checksum;
        }

        public int CalculateWholeNumberDivisionSpreadsheetChecksum(int[][] spreadsheetNumbers)
        {
            int checksum = 0;

            foreach (int[] row in spreadsheetNumbers)
            {
                Array.Sort<int>(row, new Comparison<int>((a, b) => b.CompareTo(a)));
                int quotient = 0;

                for (int i = 0; i < row.Length; i++)
                {
                    for (int j = i + 1; j < row.Length; j++)
                    {
                        if (row[i] % row[j] == 0)
                        {
                            quotient = row[i] / row[j];
                            break;
                        }
                    }

                    if (quotient > 0)
                    {
                        break;
                    }
                }

                checksum += quotient;
            }

            return checksum;
        }
    }
}
