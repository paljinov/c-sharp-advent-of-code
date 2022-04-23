using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day23
{
    public class StateComparer : IEqualityComparer<State>
    {
        public bool Equals(State first, State second)
        {
            foreach (KeyValuePair<int, char> position in first.Hallway)
            {
                if (position.Value != second.Hallway[position.Key])
                {
                    return false;
                }
            }

            foreach (KeyValuePair<char, List<char>> room in first.Rooms)
            {
                if (!room.Value.SequenceEqual(second.Rooms[room.Key]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(State state)
        {
            string hallway = string.Concat(state.Hallway.Values);
            string rooms = string.Concat(state.Rooms.Select(r => string.Concat(r.Value)));

            return hallway.GetHashCode() ^ rooms.GetHashCode();
        }
    }
}
