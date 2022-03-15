using System;

namespace App.Tasks.Year2018.Day15
{
    public class CombatRepository
    {
        public char[,] GetCombatDescription(string input)
        {
            string[] combatString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = combatString.Length;
            int columns = combatString[0].Length;
            char[,] combat = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    combat[i, j] = combatString[i][j];
                }
            }

            return combat;
        }
    }
}
