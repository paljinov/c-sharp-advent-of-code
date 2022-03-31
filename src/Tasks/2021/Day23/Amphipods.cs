using System.Collections.Generic;

namespace App.Tasks.Year2021.Day23
{
    public class Amphipods
    {

        private const int HALLWAY_HORIZONTAL_POSITION = 1;

        private readonly Dictionary<char, int> amphipodsEnergy = new Dictionary<char, int>()
        {
            { 'A', 1},
            { 'B', 10},
            { 'C', 100},
            { 'D', 1000}
        };

        private readonly Dictionary<char, int> roomsVerticalPositions = new Dictionary<char, int>()
        {
            { 'A', 3},
            { 'B', 5},
            { 'C', 7},
            { 'D', 9}
        };

        public int CalculateLeastEnergyRequiredToOrganizeTheAmphipods(char[,] amphipodsBurrow)
        {
            Dictionary<int, char> hallway = GetHallway(amphipodsBurrow);
            Dictionary<char, Queue<char>> roomsAmphipods = GetRoomsAmphipods(amphipodsBurrow);

            int leastEnergyRequired = DoCalculateLeastEnergyRequiredToOrganize(hallway, roomsAmphipods);

            return leastEnergyRequired;
        }

        public int CalculateLeastEnergyRequiredToOrganizeTheAmphipodsForFullDiagram(char[,] amphipodsBurrow)
        {
            Dictionary<int, char> hallway = GetHallway(amphipodsBurrow);
            Dictionary<char, Queue<char>> roomsAmphipods = GetRoomsAmphipods(amphipodsBurrow);

            int leastEnergyRequired = DoCalculateLeastEnergyRequiredToOrganize(hallway, roomsAmphipods);

            return leastEnergyRequired;
        }

        private int DoCalculateLeastEnergyRequiredToOrganize(
            Dictionary<int, char> hallway,
            Dictionary<char, Queue<char>> roomsAmphipods
        )
        {
            int leastEnergyRequired = int.MaxValue;

            return leastEnergyRequired;
        }

        private Dictionary<int, char> GetHallway(char[,] amphipodsBurrow)
        {
            Dictionary<int, char> hallway = new Dictionary<int, char>();

            for (int j = 1; j < amphipodsBurrow.GetLength(1) - 1; j++)
            {
                if (!roomsVerticalPositions.ContainsValue(j))
                {
                    hallway[j] = amphipodsBurrow[HALLWAY_HORIZONTAL_POSITION, j];
                }
            }

            return hallway;
        }

        private Dictionary<char, Queue<char>> GetRoomsAmphipods(char[,] amphipodsBurrow)
        {
            Dictionary<char, Queue<char>> roomsAmphipods = new Dictionary<char, Queue<char>>();

            foreach (KeyValuePair<char, int> roomVerticalPosition in roomsVerticalPositions)
            {
                Queue<char> roomAmphipods = new Queue<char>();

                for (int i = HALLWAY_HORIZONTAL_POSITION + 1; i < amphipodsBurrow.GetLength(0) - 1; i++)
                {
                    roomAmphipods.Enqueue(amphipodsBurrow[i, roomVerticalPosition.Value]);
                }

                roomsAmphipods[roomVerticalPosition.Key] = roomAmphipods;
            }

            return roomsAmphipods;
        }
    }
}
