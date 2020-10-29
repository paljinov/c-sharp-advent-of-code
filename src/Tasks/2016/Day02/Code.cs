using System.Text;

namespace App.Tasks.Year2016.Day2
{
    class Code
    {
        public const char NotExistingKey = 'X';

        private const char StartingKey = '5';

        public string FindCode(char[,] keypad, string[] instructions)
        {
            StringBuilder sb = new StringBuilder();

            // Starting digit
            char key = StartingKey;
            foreach (string digitInstructions in instructions)
            {
                key = FindKeyForInstructions(keypad, digitInstructions, key);
                sb.Append(key);
            }

            string code = sb.ToString();

            return code;
        }

        private char FindKeyForInstructions(char[,] keypad, string digitInstructions, int startingKey)
        {
            (int i, int j) = FindKeyPosition(keypad, startingKey);

            foreach (char instruction in digitInstructions)
            {
                switch (instruction)
                {
                    // moves up
                    case 'U':
                        if (i - 1 >= 0 && keypad[i - 1, j] != NotExistingKey)
                        {
                            i--;
                        }
                        break;
                    // moves down
                    case 'D':
                        if (i + 1 < keypad.GetLength(0) && keypad[i + 1, j] != NotExistingKey)
                        {
                            i++;
                        }
                        break;
                    // moves left
                    case 'L':
                        if (j - 1 >= 0 && keypad[i, j - 1] != NotExistingKey)
                        {
                            j--;
                        }
                        break;
                    // moves right
                    case 'R':
                        if (j + 1 < keypad.GetLength(1) && keypad[i, j + 1] != NotExistingKey)
                        {
                            j++;
                        }
                        break;
                }
            }

            char key = keypad[i, j];

            return key;
        }

        private (int, int) FindKeyPosition(char[,] keypad, int key)
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
