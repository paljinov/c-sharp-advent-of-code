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
            Dictionary<string, int> cycles = new Dictionary<string, int>();

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                tunnelsMap, (entrance.Value.X, entrance.Value.Y), visitedLocations, 0, ref minSteps, cycles);

            return minSteps;
        }

        private void DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
            char[,] tunnelsMap,
            (int X, int Y) currentLocation,
            Dictionary<(int X, int Y), int> visitedLocations,
            int steps,
            ref int minSteps,
            Dictionary<string, int> cycles
        )
        {
            string tunnelsMapString = StringifyTunnelsMap(tunnelsMap);
            // If this solution already exists
            if (cycles.ContainsKey(tunnelsMapString))
            {
                if (cycles[tunnelsMapString] > 100)
                {
                    return;
                }

                cycles[tunnelsMapString]++;
            }
            else
            {
                cycles[tunnelsMapString] = 1;
            }

            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            // If all keys are collected
            if (AreAllKeysCollected(tunnelsMap))
            {
                minSteps = steps;
                return;
            }

            // If location is already visited in equal number or less steps
            if (visitedLocations.ContainsKey((currentLocation.X, currentLocation.Y))
                && steps >= visitedLocations[(currentLocation.X, currentLocation.Y)])
            {
                return;
            }

            visitedLocations[(currentLocation.X, currentLocation.Y)] = steps;

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

                // If key is found
                if (char.IsLower(tunnelsMapCopy[nextLocation.X, nextLocation.Y]))
                {
                    (int X, int Y)? door = GetCharacterLocation(
                        char.ToUpper(tunnelsMapCopy[nextLocation.X, nextLocation.Y]), tunnelsMapCopy);

                    // Take key
                    tunnelsMapCopy[nextLocation.X, nextLocation.Y] = OPEN_PASSAGE;
                    if (door.HasValue)
                    {
                        // Unlock door
                        tunnelsMapCopy[door.Value.X, door.Value.Y] = OPEN_PASSAGE;
                    }

                    // Clean visited locations because it possible to go back through opened doors
                    visitedLocationsCopy.Clear();
                }

                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                    tunnelsMapCopy, nextLocation, visitedLocationsCopy, steps, ref minSteps, cycles);
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

        private string StringifyTunnelsMap(char[,] tunnelsMap)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tunnelsMap.GetLength(0); i++)
            {
                for (int j = 0; j < tunnelsMap.GetLength(1); j++)
                {
                    sb.Append(tunnelsMap[i, j]);
                }
            }

            return sb.ToString();
        }
    }
}
