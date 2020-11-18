using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2016.Day4
{
    class RealRoom
    {
        public List<Room> FilterRealRooms(List<Room> possibleRooms)
        {
            List<Room> realRooms = new List<Room>();

            foreach (Room possibleRoom in possibleRooms)
            {
                if (IsRealRoom(possibleRoom))
                {
                    realRooms.Add(possibleRoom);
                }
            }

            return realRooms;
        }

        private bool IsRealRoom(Room room)
        {
            Dictionary<int, string> letterOccurrences = GetDescendingLetterOccurrences(room.Name);

            foreach (char c in room.Checksum)
            {
                // Get letters with most occurences
                KeyValuePair<int, string> mostOccurences = letterOccurrences.First();

                StringBuilder letters = new StringBuilder(mostOccurences.Value);
                // If checksum letter is not among those with most occurences it is not the real room
                if (!letters.ToString().Contains(c))
                {
                    return false;
                }

                // Remove checked checksum from letters with most occurences
                letters = letters.Replace(c.ToString(), string.Empty);
                // If there aren't any letters left with this number of occurences
                if (letters.Length == 0)
                {
                    letterOccurrences.Remove(mostOccurences.Key);
                }
                // If there are still letters with this number of occurences
                else
                {
                    letterOccurrences[mostOccurences.Key] = letters.ToString();
                }
            }

            return true;
        }

        private Dictionary<int, string> GetDescendingLetterOccurrences(string roomName)
        {
            // Letter occurrences
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

            Dictionary<int, StringBuilder> letterOccurrencesSb = new Dictionary<int, StringBuilder>();
            // Put letters with same number of occurrences under same key (number of occurrences)
            foreach (KeyValuePair<char, int> letter in letters)
            {
                if (letterOccurrencesSb.ContainsKey(letter.Value))
                {
                    letterOccurrencesSb[letter.Value] = letterOccurrencesSb[letter.Value].Append(letter.Key);
                }
                else
                {
                    letterOccurrencesSb.Add(letter.Value, new StringBuilder(letter.Key.ToString()));
                }
            }

            // Sort descending by number of occurrences
            Dictionary<int, string> letterOccurrences =
                letterOccurrencesSb.OrderByDescending(l => l.Key).ToDictionary(l => l.Key, l => l.Value.ToString());

            return letterOccurrences;
        }
    }
}
