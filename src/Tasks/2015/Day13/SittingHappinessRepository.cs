using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day13
{
    public class SittingHappinessRepository
    {
        /// <summary>
        /// Parse input string to sitting happiness.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        /// [
        ///     Alice->Bob => 54,
        ///     Alice->Carol => - 79,
        ///     ...
        /// ]
        /// </returns>
        public Dictionary<string, int> Parse(string input)
        {
            Dictionary<string, int> sittingHappiness = new Dictionary<string, int>();

            Regex sittingHappinessRegex = new Regex(@"^(\w+).+?(\w+)\s(\d+).+?(\w+)\.$");

            string[] sittingHappinessStrings = input.Split(Environment.NewLine);
            foreach (string neighborsString in sittingHappinessStrings)
            {
                Match sittingHappinessMatch = sittingHappinessRegex.Match(neighborsString);
                GroupCollection groups = sittingHappinessMatch.Groups;

                string neighbors = $"{groups[1].Value}->{groups[4].Value}";
                int happinessUnits = int.Parse(groups[3].Value);
                if (groups[2].Value == "lose")
                {
                    happinessUnits *= -1;
                }

                sittingHappiness.Add(neighbors, happinessUnits);
            }

            return sittingHappiness;
        }

        /// <summary>
        /// Get dinner attendees.
        /// </summary>
        /// <param name="sittingHappiness"></param>
        /// <returns></returns>
        public List<string> GetDinnerAttendees(Dictionary<string, int> sittingHappiness)
        {
            List<string> attendees = new List<string>();
            foreach (KeyValuePair<string, int> attendeeSittingHappiness in sittingHappiness)
            {
                string[] attendeesNames = attendeeSittingHappiness.Key.Split("->");
                foreach (string attendeeName in attendeesNames)
                {
                    if (!attendees.Contains(attendeeName))
                    {
                        attendees.Add(attendeeName);
                    }
                }
            }

            return attendees;
        }
    }
}
