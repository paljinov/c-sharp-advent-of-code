using System;
using System.Collections.Generic;

namespace App.Tasks.Year2020.Day13
{
    public class NotesRepository
    {
        public int GetEarliestDepartureTimestamp(string input)
        {
            string[] notesString = input.Split(Environment.NewLine);

            int earliestDepartureTimestamp = int.Parse(notesString[0]);

            return earliestDepartureTimestamp;
        }

        public List<int> GetBusIds(string input)
        {
            List<int> busIds = new List<int>();

            string[] notesString = input.Split(Environment.NewLine);
            string[] busString = notesString[1].Split(',');

            for (int i = 0; i < busString.Length; i++)
            {
                string busId = busString[i];

                if (busId == "x")
                {
                    busIds.Add(0);
                }
                else
                {
                    busIds.Add(int.Parse(busId));
                }
            }

            return busIds;
        }
    }
}
