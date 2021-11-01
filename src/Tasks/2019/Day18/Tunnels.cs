using System.Collections.Generic;
using System.Text;

namespace App.Tasks.Year2019.Day18
{
    public class Tunnels
    {
        private const char ENTRANCE = '@';

        private const char OPEN_PASSAGE = '.';

        private const char STONE_WALL = '#';

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
            (int X, int Y)? entrance = GetCharacterLocation(ENTRANCE, tunnelsMap);
            tunnelsMap[entrance.Value.X, entrance.Value.Y] = OPEN_PASSAGE;

            Dictionary<string, int> statesCache = new Dictionary<string, int>();
            string tunnelMapState = StringifyTunnelMapState(tunnelsMap);

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(tunnelsMap, (entrance.Value.X, entrance.Value.Y),
                statesCache, tunnelMapState, 0, ref minSteps);

            return minSteps;
        }

        public int CountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(char[,] tunnelsMap)
        {
            tunnelsMap = UpdateMap(tunnelsMap);
            return tunnelsMap.Length;
        }

        private void DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
            char[,] tunnelsMap,
            (int X, int Y) currentLocation,
            Dictionary<string, int> statesCache,
            string tunnelMapState,
            int steps,
            ref int minSteps
        )
        {
            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            string state = StringifyState(tunnelMapState, currentLocation);
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && steps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = steps;

            // If all keys are collected
            if (string.IsNullOrEmpty(tunnelMapState))
            {
                minSteps = steps;
                return;
            }

            List<(int X, int Y)> nextLocations = new List<(int X, int Y)>();

            if (currentLocation.X - 1 >= 0
                && tunnelsMap[currentLocation.X - 1, currentLocation.Y] != STONE_WALL
                && !char.IsUpper(tunnelsMap[currentLocation.X - 1, currentLocation.Y]))
            {
                nextLocations.Add((currentLocation.X - 1, currentLocation.Y));
            }

            if (currentLocation.X + 1 < tunnelsMap.GetLength(0)
                && tunnelsMap[currentLocation.X + 1, currentLocation.Y] != STONE_WALL
                && !char.IsUpper(tunnelsMap[currentLocation.X + 1, currentLocation.Y]))
            {
                nextLocations.Add((currentLocation.X + 1, currentLocation.Y));
            }

            if (currentLocation.Y - 1 >= 0
                && tunnelsMap[currentLocation.X, currentLocation.Y - 1] != STONE_WALL
                && !char.IsUpper(tunnelsMap[currentLocation.X, currentLocation.Y - 1]))
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y - 1));
            }

            if (currentLocation.Y + 1 < tunnelsMap.GetLength(1)
                && tunnelsMap[currentLocation.X, currentLocation.Y + 1] != STONE_WALL
                && !char.IsUpper(tunnelsMap[currentLocation.X, currentLocation.Y + 1]))
            {
                nextLocations.Add((currentLocation.X, currentLocation.Y + 1));
            }

            steps++;
            foreach ((int X, int Y) nextLocation in nextLocations)
            {
                // If key is found
                if (char.IsLower(tunnelsMap[nextLocation.X, nextLocation.Y]))
                {
                    char[,] tunnelsMapCopy = tunnelsMap.Clone() as char[,];
                    string tunnelMapStateCopy = new string(tunnelMapState);

                    (int X, int Y)? door = GetCharacterLocation(
                        char.ToUpper(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), tunnelsMapCopy);

                    // Take key
                    tunnelMapStateCopy = tunnelMapStateCopy.Remove(
                        tunnelMapStateCopy.IndexOf(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), 1);
                    tunnelsMapCopy[nextLocation.X, nextLocation.Y] = OPEN_PASSAGE;
                    if (door.HasValue)
                    {
                        // Unlock door
                        tunnelMapStateCopy = tunnelMapStateCopy.Remove(
                            tunnelMapStateCopy.IndexOf(tunnelsMapCopy[door.Value.X, door.Value.Y]), 1);
                        tunnelsMapCopy[door.Value.X, door.Value.Y] = OPEN_PASSAGE;
                    }

                    DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(tunnelsMapCopy, nextLocation,
                        statesCache, tunnelMapStateCopy, steps, ref minSteps);
                }
                // If next location is open passage
                else
                {
                    DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(tunnelsMap, nextLocation,
                        statesCache, tunnelMapState, steps, ref minSteps);
                }
            }
        }

        private (int X, int Y)? GetCharacterLocation(char entrance, char[,] tunnelsMap)
        {
            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (tunnelsMap[i, j] == entrance)
                    {
                        return (i, j);
                    }
                }
            }

            return null;
        }

        private string StringifyTunnelMapState(char[,] tunnelsMap)
        {
            StringBuilder state = new StringBuilder();

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (char.IsLetter(tunnelsMap[i, j]))
                    {
                        state.Append(tunnelsMap[i, j]);
                    }
                }
            }

            return state.ToString();
        }

        private string StringifyState(string tunnelMapState, (int X, int Y) currentLocation)
        {
            return $"({tunnelMapState}),({currentLocation.X},{currentLocation.Y})";
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
