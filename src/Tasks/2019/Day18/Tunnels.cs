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

            Dictionary<string, int> statesCache = new Dictionary<string, int>();
            string tunnelMapState = StringifyTunnelMapState(tunnelsMap);

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                tunnelsMap, (entrance.X, entrance.Y), statesCache, tunnelMapState, 0, ref minSteps);

            return minSteps;
        }

        public int CountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(char[,] tunnelsMap)
        {
            int minSteps = int.MaxValue;

            tunnelsMap = UpdateMap(tunnelsMap);
            (int X, int Y)[] robotsLocations = GetCharacterLocations(ENTRANCE, tunnelsMap).ToArray();

            Dictionary<string, int> statesCache = new Dictionary<string, int>();
            string tunnelMapState = StringifyTunnelMapState(tunnelsMap);

            DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
                tunnelsMap, robotsLocations, statesCache, tunnelMapState, 0, ref minSteps);

            return minSteps;
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

            List<(int X, int Y)> nextLocations = GetNextStepLocations(tunnelsMap, currentLocation);

            steps++;
            foreach ((int X, int Y) nextLocation in nextLocations)
            {
                // If key is found
                if (char.IsLower(tunnelsMap[nextLocation.X, nextLocation.Y]))
                {
                    char[,] tunnelsMapCopy = tunnelsMap.Clone() as char[,];
                    string tunnelMapStateCopy = new string(tunnelMapState);

                    List<(int X, int Y)> doors = GetCharacterLocations(
                        char.ToUpper(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), tunnelsMapCopy);

                    // Take key
                    tunnelMapStateCopy = tunnelMapStateCopy.Remove(
                        tunnelMapStateCopy.IndexOf(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), 1);
                    tunnelsMapCopy[nextLocation.X, nextLocation.Y] = OPEN_PASSAGE;
                    if (doors.Count > 0)
                    {
                        (int X, int Y) door = doors.First();

                        // Unlock door
                        tunnelMapStateCopy = tunnelMapStateCopy.Remove(
                            tunnelMapStateCopy.IndexOf(tunnelsMapCopy[door.X, door.Y]), 1);
                        tunnelsMapCopy[door.X, door.Y] = OPEN_PASSAGE;
                    }

                    DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                        tunnelsMapCopy, nextLocation, statesCache, tunnelMapStateCopy, steps, ref minSteps);
                }
                // If next location is open passage
                else
                {
                    DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                        tunnelsMap, nextLocation, statesCache, tunnelMapState, steps, ref minSteps);
                }
            }
        }

        private void DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
            char[,] tunnelsMap,
            (int X, int Y)[] robotsLocations,
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

            string state = StringifyStateForRemoteControlledRobots(tunnelMapState, robotsLocations);
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

            steps++;
            // Iterate by each robot
            for (int i = 0; i < robotsLocations.Length; i++)
            {
                (int X, int Y) currentLocation = robotsLocations[i];
                List<(int X, int Y)> nextLocations = GetNextStepLocations(tunnelsMap, currentLocation);

                foreach ((int X, int Y) nextLocation in nextLocations)
                {
                    robotsLocations[i] = (nextLocation.X, nextLocation.Y);

                    // If key is found
                    if (char.IsLower(tunnelsMap[nextLocation.X, nextLocation.Y]))
                    {
                        char[,] tunnelsMapCopy = tunnelsMap.Clone() as char[,];
                        string tunnelMapStateCopy = new string(tunnelMapState);

                        List<(int X, int Y)> doors = GetCharacterLocations(
                            char.ToUpper(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), tunnelsMapCopy);

                        // Take key
                        tunnelMapStateCopy = tunnelMapStateCopy.Remove(
                            tunnelMapStateCopy.IndexOf(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), 1);
                        tunnelsMapCopy[nextLocation.X, nextLocation.Y] = OPEN_PASSAGE;
                        if (doors.Count > 0)
                        {
                            (int X, int Y) door = doors.First();

                            // Unlock door
                            tunnelMapStateCopy = tunnelMapStateCopy.Remove(
                                tunnelMapStateCopy.IndexOf(tunnelsMapCopy[door.X, door.Y]), 1);
                            tunnelsMapCopy[door.X, door.Y] = OPEN_PASSAGE;
                        }

                        DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
                            tunnelsMapCopy, robotsLocations, statesCache, tunnelMapStateCopy, steps, ref minSteps);
                    }
                    // If next location is open passage
                    else
                    {
                        DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
                            tunnelsMap, robotsLocations, statesCache, tunnelMapState, steps, ref minSteps);
                    }
                }

                robotsLocations[i] = currentLocation;
            }
        }

        private List<(int X, int Y)> GetNextStepLocations(char[,] tunnelsMap, (int X, int Y) currentLocation)
        {
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

            return nextLocations;
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

        private string StringifyTunnelMapState(char[,] tunnelsMap)
        {
            StringBuilder tunnelMapState = new StringBuilder();

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (char.IsLetter(tunnelsMap[i, j]))
                    {
                        tunnelMapState.Append(tunnelsMap[i, j]);
                    }
                }
            }

            return tunnelMapState.ToString();
        }

        private string StringifyState(string tunnelMapState, (int X, int Y) currentLocation)
        {
            return $"({tunnelMapState}),({currentLocation.X},{currentLocation.Y})";
        }

        private string StringifyStateForRemoteControlledRobots(
            string tunnelMapState,
            (int X, int Y)[] robotsLocations
       )
        {
            StringBuilder state = new StringBuilder($"({tunnelMapState})");
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
