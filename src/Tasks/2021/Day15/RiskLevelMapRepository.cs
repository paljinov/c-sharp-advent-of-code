using System;

namespace App.Tasks.Year2021.Day15
{
    public class RiskLevelMapRepository
    {
        public int[,] GetRiskLevelMap(string input)
        {
            string[] riskLevelMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = riskLevelMapString.Length;
            int columns = riskLevelMapString[0].Length;
            int[,] riskLevelMap = new int[rows, columns];

            for (int i = 0; i < riskLevelMapString.Length; i++)
            {
                for (int j = 0; j < riskLevelMapString[i].Length; j++)
                {
                    riskLevelMap[i, j] = (int)char.GetNumericValue(riskLevelMapString[i][j]);
                }
            }

            return riskLevelMap;
        }
    }
}
