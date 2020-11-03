using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2016.Day4
{
    class RealRoom
    {
        public bool IsRealRoom(Room room)
        {
            Dictionary<char, int> letterOccurrences = GetLetterOccurrences(room.Name);
            List<int> occurences = letterOccurrences.Values.ToList().OrderByDescending(i => i).ToList();

            for (int i = 0; i < room.Checksum.Length; i++)
            {
                char c = room.Checksum[i];
                var lettersWithOccurences = letterOccurrences.Where(x => x.Value == occurences[0]);

                StringBuilder sb = new StringBuilder();
                foreach (var lwo in lettersWithOccurences)
                {
                    sb.Append(lwo.Key);
                }
                string lettersWithOccurencesString = sb.ToString();

                if (!lettersWithOccurencesString.Contains(c))
                {
                    return false;
                }

                occurences.RemoveAt(0);
            }

            return true;
        }

        private Dictionary<char, int> GetLetterOccurrences(string roomName)
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

            return letters;
        }
    }
}
