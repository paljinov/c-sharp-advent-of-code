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

            (int X, int Y) entrance = GetCharacterLocations(ENTRANCE, tunnelsMap).First();
            Dictionary<char, (int X, int Y)> keysLocations = GetKeysLocations(tunnelsMap);
            Dictionary<char, (int X, int Y)> doorsLocations = GetDoorsLocations(tunnelsMap);
            Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsFromKeyToKeys =
                GetStepsFromKeysToOtherKeys(tunnelsMap, entrance, keysLocations);
            Dictionary<char, bool> foundKeys = keysLocations.ToDictionary(kl => kl.Key, kl => false);

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                ENTRANCE,
                entrance,
                keysLocations,
                doorsLocations,
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
            tunnelsMap = UpdateMap(tunnelsMap);
            List<(int X, int Y)> robotsLocations = GetCharacterLocations(ENTRANCE, tunnelsMap);

            (int X, int Y) entrance = GetCharacterLocations(ENTRANCE, tunnelsMap).First();
            Dictionary<char, (int X, int Y)> keysLocations = GetKeysLocations(tunnelsMap);
            Dictionary<char, (int X, int Y)> doorsLocations = GetDoorsLocations(tunnelsMap);

            Dictionary<int, ((int X, int Y) CurrentLocation, Dictionary<char, bool> FoundKeys)> robots =
                new Dictionary<int, ((int X, int Y) CurrentLocation, Dictionary<char, bool> FoundKeys)>();

            for (int i = 0; i < robotsLocations.Count; i++)
            {
                robots[i] = (robotsLocations[i], keysLocations.ToDictionary(kl => kl.Key, kl => false));
            }

            int totalMinSteps = 0;
            while (totalMinSteps == 0)
            {
                for (int i = 0; i < robots.Count; i++)
                {
                    int minSteps = int.MaxValue;

                    Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsFromKeyToKeys =
                        GetStepsFromKeysToOtherKeys(tunnelsMap, robots[i].CurrentLocation, keysLocations);
                    Dictionary<char, bool> foundKeys = keysLocations.ToDictionary(kl => kl.Key, kl => false);

                    DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                        ENTRANCE,
                        robots[i].CurrentLocation,
                        keysLocations,
                        doorsLocations,
                        stepsFromKeyToKeys,
                        new Dictionary<string, int>(),
                        robots[i].FoundKeys,
                        0,
                        ref minSteps
                    );

                    if (!robots[i].FoundKeys.Where(fk => fk.Value == false).Any())
                    {
                        totalMinSteps += minSteps;
                    }
                }
            }

            return totalMinSteps;
        }

        private void DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
            char currentKey,
            (int X, int Y) currentLocation,
            Dictionary<char, (int X, int Y)> keysLocations,
            Dictionary<char, (int X, int Y)> doorsLocations,
            Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsToKey,
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
            string state = StringifyState(foundKeysString, currentLocation);
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && steps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = steps;

            // If all keys are collected in minimum number of steps
            if (steps < minSteps && !foundKeys.Where(fk => fk.Value == false).Any())
            {
                minSteps = steps;
                return;
            }

            Dictionary<char, int> reachableKeys = GetKeysReachableFromKey(stepsToKey[currentKey], foundKeys);

            foreach (KeyValuePair<char, int> reachableKey in reachableKeys)
            {
                char nextKey = reachableKey.Key;
                int newSteps = steps + reachableKey.Value;

                Dictionary<char, bool> nextFoundKeys = foundKeys.ToDictionary(fk => fk.Key, fk => fk.Value);
                nextFoundKeys[nextKey] = true;

                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                    nextKey,
                    keysLocations[nextKey],
                    keysLocations,
                    doorsLocations,
                    stepsToKey,
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
            (int X, int Y) currentLocation,
            Dictionary<char, (int X, int Y)> keysLocations
        )
        {
            Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>> stepsFromKeyToKeys =
                new Dictionary<char, Dictionary<char, (int Steps, string DoorsBetween)>>();

            Dictionary<char, (int Steps, string DoorsBetween)> reachedKeys =
                new Dictionary<char, (int Steps, string DoorsBetween)>();

            Queue<(int X, int Y)> nonProcessedPositions = new Queue<(int X, int Y)>();
            nonProcessedPositions.Enqueue(currentLocation);

            while (nonProcessedPositions.Count > 0)
            {
                (int X, int Y) location = nonProcessedPositions.Dequeue();
                char position = tunnelsMap[location.X, location.Y];

                GetStepsFromKeyToKeys(
                    tunnelsMap,
                    currentLocation,
                    0,
                    string.Empty,
                    new Dictionary<string,
                    int>(),
                    reachedKeys
                );

                reachedKeys.Remove(position);
                stepsFromKeyToKeys[position] = reachedKeys;

                foreach (char key in reachedKeys.Keys)
                {
                    if (!stepsFromKeyToKeys.ContainsKey(key))
                    {
                        nonProcessedPositions.Enqueue(GetCharacterLocations(key, tunnelsMap).First());
                    }
                }
            }

            return stepsFromKeyToKeys;
        }

        private void GetStepsFromKeyToKeys(
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
                reachableKeys[tunnelsMap[currentLocation.X, currentLocation.Y]] = (steps, doors.ToLower());
            }

            steps++;
            foreach ((int X, int Y) step in Tunnels.steps)
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

                    GetStepsFromKeyToKeys(
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

        private Dictionary<char, (int X, int Y)> GetKeysLocations(char[,] tunnelsMap)
        {
            Dictionary<char, (int X, int Y)> keysLocations = new Dictionary<char, (int X, int Y)>();

            string keys = GetKeys(tunnelsMap);
            foreach (char key in keys)
            {
                (int X, int Y) keyLocation = GetCharacterLocations(key, tunnelsMap).First();
                keysLocations[key] = keyLocation;
            }

            return keysLocations;
        }

        private Dictionary<char, (int X, int Y)> GetDoorsLocations(char[,] tunnelsMap)
        {
            Dictionary<char, (int X, int Y)> doorsLocations = new Dictionary<char, (int X, int Y)>();

            string doors = GetDoors(tunnelsMap).ToLower();
            foreach (char door in doors)
            {
                (int X, int Y) doorLocation = GetCharacterLocations(door, tunnelsMap).First();
                doorsLocations[door] = doorLocation;
            }

            return doorsLocations;
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
