using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day13
{
    public class SittingArrangements
    {
        /// <summary>
        /// Get sitting arrangements.
        /// </summary>
        /// <param name="sittingHappiness"></param>
        /// <param name="attendees"></param>
        /// <returns>
        /// [
        ///     1st arrangement => [
        ///         Alice->Bob => 54,
        ///         Alice->Carol => -79,
        ///         ...
        ///     ],
        ///     ...
        /// ]
        /// </returns>
        public List<Dictionary<string, int>> GetSittingArrangements(
            Dictionary<string, int> sittingHappiness,
            List<string> attendees
        )
        {
            List<Dictionary<string, int>> sittingArrangements = new List<Dictionary<string, int>>();
            List<List<string>> sittingArrangementsPermuations = new List<List<string>>();
            GetSittingArrangementsPermuations(attendees, 0, attendees.Count - 1, sittingArrangementsPermuations);

            foreach (List<string> sittingArrangementsPermuation in sittingArrangementsPermuations)
            {
                Dictionary<string, int> sittingArrangement = new Dictionary<string, int>();

                for (int i = 0; i < sittingArrangementsPermuation.Count; i++)
                {
                    int leftNeighborIndex = i == 0 ? sittingArrangementsPermuation.Count - 1 : i - 1;
                    int rightNeighborIndex = i == sittingArrangementsPermuation.Count - 1 ? 0 : i + 1;

                    string attendee = sittingArrangementsPermuation.ElementAt(i);
                    string leftNeighbor = sittingArrangementsPermuation.ElementAt(leftNeighborIndex);
                    string rightNeighbor = sittingArrangementsPermuation.ElementAt(rightNeighborIndex);

                    string[] neighbors = new string[] {
                        $"{attendee}->{leftNeighbor}",
                        $"{leftNeighbor}->{attendee}",
                        $"{attendee}->{rightNeighbor}",
                        $"{rightNeighbor}->{attendee}"
                    };

                    foreach (string sittingNextTo in neighbors)
                    {
                        sittingArrangement.TryAdd(sittingNextTo, sittingHappiness[sittingNextTo]);
                    }
                }

                sittingArrangements.Add(sittingArrangement);
            }

            return sittingArrangements;
        }

        /// <summary>
        /// Calculate optimal seating arrangement - maximum total change in happiness.
        /// </summary>
        /// <param name="sittingArrangements"></param>
        /// <returns></returns>
        public int CalculateOptimalSeatingArrangement(List<Dictionary<string, int>> sittingArrangements)
        {
            int optimalTotalChangeInHappiness = 0;

            foreach (Dictionary<string, int> sittingArrangement in sittingArrangements)
            {
                int happinessChange = 0;
                foreach (KeyValuePair<string, int> neighbor in sittingArrangement)
                {
                    happinessChange += neighbor.Value;
                }

                optimalTotalChangeInHappiness = Math.Max(optimalTotalChangeInHappiness, happinessChange);
            }

            return optimalTotalChangeInHappiness;
        }

        /// <summary>
        /// Get all sitting arrangements permuations around the table.
        /// </summary>
        /// <param name="attendees"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="sittingArrangementsPermuations"></param>
        private void GetSittingArrangementsPermuations(
            List<string> attendees,
            int start,
            int end,
            List<List<string>> sittingArrangementsPermuations
        )
        {
            if (start == end)
            {
                sittingArrangementsPermuations.Add(new List<string>(attendees));
            }
            else
            {
                for (int i = start; i < end; i++)
                {
                    SwapAttendees(attendees, start, i);
                    GetSittingArrangementsPermuations(attendees, start + 1, end, sittingArrangementsPermuations);

                    // Reset attendees order for the next iteration
                    SwapAttendees(attendees, start, i);
                }
            }
        }

        /// <summary>
        /// Swap two attendees in a list.
        /// </summary>
        /// <param name="attendees"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void SwapAttendees(List<string> attendees, int i, int j)
        {
            string tempAttendee = attendees[i];
            attendees[i] = attendees[j];
            attendees[j] = tempAttendee;
        }
    }
}
