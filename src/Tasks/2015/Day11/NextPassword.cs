using System;
using System.Linq;

namespace App.Tasks.Year2015.Day11
{
    public class NextPassword
    {
        public string Find(string input)
        {
            char[] newPassword = input.ToCharArray();
            string end = new string('z', newPassword.Length);

            bool newPasswordFound = false;
            // Setting index to last letter
            int i = newPassword.Length - 1;

            // Iterate until new password is found or until end is reached
            while (!newPasswordFound && !newPassword.Equals(end))
            {
                // Until current letter is less than 'z'
                if (newPassword[i] < 'z')
                {
                    newPassword[i]++;
                }
                // If current letter is 'z'
                else
                {
                    // Go left until letter is 'z', going left is possible until first password letter is reached
                    while (i > 0 && newPassword[i - 1] == 'z')
                    {
                        newPassword[i] = 'a';
                        i--;
                    }

                    if (i == 0 && newPassword[i] == 'z')
                    {
                        break;
                    }

                    // Set current letter to 'a', increase left letter, and set iterator to last letter
                    newPassword[i] = 'a';
                    newPassword[i - 1]++;
                    i = newPassword.Length - 1;
                }

                if (!ContainsForbiddenLetters(newPassword))
                {
                    if (IncreasingStraightCondition(newPassword))
                    {
                        if (ContainsTwoLetterPairs(newPassword))
                        {
                            newPasswordFound = true;
                        }
                    }
                }
            }

            return new string(newPassword);
        }

        private bool ContainsForbiddenLetters(char[] password)
        {
            if (password.Contains('i'))
            {
                return true;
            }
            if (password.Contains('o'))
            {
                return true;
            }
            if (password.Contains('l'))
            {
                return true;
            }

            return false;
        }

        private bool IncreasingStraightCondition(char[] password)
        {
            for (int i = 0; i < password.Length - 3; i++)
            {
                if (password[i] + 1 == password[i + 1] && password[i + 1] + 1 == password[i + 2])
                {
                    return true;
                }
            }

            return false;
        }

        private bool ContainsTwoLetterPairs(char[] password)
        {
            int pairsCount = 0;

            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] == password[i + 1])
                {
                    pairsCount++;
                    i++;

                    if (pairsCount == 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
