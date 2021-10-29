using System.Collections.Generic;
using System.Text;

namespace App.Tasks.Year2019.Day18
{
    public class Tunnels
    {
        private const char ENTRANCE = '@';

        private const char OPEN_PASSAGE = '.';

        private const char STONE_WALL = '#';

        public int CountStepsOfShortestPathThatCollectsAllOfTheKeys(char[,] tunnelsMap)
        {
            int minSteps = int.MaxValue;
            (int X, int Y)? entrance = GetCharacterLocation(ENTRANCE, tunnelsMap);
            tunnelsMap[entrance.Value.X, entrance.Value.Y] = OPEN_PASSAGE;

            Dictionary<(int X, int Y), int> visitedLocations = new Dictionary<(int X, int Y), int>();
            HashSet<string> statesCache = new HashSet<string>();
            string tunnelMapState = StringifyTunnelMapState(tunnelsMap);

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(tunnelsMap, (entrance.Value.X, entrance.Value.Y),
                visitedLocations, statesCache, tunnelMapState, 0, ref minSteps);

            return minSteps;
        }

        public int CountFewestStepsNecessaryToCollectAllOfTheKeysForUpdatedMap(char[,] tunnelsMap)
        {
            return tunnelsMap.Length;
        }

        private void DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
            char[,] tunnelsMap,
            (int X, int Y) currentLocation,
            Dictionary<(int X, int Y), int> visitedLocations,
            HashSet<string> statesCache,
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

            // If location is already visited in equal number or less steps
            if (visitedLocations.ContainsKey((currentLocation.X, currentLocation.Y))
                && steps >= visitedLocations[(currentLocation.X, currentLocation.Y)])
            {
                return;
            }
            visitedLocations[(currentLocation.X, currentLocation.Y)] = steps;

            // If state repeats
            string state = StringifyState(tunnelMapState, currentLocation, steps);
            // If this state already exists
            if (statesCache.Contains(state))
            {
                return;
            }
            statesCache.Add(state);

            // If all keys are collected
            if (AreAllKeysCollected(tunnelsMap))
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
                char[,] tunnelsMapCopy = tunnelsMap.Clone() as char[,];
                Dictionary<(int X, int Y), int> visitedLocationsCopy =
                    new Dictionary<(int X, int Y), int>(visitedLocations);
                string tunnelMapStateCopy = new string(tunnelMapState);

                // If key is found
                if (char.IsLower(tunnelsMapCopy[nextLocation.X, nextLocation.Y]))
                {
                    (int X, int Y)? door = GetCharacterLocation(
                        char.ToUpper(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), tunnelsMapCopy);

                    // Take key
                    tunnelMapStateCopy = tunnelMapStateCopy.Replace(
                        tunnelsMapCopy[nextLocation.X, nextLocation.Y].ToString(), string.Empty);
                    tunnelsMapCopy[nextLocation.X, nextLocation.Y] = OPEN_PASSAGE;
                    if (door.HasValue)
                    {
                        // Unlock door
                        tunnelMapStateCopy = tunnelMapStateCopy.Replace(
                            tunnelsMapCopy[door.Value.X, door.Value.Y].ToString(), string.Empty);
                        tunnelsMapCopy[door.Value.X, door.Value.Y] = OPEN_PASSAGE;
                    }

                    // Clean visited locations because it possible to go back through opened doors
                    visitedLocationsCopy.Clear();
                }

                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(tunnelsMapCopy, nextLocation, visitedLocationsCopy,
                    statesCache, tunnelMapStateCopy, steps, ref minSteps);
            }
        }

        private bool AreAllKeysCollected(char[,] tunnelsMap)
        {
            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (char.IsLower(tunnelsMap[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
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
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    if (char.IsLetter(tunnelsMap[i, j]))
                    {
                        sb.Append(tunnelsMap[i, j]);
                    }
                }
            }

            return sb.ToString();
        }

        private string StringifyState(string tunnelMapState, (int X, int Y) currentLocation, int steps)
        {
            return $"({tunnelMapState}),({currentLocation.X},{currentLocation.Y}),({steps})";
        }
    }
}
