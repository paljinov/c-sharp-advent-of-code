using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day23
{
    public struct State
    {
        public Dictionary<int, char> Hallway { get; set; }
        public Dictionary<char, List<char>> Rooms { get; set; }

        public State(Dictionary<int, char> hallway, Dictionary<char, List<char>> rooms)
        {
            Hallway = hallway;
            Rooms = rooms;
        }

        public State Clone()
        {
            Dictionary<int, char> hallway = Hallway.ToDictionary(h => h.Key, h => h.Value);

            Dictionary<char, List<char>> rooms = new Dictionary<char, List<char>>();
            foreach (KeyValuePair<char, List<char>> room in Rooms)
            {
                rooms[room.Key] = room.Value.ToList();
            }

            State state = new State(hallway, rooms);

            return state;
        }
    }
}
