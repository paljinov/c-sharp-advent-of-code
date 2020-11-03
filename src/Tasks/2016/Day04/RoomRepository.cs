using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day4
{
    class RoomRepository
    {
        public List<Room> GetPossibleRooms(string input)
        {
            List<Room> rooms = new List<Room>();

            Regex roomRegex = new Regex(@"([a-z\-]+)-(\d+)\[([a-z]{5})\]");

            string[] roomsString = input.Split(Environment.NewLine);
            foreach (string roomString in roomsString)
            {
                Match match = roomRegex.Match(roomString);
                GroupCollection groups = match.Groups;

                rooms.Add(new Room
                {
                    Name = groups[1].Value,
                    SectorId = int.Parse(groups[2].Value),
                    Checksum = groups[3].Value
                });
            }

            return rooms;
        }
    }
}
