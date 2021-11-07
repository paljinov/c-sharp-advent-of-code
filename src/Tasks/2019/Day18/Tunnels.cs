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
            string keys = GetKeys(tunnelsMap);

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                tunnelsMap, (entrance.X, entrance.Y), null, statesCache, keys, 0, ref minSteps);

            return minSteps;
        }

        public int CountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(char[,] tunnelsMap)
        {
            int minSteps = int.MaxValue;

            tunnelsMap = UpdateMap(tunnelsMap);

            char[][,] quadrantTunnelsMaps = new char[4][,];
            char[,] quadrantMap = new char[tunnelsMap.GetLength(0) / 2, tunnelsMap.GetLength(1) / 2];

            int rowMid = tunnelsMap.GetLength(0) / 2;
            int columnMid = tunnelsMap.GetLength(1) / 2;

            int k = 0;
            int h = 0;
            for (int i = 0; i < rowMid; i++)
            {
                for (int j = 0; j < columnMid; j++)
                {
                    quadrantMap[k, h] = tunnelsMap[i, j];
                    h++;
                }
                h = 0;
                k++;

                quadrantTunnelsMaps[0] = quadrantMap;
            }

            k = 0;
            h = 0;
            for (int i = 0; i < rowMid; i++)
            {
                for (int j = columnMid + 1; j < tunnelsMap.GetLength(1); j++)
                {
                    quadrantMap[k, h] = tunnelsMap[i, j];
                    h++;
                }
                h = 0;
                k++;

                quadrantTunnelsMaps[1] = quadrantMap;
            }

            k = 0;
            h = 0;
            for (int i = rowMid + 1; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < columnMid; j++)
                {
                    quadrantMap[k, h] = tunnelsMap[i, j];
                    h++;
                }
                h = 0;
                k++;

                quadrantTunnelsMaps[2] = quadrantMap;
            }

            k = 0;
            h = 0;
            for (int i = rowMid + 1; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = columnMid + 1; j < tunnelsMap.GetLength(1); j++)
                {
                    quadrantMap[k, h] = tunnelsMap[i, j];
                    h++;
                }
                h = 0;
                k++;

                quadrantTunnelsMaps[3] = quadrantMap;
            }

            int[] quadrantMinSteps = new int[4];
            int quadrant = 0;
            foreach (char[,] quadrantTunnelsMap in quadrantTunnelsMaps)
            {
                for (int i = 0; i < quadrantTunnelsMap.GetLength(0); i++)
                {
                    for (int j = 0; j < quadrantTunnelsMap.GetLength(0); j++)
                    {
                        if (char.IsUpper(quadrantTunnelsMap[i, j]))
                        {
                            List<(int X, int Y)> foundKeys = GetCharacterLocations(
                                char.ToLower(quadrantTunnelsMap[i, j]), quadrantTunnelsMap);

                            if (foundKeys.Count == 0)
                            {
                                quadrantTunnelsMap[i, j] = OPEN_PASSAGE;
                            }
                        }
                    }
                }

                quadrantMinSteps[quadrant] = int.MaxValue;
                (int X, int Y) entrance = GetCharacterLocations(ENTRANCE, quadrantTunnelsMap).First();
                tunnelsMap[entrance.X, entrance.Y] = OPEN_PASSAGE;

                Dictionary<string, int> statesCache = new Dictionary<string, int>();
                string keys = GetKeys(quadrantTunnelsMap);

                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                    quadrantTunnelsMap, (entrance.X, entrance.Y), null, statesCache, keys, 0, ref quadrantMinSteps[quadrant]);

                minSteps += quadrantMinSteps[quadrant];
                quadrant++;
            }

            return minSteps;
        }

        private void DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
            char[,] tunnelsMap,
            (int X, int Y) currentLocation,
            Direction? currentDirection,
            Dictionary<string, int> statesCache,
            string remainingKeys,
            int steps,
            ref int minSteps
        )
        {
            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            string state = StringifyState(remainingKeys, currentLocation);
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && steps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = steps;

            // If all keys are collected
            if (string.IsNullOrEmpty(remainingKeys))
            {
                minSteps = steps;
                return;
            }

            Dictionary<(int X, int Y), Direction> nextLocations =
                   GetNextStepLocations(tunnelsMap, currentLocation, currentDirection);

            steps++;
            foreach (KeyValuePair<(int X, int Y), Direction> nextLocation in nextLocations)
            {
                (char[,] TunnelsMap, string RemainingKeys, Direction? Direction) nextStepTunnelMap =
                    GetNextStepTunnelMap(tunnelsMap, nextLocation.Key, nextLocation.Value, remainingKeys);

                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                    nextStepTunnelMap.TunnelsMap,
                    nextLocation.Key,
                    nextStepTunnelMap.Direction,
                    statesCache,
                    nextStepTunnelMap.RemainingKeys,
                    steps,
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
            ref int minSteps
        )
        {
            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            string state = StringifyStateForRemoteControlledRobots(remainingKeys, robotsLocations);
            // If this tunnel map state and current position already exists in equal or less number of steps
            if (statesCache.ContainsKey(state) && steps >= statesCache[state])
            {
                return;
            }
            statesCache[state] = steps;

            // If all keys are collected
            if (string.IsNullOrEmpty(remainingKeys))
            {
                minSteps = steps;
                return;
            }

            steps++;
            // Iterate by each robot
            for (int i = 0; i < robotsLocations.Length; i++)
            {
                (int X, int Y) currentLocation = robotsLocations[i];
                Direction? currentDirection = robotsDirections[i];

                Dictionary<(int X, int Y), Direction> nextLocations =
                    GetNextStepLocations(tunnelsMap, currentLocation, currentDirection);

                foreach (KeyValuePair<(int X, int Y), Direction> nextLocation in nextLocations)
                {
                    (char[,] TunnelsMap, string RemainingKeys, Direction? Direction) nextStepTunnelMap =
                        GetNextStepTunnelMap(tunnelsMap, nextLocation.Key, nextLocation.Value, remainingKeys);

                    robotsLocations[i] = (nextLocation.Key.X, nextLocation.Key.Y);
                    robotsDirections[i] = nextStepTunnelMap.Direction;

                    DoCountFewestStepsNecessaryToCollectAllOfTheKeysForRemoteControlledRobots(
                        nextStepTunnelMap.TunnelsMap,
                        robotsLocations,
                        robotsDirections,
                        statesCache,
                        nextStepTunnelMap.RemainingKeys,
                        steps,
                        ref minSteps
                    );
                }

                robotsLocations[i] = currentLocation;
                robotsDirections[i] = currentDirection;
            }
        }

        /// <summary>
        ///  Get next step locations.
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

        private string StringifyState(string remainingKeys, (int X, int Y) currentLocation)
        {
            return $"({remainingKeys}),({currentLocation.X},{currentLocation.Y})";
        }

        private string StringifyStateForRemoteControlledRobots(
            string remainingKeys,
            (int X, int Y)[] robotsLocations
       )
        {
            StringBuilder state = new StringBuilder($"({remainingKeys})");
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
