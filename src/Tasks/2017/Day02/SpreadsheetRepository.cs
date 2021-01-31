using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day2
{
    public class SpreadsheetRepository
    {
        public int[][] GetSpreadsheetNumbers(string input)
        {
            string[] spreadsheetNumbersString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int[][] spreadsheetNumbers = new int[spreadsheetNumbersString.Length][];

            for (int i = 0; i < spreadsheetNumbersString.Length; i++)
            {
                string[] rowNumbersString = Regex.Split(spreadsheetNumbersString[i], @"\s+");
                int[] rowNumbers = new int[rowNumbersString.Length];

                for (int j = 0; j < rowNumbersString.Length; j++)
                {
                    rowNumbers[j] = int.Parse(rowNumbersString[j]);
                }

                spreadsheetNumbers[i] = rowNumbers;
            }

            return spreadsheetNumbers;
        }
    }
}
