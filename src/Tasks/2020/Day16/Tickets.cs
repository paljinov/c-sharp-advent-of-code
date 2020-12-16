using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day16
{
    public class Tickets
    {
        private const string FIELD_STARTS_WITH = "departure";

        public int GetTicketScanningErrorRate(List<Field> fields, List<List<int>> nearbyTickets)
        {
            Dictionary<int, int> invalidNearbyTickets = GetInvalidNearbyTickets(fields, nearbyTickets);

            int ticketScanningErrorRate = invalidNearbyTickets.Sum(x => x.Value);

            return ticketScanningErrorRate;
        }

        public long GetTicketDepartureFieldsProduct(List<Field> fields, List<int> ticket, List<List<int>> nearbyTickets)
        {
            Dictionary<int, int> invalidNearbyTickets = GetInvalidNearbyTickets(fields, nearbyTickets);
            List<List<int>> validNearbyTickets = GetValidNearbyTickets(nearbyTickets, invalidNearbyTickets);

            Dictionary<string, int> ticketFieldsPositions = GetTicketFieldsPositions(fields, validNearbyTickets);

            long product = 1;
            foreach (KeyValuePair<string, int> ticketFieldPosition in ticketFieldsPositions)
            {
                if (ticketFieldPosition.Key.StartsWith(FIELD_STARTS_WITH))
                {
                    product *= ticket[ticketFieldPosition.Value];
                }
            }

            return product;
        }

        private Dictionary<int, int> GetInvalidNearbyTickets(List<Field> fields, List<List<int>> nearbyTickets)
        {
            Dictionary<int, int> invalidNearbyTickets = new Dictionary<int, int>();

            for (int i = 0; i < nearbyTickets.Count; i++)
            {
                List<int> nearbyTicket = nearbyTickets[i];
                foreach (int value in nearbyTicket)
                {
                    bool valid = false;
                    foreach (Field field in fields)
                    {
                        if ((value >= field.FirstRangeStart && value <= field.FirstRangeEnd)
                            || (value >= field.SecondRangeStart && value <= field.SecondRangeEnd))
                        {
                            valid = true;
                            break;
                        }
                    }

                    if (!valid)
                    {
                        invalidNearbyTickets.Add(i, value);
                    }
                }
            }

            return invalidNearbyTickets;
        }

        private List<List<int>> GetValidNearbyTickets(
            List<List<int>> nearbyTickets,
            Dictionary<int, int> invalidNearbyTickets
        )
        {
            List<List<int>> validNearbyTickets = new List<List<int>>();
            for (int i = 0; i < nearbyTickets.Count; i++)
            {
                if (!invalidNearbyTickets.ContainsKey(i))
                {
                    validNearbyTickets.Add(nearbyTickets[i]);
                }
            }

            return validNearbyTickets;
        }

        private Dictionary<string, int> GetTicketFieldsPositions(List<Field> fields, List<List<int>> validNearbyTickets)
        {
            Dictionary<string, int> ticketFieldsPositions = new Dictionary<string, int>();

            int positions = validNearbyTickets[0].Count;

            while (ticketFieldsPositions.Count < fields.Count)
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    Field field = fields[i];
                    // If position is found for this field
                    if (ticketFieldsPositions.ContainsKey(field.Name))
                    {
                        continue;
                    }

                    int position = -1;

                    for (int j = 0; j < positions; j++)
                    {
                        // If field is already found for this position
                        if (ticketFieldsPositions.ContainsValue(j))
                        {
                            continue;
                        }

                        bool possibleAtPosition = true;
                        foreach (List<int> validNearbyTicket in validNearbyTickets)
                        {
                            int value = validNearbyTicket[j];

                            if (!(value >= field.FirstRangeStart && value <= field.FirstRangeEnd)
                                && !(value >= field.SecondRangeStart && value <= field.SecondRangeEnd))
                            {
                                possibleAtPosition = false;
                                break;
                            }
                        }

                        if (possibleAtPosition)
                        {
                            if (position == -1)
                            {
                                position = j;
                            }
                            // If multiple positions are possible we cannot determine anything
                            else
                            {
                                position = -1;
                                break;
                            }
                        }
                    }

                    if (position >= 0)
                    {
                        ticketFieldsPositions.Add(field.Name, position);
                        break;
                    }
                }
            }

            return ticketFieldsPositions;
        }
    }
}
