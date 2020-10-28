using System;
using System.Text;

namespace App.Tasks.Year2016.Day2
{
    class Code
    {
        private readonly int[,] keypad = new int[,] {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        public int Find(string[] instructions)
        {
            StringBuilder sb = new StringBuilder();

            // Starting digit
            int digit = 5;
            foreach (string digitInstructions in instructions)
            {
                digit = FindDigitForInstructions(digitInstructions, digit);
                sb.Append(digit);
            }

            int code = int.Parse(sb.ToString());

            return code;
        }

        private int FindDigitForInstructions(string digitInstructions, int startingKey)
        {
            (int i, int j) = FindKeypadKeyPosition(startingKey);

            foreach (char instruction in digitInstructions)
            {
                switch (instruction)
                {
                    // moves up
                    case 'U':
                        i = i - 1 > 0 ? i - 1 : 0;
                        break;
                    // moves down
                    case 'D':
                        i = i + 1 < keypad.GetLength(0) ? i + 1 : keypad.GetLength(0) - 1;
                        break;
                    // moves left
                    case 'L':
                        j = j - 1 > 0 ? j - 1 : 0;
                        break;
                    // moves right
                    case 'R':
                        j = j + 1 < keypad.GetLength(1) ? j + 1 : keypad.GetLength(1) - 1;
                        break;
                }
            }

            int digit = keypad[i, j];

            return digit;
        }

        private (int, int) FindKeypadKeyPosition(int key)
        {
            for (int i = 0; i < keypad.GetLength(0); i++)
            {
                for (int j = 0; j < keypad.GetLength(1); j++)
                {
                    if (keypad[i, j] == key)
                    {
                        return (i, j);
                    }
                }
            }

            return (0, 0);
        }
    }
}
