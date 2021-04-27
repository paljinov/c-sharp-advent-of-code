using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day4
{
    public class GuardsRecordsRepository
    {
        public Dictionary<DateTime, Record> GetGuardsRecords(string input)
        {
            Dictionary<DateTime, Record> guardsRecords = new Dictionary<DateTime, Record>();

            Regex guardsRecordRegex = new Regex(@"^\[(\d{4}-\d{2}-\d{2}\s\d{2}:\d{2})\]\s(.+)$");
            Regex guardBeginsShiftRegex = new Regex(@"^Guard\s#(\d+)\sbegins\sshift$");

            string[] guardsRecordsArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string guardsRecordString in guardsRecordsArray)
            {
                Match guardsRecordMatch = guardsRecordRegex.Match(guardsRecordString);
                GroupCollection guardsRecordGroups = guardsRecordMatch.Groups;

                DateTime timestamp = DateTime.Parse(guardsRecordGroups[1].Value);
                Action action;
                int? guardId = null;

                switch (guardsRecordGroups[2].Value)
                {
                    case "falls asleep":
                        action = Action.FallsAsleep;
                        break;
                    case "wakes up":
                        action = Action.WakesUp;
                        break;
                    default:
                        action = Action.BeginsShift;

                        Match guardBeginsShiftMatch = guardBeginsShiftRegex.Match(guardsRecordGroups[2].Value);
                        guardId = int.Parse(guardBeginsShiftMatch.Groups[1].Value);
                        break;
                }

                Record record = new Record
                {
                    GuardId = guardId,
                    Action = action
                };

                guardsRecords.Add(timestamp, record);
            }

            return guardsRecords;
        }
    }
}
