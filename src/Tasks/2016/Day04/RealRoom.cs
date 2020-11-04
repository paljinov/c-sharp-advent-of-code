using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2016.Day4
{
    class RealRoom
    {
        public bool IsRealRoom(Room room)
        {
            Dictionary<int, string> letterOccurrences = GetDescendingLetterOccurrences(room.Name);

            foreach (char c in room.Checksum)
            {
                KeyValuePair<int, string> mostOccurences = letterOccurrences.First();

                string letters = mostOccurences.Value;
                if (!letters.Contains(c))
                {
                    return false;
                }

                letters = letters.Replace(c.ToString(), string.Empty);
                if (letters == string.Empty)
                {
                    letterOccurrences.Remove(mostOccurences.Key);
                }
                else
                {
                    letterOccurrences[mostOccurences.Key] = letters;
                }
            }

            return true;
        }

        private Dictionary<int, string> GetDescendingLetterOccurrences(string roomName)
        {
            Dictionary<char, int> letters = new Dictionary<char, int>();
            foreach (char letter in roomName)
            {
                if (letter != '-')
                {
                    if (letters.ContainsKey(letter))
                    {
                        letters[letter]++;
                    }
                    else
                    {
                        letters.Add(letter, 1);
                    }
                }
            }

            Dictionary<int, string> letterOccurrences = new Dictionary<int, string>();
            foreach (KeyValuePair<char, int> letter in letters)
            {
                if (letterOccurrences.ContainsKey(letter.Value))
                {
                    letterOccurrences[letter.Value] = letterOccurrences[letter.Value] + letter.Key.ToString();
                }
                else
                {
                    letterOccurrences.Add(letter.Value, letter.Key.ToString());
                }
            }

            letterOccurrences = letterOccurrences.OrderByDescending(l => l.Key).ToDictionary(l => l.Key, l => l.Value);

            return letterOccurrences;
        }
    }
}
