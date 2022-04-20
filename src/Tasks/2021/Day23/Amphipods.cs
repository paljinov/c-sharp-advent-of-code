using System;
using System.Collections.Generic;

namespace App.Tasks.Year2021.Day23
{
    public class Amphipods
    {

        private const char OPEN_SPACE = '.';

        private const int HALLWAY_HORIZONTAL_POSITION = 1;

        private static readonly Dictionary<char, int> amphipodsEnergy = new Dictionary<char, int>()
        {
            { (char) AmphipodType.Amber, 1 },
            { (char) AmphipodType.Bronze, 10 },
            { (char) AmphipodType.Copper, 100 },
            { (char) AmphipodType.Desert, 1000 }
        };

        private static readonly Dictionary<char, int> roomsVerticalPositions = new Dictionary<char, int>()
        {
            { (char) AmphipodType.Amber, 3 },
            { (char) AmphipodType.Bronze, 5 },
            { (char) AmphipodType.Copper, 7 },
            { (char) AmphipodType.Desert, 9 }
        };

        public int CalculateLeastEnergyRequiredToOrganizeTheAmphipods(char[,] amphipodsBurrow)
        {
            int leastEnergyRequired = int.MaxValue;

            Dictionary<int, char> hallway = GetHallway(amphipodsBurrow);
            Dictionary<char, Queue<char>> roomsAmphipods = GetRoomsAmphipods(amphipodsBurrow);

            DoCalculateLeastEnergyRequiredToOrganize(hallway, roomsAmphipods, 0, leastEnergyRequired);

            return leastEnergyRequired;
        }

        public int CalculateLeastEnergyRequiredToOrganizeTheAmphipodsForFullDiagram(char[,] amphipodsBurrow)
        {
            int leastEnergyRequired = int.MaxValue;

            Dictionary<int, char> hallway = GetHallway(amphipodsBurrow);
            Dictionary<char, Queue<char>> roomsAmphipods = GetRoomsAmphipods(amphipodsBurrow);

            DoCalculateLeastEnergyRequiredToOrganize(hallway, roomsAmphipods, 0, leastEnergyRequired);

            return leastEnergyRequired;
        }

        private void DoCalculateLeastEnergyRequiredToOrganize(
            Dictionary<int, char> hallway,
            Dictionary<char, Queue<char>> roomsAmphipods,
            int energyRequired,
            int leastEnergyRequired
        )
        {
            // If energy required is greater than or equal to least energy required
            if (energyRequired >= leastEnergyRequired)
            {
                return;
            }

            bool areAmphipodsOrganized = AreAmphipodsOrganized(roomsAmphipods);
            if (areAmphipodsOrganized)
            {
                leastEnergyRequired = energyRequired;
            }

            foreach (KeyValuePair<char, Queue<char>> roomAmphipods in roomsAmphipods)
            {
                Queue<char> amphipods = roomAmphipods.Value;

                while (amphipods.Count > 0)
                {
                    Dictionary<char, Queue<char>> nextRoomsAmphipods =
                        new Dictionary<char, Queue<char>>(roomsAmphipods);

                    // Step out the room
                    foreach (KeyValuePair<int, char> position in hallway)
                    {
                        if (position.Value == OPEN_SPACE)
                        {
                            char amphipod = amphipods.Dequeue();
                            int steps = Math.Abs(position.Key - roomsVerticalPositions[amphipod]);
                            energyRequired += steps * amphipodsEnergy[amphipod];

                            DoCalculateLeastEnergyRequiredToOrganize(
                                hallway,
                                roomsAmphipods,
                                energyRequired,
                                leastEnergyRequired
                            );
                        }
                    }

                    // Step in the room
                    foreach (KeyValuePair<int, char> position in hallway)
                    {
                        char amphipod = amphipods.Dequeue();
                        int steps = Math.Abs(position.Key - roomsVerticalPositions[amphipod]);
                        energyRequired += steps * amphipodsEnergy[amphipod];

                        DoCalculateLeastEnergyRequiredToOrganize(
                            hallway,
                            roomsAmphipods,
                            energyRequired,
                            leastEnergyRequired
                        );
                    }
                }
            }
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

        private bool AreAmphipodsOrganized(Dictionary<char, Queue<char>> roomsAmphipods)
        {
            foreach (KeyValuePair<char, Queue<char>> roomAmphipods in roomsAmphipods)
            {
                char amphipodsTypeForRoom = roomAmphipods.Key;
                Queue<char> amphipods = roomAmphipods.Value;

                while (amphipods.Count > 0)
                {
                    char amphipod = amphipods.Dequeue();
                    if (amphipodsTypeForRoom != amphipod)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void MoveAmphipodOut()
        {
        }

        private void MoveAmphipodIn()
        {
        }
    }
}
