using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day16
{
    public class NotesRepository
    {
        public List<Field> GetFields(string input)
        {
            List<Field> fields = new List<Field>();

            List<List<string>> notes = ParseNotes(input);
            Regex fieldRegex = new Regex(@"^([a-z\s]+):\s(\d+)-(\d+)\sor\s(\d+)-(\d+)$");

            foreach (string fieldString in notes[0])
            {
                Match fieldMatch = fieldRegex.Match(fieldString);
                GroupCollection fieldGroups = fieldMatch.Groups;

                fields.Add(new Field
                {
                    Name = fieldGroups[1].Value,
                    FirstRangeStart = int.Parse(fieldGroups[2].Value),
                    FirstRangeEnd = int.Parse(fieldGroups[3].Value),
                    SecondRangeStart = int.Parse(fieldGroups[4].Value),
                    SecondRangeEnd = int.Parse(fieldGroups[5].Value)
                });
            }

            return fields;
        }

        public List<int> GetTicket(string input)
        {
            List<int> ticket = new List<int>();

            List<List<string>> notes = ParseNotes(input);
            string[] ticketString = notes[1][1].Split(",");

            foreach (string position in ticketString)
            {
                ticket.Add(int.Parse(position));
            }

            return ticket;
        }

        public List<List<int>> GetNearbyTickets(string input)
        {
            List<List<int>> nearbyTickets = new List<List<int>>();

            List<List<string>> notes = ParseNotes(input);

            for (int i = 1; i < notes[2].Count; i++)
            {
                string[] nearbyTicketString = notes[2][i].Split(",");

                List<int> ticket = new List<int>();
                foreach (string position in nearbyTicketString)
                {
                    ticket.Add(int.Parse(position));
                }

                nearbyTickets.Add(ticket);
            }

            return nearbyTickets;
        }

        private List<List<string>> ParseNotes(string input)
        {
            string[] notesString = input.Split(Environment.NewLine);
            List<List<string>> notes = new List<List<string>>();

            List<string> section = new List<string>();
            foreach (string line in notesString)
            {
                if (line == string.Empty)
                {
                    notes.Add(section);
                    section = new List<string>();
                }
                else
                {
                    section.Add(line);
                }
            }
            notes.Add(section);

            return notes;
        }
    }
}
