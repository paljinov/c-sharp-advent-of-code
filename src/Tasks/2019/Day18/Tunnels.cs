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

        private static readonly (int X, int Y)[] steps = new (int X, int Y)[] {
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

            (int X, int Y) entrance = GetCharacterPositions(ENTRANCE, tunnelsMap).First();
            Dictionary<char, (int X, int Y)> keysPositions = GetKeysPositions(tunnelsMap);
            Dictionary<char, (int X, int Y)> doorsPositions = GetDoorsPositions(tunnelsMap);
            Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsFromKeyToKeys =
                GetStepsFromKeysToOtherKeys(tunnelsMap, entrance);
            Dictionary<char, bool> foundKeys = keysPositions.ToDictionary(kl => kl.Key, kl => false);

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                tunnelsMap,
                entrance,
                keysPositions,
                doorsPositions,
                stepsFromKeyToKeys,
                new Dictionary<string, int>(),
                foundKeys,
                0,
                ref minSteps
            );

            return minSteps;
        }

        public int CountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(char[,] tunnelsMap)
        {
            int totalMinSteps = 0;

            tunnelsMap = UpdateMap(tunnelsMap);

            List<(int X, int Y)> robotsPositions = GetCharacterPositions(ENTRANCE, tunnelsMap);
            Dictionary<char, (int X, int Y)> keysPositions = GetKeysPositions(tunnelsMap);
            Dictionary<char, (int X, int Y)> doorsPositions = GetDoorsPositions(tunnelsMap);

            for (int robot = 0; robot < robotsPositions.Count; robot++)
            {
                Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsFromKeyToKeys =
                    GetStepsFromKeysToOtherKeys(tunnelsMap, robotsPositions[robot]);

                // Reacheable keys for this robot
                List<char> reacheableKeys = stepsFromKeyToKeys.Select(s => s.Key).Where(k => k != ENTRANCE).ToList();
                Dictionary<char, bool> foundKeys = reacheableKeys.ToDictionary(rk => rk, rk => false);

                // Assume that other robots opened doors for keys which are in their area
                foreach (KeyValuePair<char, Dictionary<char, (int Steps, string DoorsBetween)>> fromKey in stepsFromKeyToKeys)
                {
                    foreach (KeyValuePair<char, (int Steps, string DoorsBetween)> toKey in fromKey.Value)
                    {
                        string doorsBetween = string.Empty;
                        foreach (char c in toKey.Value.DoorsBetween)
                        {
                            if (reacheableKeys.Contains(c))
                            {
                                doorsBetween += c;
                            }
                        }

                        stepsFromKeyToKeys[fromKey.Key][toKey.Key] = (toKey.Value.Steps, doorsBetween);
                    }
                }

                int minSteps = int.MaxValue;
                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                    tunnelsMap,
                    robotsPositions[robot],
                    keysPositions,
                    doorsPositions,
                    stepsFromKeyToKeys,
                    new Dictionary<string, int>(),
                    foundKeys,
                    0,
                    ref minSteps
                );

                totalMinSteps += minSteps;
            }

            return totalMinSteps;
        }

        private void DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
            char[,] tunnelsMap,
            (int X, int Y) currentPosition,
            Dictionary<char, (int X, int Y)> keysPositions,
            Dictionary<char, (int X, int Y)> doorsPositions,
            Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsFromKeyToKeys,
            Dictionary<string, int> statesCache,
            Dictionary<char, bool> foundKeys,
            int steps,
            ref int minSteps
        )
        {
            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            string foundKeysString = string.Concat(foundKeys.Where(fk => fk.Value == true).Select(fk => fk.Key));
            string state = StringifyState(foundKeysString, currentPosition);
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && steps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = steps;

            // If all keys are collected in minimum number of steps
            if (!foundKeys.Where(fk => fk.Value == false).Any())
            {
                minSteps = steps;
                return;
            }

            char currentKey = tunnelsMap[currentPosition.X, currentPosition.Y];
            Dictionary<char, int> reachableKeys = GetKeysReachableFromKey(stepsFromKeyToKeys[currentKey], foundKeys);

            foreach (KeyValuePair<char, int> reachableKey in reachableKeys)
            {
                char nextKey = reachableKey.Key;
                int newSteps = steps + reachableKey.Value;

                Dictionary<char, bool> nextFoundKeys = foundKeys.ToDictionary(fk => fk.Key, fk => fk.Value);
                nextFoundKeys[nextKey] = true;

                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                    tunnelsMap,
                    keysPositions[nextKey],
                    keysPositions,
                    doorsPositions,
                    stepsFromKeyToKeys,
                    statesCache,
                    nextFoundKeys,
                    newSteps,
                    ref minSteps
                );
            }
        }

        private Dictionary<char, int> GetKeysReachableFromKey(
            Dictionary<char, (int Steps, string DoorsBetween)> stepsToKey,
            Dictionary<char, bool> foundKeys
        )
        {
            Dictionary<char, int> reachableKeys = new Dictionary<char, int>();

            foreach (KeyValuePair<char, (int Steps, string DoorsBetween)> stepsToKeyFromKey in stepsToKey)
            {
                // If key is already found
                if (foundKeys[stepsToKeyFromKey.Key])
                {
                    continue;
                }

                bool doorStillExists = false;
                foreach (char door in stepsToKeyFromKey.Value.DoorsBetween)
                {
                    if (!foundKeys[door])
                    {
                        doorStillExists = true;
                        break;
                    }
                }

                if (!doorStillExists)
                {
                    reachableKeys.Add(stepsToKeyFromKey.Key, stepsToKeyFromKey.Value.Steps);
                }
            }

            return reachableKeys;
        }

        private Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> GetStepsFromKeysToOtherKeys(
            char[,] tunnelsMap,
            (int X, int Y) currentPosition
        )
        {
            Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsFromKeyToKeys =
                new Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>>();

            Queue<(int X, int Y)> nonProcessedPositions = new Queue<(int X, int Y)>();
            nonProcessedPositions.Enqueue(currentPosition);

            while (nonProcessedPositions.Count > 0)
            {
                (int X, int Y) keyPosition = nonProcessedPositions.Dequeue();
                char key = tunnelsMap[keyPosition.X, keyPosition.Y];

                Dictionary<char, (int Steps, string DoorsBetween)> reachedKeys =
                    new Dictionary<char, (int Steps, string DoorsBetween)>();

                GetStepsFromKeyToKeys(
                    tunnelsMap,
                    keyPosition,
                    0,
                    string.Empty,
                    new Dictionary<string, int>(),
                    reachedKeys
                );

                reachedKeys.Remove(key);
                stepsFromKeyToKeys[key] = reachedKeys;

                foreach (char reachedKey in reachedKeys.Keys)
                {
                    if (!stepsFromKeyToKeys.ContainsKey(reachedKey))
                    {
                        nonProcessedPositions.Enqueue(GetCharacterPositions(reachedKey, tunnelsMap).First());
                    }
                }
            }

            return stepsFromKeyToKeys;
        }

        private void GetStepsFromKeyToKeys(
            char[,] tunnelsMap,
            (int X, int Y) currentPosition,
            int steps,
            string doors,
            Dictionary<string, int> statesCache,
            Dictionary<char, (int Steps, string DoorsBetween)> reachableKeys
        )
        {
            string state = $"{currentPosition.X},{currentPosition.Y}";
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && steps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = steps;

            // If key is found
            if (char.IsLower(tunnelsMap[currentPosition.X, currentPosition.Y]))
            {
                reachableKeys[tunnelsMap[currentPosition.X, currentPosition.Y]] = (steps, doors.ToLower());
            }

            steps++;
            foreach ((int X, int Y) step in Tunnels.steps)
            {
                (int X, int Y) nextPosition = (currentPosition.X + step.X, currentPosition.Y + step.Y);

                // If next position is inside tunnels map boundaries, and it is not stone wall or closed doors
                if (nextPosition.X >= 0 && nextPosition.X < tunnelsMap.GetLength(0)
                    && nextPosition.Y >= 0 && nextPosition.Y < tunnelsMap.GetLength(1)
                    && tunnelsMap[nextPosition.X, nextPosition.Y] != STONE_WALL)
                {
                    string newDoors = doors;
                    if (char.IsUpper(tunnelsMap[nextPosition.X, nextPosition.Y]))
                    {
                        newDoors += tunnelsMap[nextPosition.X, nextPosition.Y];
                    }

                    GetStepsFromKeyToKeys(
                        tunnelsMap,
                        nextPosition,
                        steps,
                        newDoors,
                        statesCache,
                        reachableKeys
                    );
                }
            }
        }

        private List<(int X, int Y)> GetCharacterPositions(char character, char[,] tunnelsMap)
        {
            List<(int X, int Y)> characterPositions = new List<(int X, int Y)>();

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (tunnelsMap[i, j] == character)
                    {
                        characterPositions.Add((i, j));
                    }
                }
            }

            return characterPositions;
        }

        private Dictionary<char, (int X, int Y)> GetKeysPositions(char[,] tunnelsMap)
        {
            Dictionary<char, (int X, int Y)> keysPositions = new Dictionary<char, (int X, int Y)>();

            string keys = GetKeys(tunnelsMap);
            foreach (char key in keys)
            {
                (int X, int Y) keyPosition = GetCharacterPositions(key, tunnelsMap).First();
                keysPositions[key] = keyPosition;
            }

            return keysPositions;
        }

        private Dictionary<char, (int X, int Y)> GetDoorsPositions(char[,] tunnelsMap)
        {
            Dictionary<char, (int X, int Y)> doorsPositions = new Dictionary<char, (int X, int Y)>();

            string doors = GetDoors(tunnelsMap).ToLower();
            foreach (char door in doors)
            {
                (int X, int Y) doorPosition = GetCharacterPositions(door, tunnelsMap).First();
                doorsPositions[door] = doorPosition;
            }

            return doorsPositions;
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

        private string StringifyState(string remainingKeys, (int X, int Y) currentPosition)
        {
            return $"({remainingKeys}),({currentPosition.X},{currentPosition.Y})";
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
