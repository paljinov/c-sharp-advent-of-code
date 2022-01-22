using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2019.Day18
{
    public class Tunnels
    {
        private const char ENTRANCE = '@';

        private const char OPEN_PASSAGE = '.';

        private const char STONE_WALL = '#';

        private static readonly (int X, int Y)[] possibleSteps = new (int X, int Y)[] {
            // Down
            (1, 0),
            // Right
            (0, 1),
            // Up
            (-1, 0),
            // Left
            (0, -1)
        };

        private readonly char[,] findArea = new char[,] {
            { OPEN_PASSAGE, OPEN_PASSAGE, OPEN_PASSAGE },
            { OPEN_PASSAGE, ENTRANCE, OPEN_PASSAGE },
            { OPEN_PASSAGE, OPEN_PASSAGE, OPEN_PASSAGE }
        };

        private readonly char[,] updateArea = new char[,] {
            { ENTRANCE, STONE_WALL, ENTRANCE },
            { STONE_WALL, STONE_WALL, STONE_WALL },
            { ENTRANCE, STONE_WALL, ENTRANCE }
        };

        public int CountStepsOfShortestPathThatCollectsAllOfTheKeys(char[,] tunnelsMap)
        {
            int minSteps = int.MaxValue;
            (int X, int Y) entrance = GetCharacterLocations(ENTRANCE, tunnelsMap).First();
            tunnelsMap[entrance.X, entrance.Y] = OPEN_PASSAGE;

            string keys = GetKeys(tunnelsMap);
            Dictionary<char, (int X, int Y)> keysLocations = new Dictionary<char, (int X, int Y)>();
            foreach (char key in keys)
            {
                (int X, int Y) keyLocation = GetCharacterLocations(key, tunnelsMap).First();
                keysLocations[key] = keyLocation;
            }

            string doors = GetDoors(tunnelsMap);
            Dictionary<char, (int X, int Y)> doorsLocations = new Dictionary<char, (int X, int Y)>();
            foreach (char door in doors)
            {
                (int X, int Y) doorLocation = GetCharacterLocations(door, tunnelsMap).First();
                doorsLocations[door] = doorLocation;
            }

            Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsToKey =
                new Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>>();

            foreach (char key in $"{ENTRANCE}{keys}")
            {
                (int X, int Y) keyLocation = entrance;
                if (keysLocations.ContainsKey(key))
                {
                    keyLocation = keysLocations[key];
                }

                Dictionary<char, (int Steps, string DoorsBetween)> reachedKeys = new Dictionary<char, (int Steps, string DoorsBetween)>();
                GetStepsToKey(tunnelsMap, keyLocation, 0, string.Empty, new Dictionary<string, int>(), reachedKeys);
                reachedKeys.Remove(key);
                stepsToKey[key] = reachedKeys;
            }

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                tunnelsMap,
                ENTRANCE,
                entrance,
                keysLocations,
                doorsLocations,
                stepsToKey,
                new Dictionary<string,
                int>(),
                keys.ToHashSet(),
                0,
                ref minSteps
            );

            return minSteps;
        }

        public int CountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(char[,] tunnelsMap)
        {
            int minSteps = int.MaxValue;

            tunnelsMap = UpdateMap(tunnelsMap);
            (int X,
            int Y)[] robotsLocations = GetCharacterLocations(ENTRANCE, tunnelsMap).ToArray();
            Direction?[] robotsDirections = new Direction?[robotsLocations.Length];
            int[] robotsStepsToNextKey = new int[robotsLocations.Length];

            Dictionary<string, int> statesCache = new Dictionary<string, int>();
            string keys = GetKeys(tunnelsMap);

            DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
                tunnelsMap, robotsLocations, robotsDirections, statesCache, keys, 0, ref minSteps, robotsStepsToNextKey, 0);

            return minSteps;
        }

        private void DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
            char[,] tunnelsMap,
            char currentKey,
            (int X, int Y) currentLocation,
            Dictionary<char, (int X, int Y)> keysLocations,
            Dictionary<char, (int X, int Y)> doorsLocations,
            Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsToKey,
            Dictionary<string, int> statesCache,
            HashSet<char> remainingKeys,
            int steps,
            ref int minSteps
        )
        {
            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            string remainingKeysString = string.Concat(remainingKeys);
            string state = StringifyState(remainingKeysString, currentLocation);
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && steps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = steps;

            Dictionary<char, int> reachableKeys = new Dictionary<char, int>();
            foreach (KeyValuePair<char, (int Steps, string DoorsBetween)> stepsToKeyFromKey in stepsToKey[currentKey])
            {
                string lowercaseDoorsBetween = stepsToKeyFromKey.Value.DoorsBetween.ToLower();

                bool doorStillExists = false;
                foreach (char door in lowercaseDoorsBetween)
                {
                    doorStillExists = remainingKeys.Contains(door);
                    if (doorStillExists)
                    {
                        break;
                    }
                }

                if (!doorStillExists)
                {
                    reachableKeys.Add(stepsToKeyFromKey.Key, stepsToKeyFromKey.Value.Steps);
                }
            }

            foreach (KeyValuePair<char, int> reachableKey in reachableKeys)
            {
                char[,] tunnelsMapCopy = tunnelsMap.Clone() as char[,];
                HashSet<char> remainingKeysCopy = remainingKeys.ToHashSet();
                int newSteps = steps + reachableKey.Value;

                char nextKey = reachableKey.Key;
                (int X, int Y) nextKeyLocation = keysLocations[nextKey];
                char nextDoor = char.ToUpper(nextKey);

                remainingKeysCopy.Remove(nextKey);

                // If all keys are collected
                if (remainingKeysCopy.Count == 0 && newSteps < minSteps)
                {
                    minSteps = newSteps;
                    return;
                }

                tunnelsMapCopy[nextKeyLocation.X, nextKeyLocation.Y] = OPEN_PASSAGE;
                if (doorsLocations.ContainsKey(nextDoor))
                {
                    tunnelsMapCopy[doorsLocations[nextDoor].X, doorsLocations[nextDoor].Y] = OPEN_PASSAGE;
                }

                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                    tunnelsMapCopy,
                    nextKey,
                    nextKeyLocation,
                    keysLocations,
                    doorsLocations,
                    stepsToKey,
                    statesCache,
                    remainingKeysCopy,
                    newSteps,
                    ref minSteps
                );
            }
        }

        private void DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
            char[,] tunnelsMap,
            (int X, int Y)[] robotsLocations,
            Direction?[] robotsDirections,
            Dictionary<string, int> statesCache,
            string remainingKeys,
            int steps,
            ref int minSteps,
            int[] robotsStepsToNextKey,
            int currentRobot
        )
        {
            int currentSteps = steps + robotsStepsToNextKey[currentRobot];
            (int X, int Y) currentLocation = robotsLocations[currentRobot];
            Direction? currentDirection = robotsDirections[currentRobot];

            // If better solution is already found
            if (currentSteps >= minSteps)
            {
                return;
            }

            string state = StringifyStateForRemoteControlledRobots(remainingKeys, currentRobot, robotsLocations);
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && currentSteps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = currentSteps;

            // If all keys are collected
            if (string.IsNullOrEmpty(remainingKeys))
            {
                minSteps = steps;
                return;
            }

            Dictionary<(int X, int Y), Direction> nextLocations =
                GetNextStepLocations(tunnelsMap, currentLocation, currentDirection);

            // If robot can't go anywhere change robot
            if (nextLocations.Count == 0)
            {
                currentRobot += 1;
                if (currentRobot == robotsLocations.Length)
                {
                    currentRobot = 0;
                }

                DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
                    tunnelsMap,
                    robotsLocations,
                    robotsDirections,
                    statesCache,
                    remainingKeys,
                    steps,
                    ref minSteps,
                    robotsStepsToNextKey,
                    currentRobot
                );
            }
            else
            {
                robotsStepsToNextKey[currentRobot]++;
                foreach (KeyValuePair<(int X, int Y), Direction> nextLocation in nextLocations)
                {
                    currentSteps = steps;

                    (char[,] TunnelsMap, string RemainingKeys, Direction? Direction) nextStepTunnelMap =
                        GetNextStepTunnelMap(tunnelsMap, nextLocation.Key, nextLocation.Value, remainingKeys);

                    // If key is found
                    if (!nextStepTunnelMap.Direction.HasValue)
                    {
                        currentSteps += robotsStepsToNextKey[currentRobot];
                    }

                    robotsLocations[currentRobot] = (nextLocation.Key.X, nextLocation.Key.Y);
                    robotsDirections[currentRobot] = nextStepTunnelMap.Direction;

                    DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
                        nextStepTunnelMap.TunnelsMap,
                        robotsLocations,
                        robotsDirections,
                        statesCache,
                        nextStepTunnelMap.RemainingKeys,
                        currentSteps,
                        ref minSteps,
                        robotsStepsToNextKey,
                        currentRobot
                    );
                }
            }
        }

        /// <summary>
        ///     Get next step locations.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y)"></param>
        /// <param name="tunnelsMap"></param>
        /// <param name="X"></param>
        /// <param name="currentLocation"></param>
        /// <param name="currentDirection">Current direction is used to stop going backwards</param>
        /// <returns></returns>
        private Dictionary<(int X, int Y), Direction> GetNextStepLocations(
            char[,] tunnelsMap,
            (int X, int Y) currentLocation,
            Direction? currentDirection
        )
        {
            Dictionary<(int X, int Y), Direction> nextLocations = new Dictionary<(int X, int Y), Direction>();

            if ((!currentDirection.HasValue || currentDirection.Value != Direction.Up)
                && currentLocation.X - 1 >= 0
                && tunnelsMap[currentLocation.X - 1, currentLocation.Y] != STONE_WALL
                && !char.IsUpper(tunnelsMap[currentLocation.X - 1, currentLocation.Y]))
            {
                nextLocations.Add((currentLocation.X - 1, currentLocation.Y), Direction.Down);
            }

            if ((!currentDirection.HasValue || currentDirection.Value != Direction.Down)
                && currentLocation.X + 1 < tunnelsMap.GetLength(0)
                && tunnelsMap[currentLocation.X + 1, currentLocation.Y] != STONE_WALL
                && !char.IsUpper(tunnelsMap[currentLocation.X + 1, currentLocation.Y]))
            {
                nextLocations.Add((currentLocation.X + 1, currentLocation.Y), Direction.Up);
            }

            if ((!currentDirection.HasValue || currentDirection.Value != Direction.Right)
                && currentLocation.Y - 1 >= 0
                && tunnelsMap[currentLocation.X, currentLocation.Y - 1] != STONE_WALL
                && !char.IsUpper(tunnelsMap[currentLocation.X, currentLocation.Y - 1]))
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y - 1), Direction.Left);
            }

            if ((!currentDirection.HasValue || currentDirection.Value != Direction.Left)
                && currentLocation.Y + 1 < tunnelsMap.GetLength(1)
                && tunnelsMap[currentLocation.X, currentLocation.Y + 1] != STONE_WALL
                && !char.IsUpper(tunnelsMap[currentLocation.X, currentLocation.Y + 1]))
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y + 1), Direction.Right);
            }

            return nextLocations;
        }

        private (char[,] TunnelsMap, string RemainingKeys, Direction? Direction) GetNextStepTunnelMap(
            char[,] tunnelsMap,
            (int X, int Y) nextLocation,
            Direction nextDirection,
            string remainingKeys
        )
        {
            // If key is found
            if (char.IsLower(tunnelsMap[nextLocation.X, nextLocation.Y]))
            {
                char[,] tunnelsMapCopy = tunnelsMap.Clone() as char[,];
                string remainingKeysCopy = new string(remainingKeys);

                List<(int X, int Y)> doors = GetCharacterLocations(
                    char.ToUpper(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), tunnelsMapCopy);

                // Take key
                remainingKeysCopy = remainingKeysCopy.Remove(
                    remainingKeysCopy.IndexOf(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), 1);
                tunnelsMapCopy[nextLocation.X, nextLocation.Y] = OPEN_PASSAGE;
                if (doors.Count > 0)
                {
                    (int X, int Y) door = doors.First();
                    tunnelsMapCopy[door.X, door.Y] = OPEN_PASSAGE;
                }

                return (tunnelsMapCopy, remainingKeysCopy, null);
            }
            // If next location is open passage
            else
            {
                return (tunnelsMap, remainingKeys, nextDirection);
            }
        }

        private void GetStepsToKey(
            char[,] tunnelsMap,
            (int X, int Y) currentLocation,
            int steps,
            string doors,
            Dictionary<string, int> statesCache,
            Dictionary<char, (int Steps, string DoorsBetween)> reachableKeys
        )
        {
            string state = $"{currentLocation.X},{currentLocation.Y}";
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && steps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = steps;

            // If key is found
            if (char.IsLower(tunnelsMap[currentLocation.X, currentLocation.Y]))
            {
                reachableKeys[tunnelsMap[currentLocation.X, currentLocation.Y]] = (steps, doors);
            }

            steps++;
            foreach ((int X, int Y) step in possibleSteps)
            {
                (int X, int Y) nextLocation = (currentLocation.X + step.X, currentLocation.Y + step.Y);

                // If next position is inside tunnels map boundaries, and it is not stone wall or closed doors
                if (nextLocation.X >= 0 && nextLocation.X < tunnelsMap.GetLength(0)
                    && nextLocation.Y >= 0 && nextLocation.Y < tunnelsMap.GetLength(1)
                    && tunnelsMap[nextLocation.X, nextLocation.Y] != STONE_WALL)
                {
                    string newDoors = doors;
                    if (char.IsUpper(tunnelsMap[nextLocation.X, nextLocation.Y]))
                    {
                        newDoors += tunnelsMap[nextLocation.X, nextLocation.Y];
                    }

                    GetStepsToKey(
                        tunnelsMap,
                        nextLocation,
                        steps,
                        newDoors,
                        statesCache,
                        reachableKeys
                    );
                }
            }
        }

        private List<(int X, int Y)> GetCharacterLocations(char character, char[,] tunnelsMap)
        {
            List<(int X, int Y)> characterLocations = new List<(int X, int Y)>();

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (tunnelsMap[i, j] == character)
                    {
                        characterLocations.Add((i, j));
                    }
                }
            }

            return characterLocations;
        }

        private string GetKeys(char[,] tunnelsMap)
        {
            StringBuilder keys = new StringBuilder();

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (char.IsLower(tunnelsMap[i, j]))
                    {
                        keys.Append(tunnelsMap[i, j]);
                    }
                }
            }

            return keys.ToString();
        }

        private string GetDoors(char[,] tunnelsMap)
        {
            StringBuilder keys = new StringBuilder();

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (char.IsUpper(tunnelsMap[i, j]))
                    {
                        keys.Append(tunnelsMap[i, j]);
                    }
                }
            }

            return keys.ToString();
        }

        private string StringifyState(string remainingKeys, (int X, int Y) currentLocation)
        {
            return $"({remainingKeys}),({currentLocation.X},{currentLocation.Y})";
        }

        private string StringifyStateForRemoteControlledRobots(
            string remainingKeys,
            int currentRobot,
            (int X, int Y)[] robotsLocations
        )
        {
            StringBuilder state = new StringBuilder($"({remainingKeys}),({currentRobot})");
            foreach ((int X, int Y) currentLocation in robotsLocations)
            {
                state.Append($",({currentLocation.X},{currentLocation.Y})");
            }

            return state.ToString();
        }

        private char[,] UpdateMap(char[,] tunnelsMap)
        {
            bool isAreaFound = true;

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    isAreaFound = true;
                    // Find area
                    for (int k = 0; k < findArea.GetLength(0); k++)
                    {
                        for (int h = 0; h < findArea.GetLength(1); h++)
                        {
                            if (i + k < tunnelsMap.GetLength(0) && j + h < tunnelsMap.GetLength(1)
                                && tunnelsMap[i + k, j + h] != findArea[k, h])
                            {
                                isAreaFound = false;
                                break;
                            }
                        }

                        if (!isAreaFound)
                        {
                            break;
                        }
                    }

                    // Update map
                    if (isAreaFound)
                    {
                        for (int k = 0; k < updateArea.GetLength(0); k++)
                        {
                            for (int h = 0; h < updateArea.GetLength(1); h++)
                            {
                                tunnelsMap[i + k, j + h] = updateArea[k, h];
                            }
                        }

                        break;
                    }
                }

                if (isAreaFound)
                {
                    break;
                }
            }

            return tunnelsMap;
        }
    }
}
