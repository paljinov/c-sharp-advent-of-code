using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day23
{
    public class Amphipods
    {
        private const char OPEN_SPACE = '.';

        private const int HALLWAY_HORIZONTAL_POSITION = 1;

        private readonly IEnumerable<char> amphipodsTypes = Enum.GetValues(typeof(AmphipodType))
            .Cast<AmphipodType>()
            .Select(at => (char)at);

        private static readonly Dictionary<char, int> amphipodsStepEnergy = new Dictionary<char, int>()
        {
            { (char)AmphipodType.Amber, 1 },
            { (char)AmphipodType.Bronze, 10 },
            { (char)AmphipodType.Copper, 100 },
            { (char)AmphipodType.Desert, 1000 }
        };

        private static readonly Dictionary<char, int> roomsVerticalPositions = new Dictionary<char, int>()
        {
            { (char)AmphipodType.Amber, 2 },
            { (char)AmphipodType.Bronze, 4 },
            { (char)AmphipodType.Copper, 6 },
            { (char)AmphipodType.Desert, 8 }
        };

        public int CalculateLeastEnergyRequiredToOrganizeTheAmphipods(char[,] amphipodsBurrow)
        {
            Dictionary<int, char> hallway = GetHallway(amphipodsBurrow);
            Dictionary<char, List<char>> roomsAmphipods = GetRoomsAmphipods(amphipodsBurrow);

            int leastEnergyRequired = DoCalculateLeastEnergyRequiredToOrganizeTheAmphipods(hallway, roomsAmphipods);

            return leastEnergyRequired;
        }

        public int CalculateLeastEnergyRequiredToOrganizeTheAmphipodsForFullDiagram(char[,] amphipodsBurrow)
        {
            Dictionary<int, char> hallway = GetHallway(amphipodsBurrow);
            Dictionary<char, List<char>> roomsAmphipods = GetRoomsAmphipods(amphipodsBurrow);

            // Between the first and second lines of text that contain amphipod starting positions
            // insert the following lines:
            // #D#C#B#A#
            // #D#B#A#C#
            roomsAmphipods[(char)AmphipodType.Amber].Insert(1, (char)AmphipodType.Desert);
            roomsAmphipods[(char)AmphipodType.Amber].Insert(1, (char)AmphipodType.Desert);
            roomsAmphipods[(char)AmphipodType.Bronze].Insert(1, (char)AmphipodType.Bronze);
            roomsAmphipods[(char)AmphipodType.Bronze].Insert(1, (char)AmphipodType.Copper);
            roomsAmphipods[(char)AmphipodType.Copper].Insert(1, (char)AmphipodType.Amber);
            roomsAmphipods[(char)AmphipodType.Copper].Insert(1, (char)AmphipodType.Bronze);
            roomsAmphipods[(char)AmphipodType.Desert].Insert(1, (char)AmphipodType.Copper);
            roomsAmphipods[(char)AmphipodType.Desert].Insert(1, (char)AmphipodType.Amber);

            int leastEnergyRequired = DoCalculateLeastEnergyRequiredToOrganizeTheAmphipods(hallway, roomsAmphipods);

            return leastEnergyRequired;
        }

        private Dictionary<int, char> GetHallway(char[,] amphipodsBurrow)
        {
            Dictionary<int, char> hallway = new Dictionary<int, char>();

            for (int j = 1; j < amphipodsBurrow.GetLength(1) - 1; j++)
            {
                hallway[j - 1] = amphipodsBurrow[HALLWAY_HORIZONTAL_POSITION, j];
            }

            return hallway;
        }

        private Dictionary<char, List<char>> GetRoomsAmphipods(char[,] amphipodsBurrow)
        {
            Dictionary<char, List<char>> roomsAmphipods = new Dictionary<char, List<char>>();

            foreach (KeyValuePair<char, int> roomVerticalPosition in roomsVerticalPositions)
            {
                List<char> roomAmphipods = new List<char>();

                for (int i = HALLWAY_HORIZONTAL_POSITION + 1; i < amphipodsBurrow.GetLength(0) - 1; i++)
                {
                    roomAmphipods.Add(amphipodsBurrow[i, roomVerticalPosition.Value + 1]);
                }

                roomsAmphipods[roomVerticalPosition.Key] = roomAmphipods;
            }

            return roomsAmphipods;
        }

        private int DoCalculateLeastEnergyRequiredToOrganizeTheAmphipods(
            Dictionary<int, char> hallway,
            Dictionary<char, List<char>> roomsAmphipods
        )
        {
            State start = new State(hallway, roomsAmphipods);
            State organizedRoomsAmphipods = start.Clone();
            organizedRoomsAmphipods.Rooms = GetOrganizedRoomsAmphipods(start.Rooms.First().Value.Count);

            // Unprocessed amphipods moves
            PriorityQueue<State, int> nextMoves = new PriorityQueue<State, int>();
            nextMoves.Enqueue(start, 0);

            // States with least energy needed to reach it
            Dictionary<State, int> states = new Dictionary<State, int>(new StateComparer())
            {
                { start, 0 }
            };

            // Using Dijkstra's algorithm to move amphipods
            while (nextMoves.Count > 0)
            {
                State currentState = nextMoves.Dequeue();
                int currentEnergy = states[currentState];

                List<(State State, int Energy)> nextMovesFromCurrentState = FindNextMoves(currentState);

                foreach ((State nextState, int nextEnergy) in nextMovesFromCurrentState)
                {
                    int totalEnergy = currentEnergy + nextEnergy;
                    // If this state doesn't exists, or it is reached with less spent energy
                    if (!states.ContainsKey(nextState) || totalEnergy < states[nextState])
                    {
                        nextMoves.Enqueue(nextState, totalEnergy);
                        states[nextState] = totalEnergy;
                    }
                }
            }

            return states[organizedRoomsAmphipods];
        }

        private Dictionary<char, List<char>> GetOrganizedRoomsAmphipods(int roomSize)
        {
            Dictionary<char, List<char>> organizedRoomsAmphipods = new Dictionary<char, List<char>>();

            foreach (char amphipodType in amphipodsTypes)
            {
                char amphipod = amphipodType;

                List<char> amphipods = new List<char>();
                for (int i = 0; i < roomSize; i++)
                {
                    amphipods.Add(amphipod);
                }

                organizedRoomsAmphipods[amphipod] = amphipods;
            }

            return organizedRoomsAmphipods;
        }

        private List<(State State, int Energy)> FindNextMoves(State state)
        {
            List<(State State, int Energy)> nextMoves = new List<(State State, int Energy)>();

            foreach (KeyValuePair<char, int> roomVerticalPosition in roomsVerticalPositions)
            {
                List<int> hallwayOpenSpaces = FindHallwayOpenSpaces(state, roomVerticalPosition.Value);
                foreach (int hallwayPosition in hallwayOpenSpaces)
                {
                    (State? nextState, int energy) = MoveAmphipodOut(state, roomVerticalPosition.Key, hallwayPosition);
                    if (nextState.HasValue)
                    {
                        nextMoves.Add((nextState.Value, energy));
                    }
                }
            }

            for (int hallwayPosition = 0; hallwayPosition < state.Hallway.Count; hallwayPosition++)
            {
                // If there is amphipod on hallway position
                if (state.Hallway[hallwayPosition] != OPEN_SPACE)
                {
                    (State? nextState, int energy) = MoveAmphipodIn(state, hallwayPosition);
                    if (nextState.HasValue)
                    {
                        nextMoves.Add((nextState.Value, energy));
                    }
                }
            }

            return nextMoves;
        }

        private (State?, int) MoveAmphipodOut(State state, char room, int hallwayPosition)
        {
            char amphipod = '\0';
            int roomPosition = -1;

            // Take first amphipod from the room
            for (int level = 0; level < state.Rooms[room].Count; level++)
            {
                amphipod = state.Rooms[room][level];
                if (amphipodsTypes.Contains(amphipod))
                {
                    roomPosition = level;
                    break;
                }
            }

            if (roomPosition < 0)
            {
                return (null, 0);
            }

            int steps = Math.Abs(hallwayPosition - roomsVerticalPositions[room]) + roomPosition + 1;
            int spentEnergy = steps * amphipodsStepEnergy[amphipod];

            State newState = state.Clone();
            newState.Hallway[hallwayPosition] = amphipod;
            newState.Rooms[room][roomPosition] = OPEN_SPACE;

            return (newState, spentEnergy);
        }

        private (State?, int) MoveAmphipodIn(State state, int amphipodHallwayPosition)
        {
            char amphipod = state.Hallway[amphipodHallwayPosition];

            int destinationRoomPosition = roomsVerticalPositions[amphipod];
            int startPosition = destinationRoomPosition > amphipodHallwayPosition
                ? amphipodHallwayPosition + 1 : amphipodHallwayPosition - 1;
            int leftPosition = Math.Min(destinationRoomPosition, startPosition);
            int rightPosition = Math.Max(destinationRoomPosition, startPosition);

            IEnumerable<KeyValuePair<int, char>> pathToDestinationRoom = state.Hallway
                .Skip(leftPosition)
                .Take(rightPosition - leftPosition + 1);

            // If path is not clear
            if (pathToDestinationRoom.Any(p => p.Value != OPEN_SPACE))
            {
                return (null, 0);
            }

            // If any position in this room is filled with other amphipod types
            if (state.Rooms[amphipod].Any(a => a != OPEN_SPACE && a != amphipod))
            {
                return (null, 0);
            }

            // Amphipod is placed on deepest possible position inside the room
            int roomPosition = state.Rooms[amphipod].LastIndexOf(OPEN_SPACE);
            int steps = rightPosition - leftPosition + 1 + roomPosition + 1;
            int spentEnergy = steps * amphipodsStepEnergy[amphipod];

            State newState = state.Clone();
            newState.Hallway[amphipodHallwayPosition] = OPEN_SPACE;
            newState.Rooms[amphipod][roomPosition] = amphipod;

            return (newState, spentEnergy);
        }

        private List<int> FindHallwayOpenSpaces(State state, int roomPosition)
        {
            List<int> openSpaces = new List<int>();

            // Left
            for (int position = roomPosition - 1; position >= 0; position--)
            {
                // Until there is amphipod
                if (state.Hallway[position] != OPEN_SPACE)
                {
                    break;
                }

                // Amphipods will never stop on the space immediately outside any room
                if (!roomsVerticalPositions.ContainsValue(position))
                {
                    openSpaces.Add(position);
                }
            }

            // Right
            for (int position = roomPosition + 1; position < state.Hallway.Count; position++)
            {
                // Until there is amphipod
                if (state.Hallway[position] != OPEN_SPACE)
                {
                    break;
                }

                // Amphipods will never stop on the space immediately outside any room
                if (!roomsVerticalPositions.ContainsValue(position))
                {
                    openSpaces.Add(position);
                }
            }

            return openSpaces;
        }
    }
}
