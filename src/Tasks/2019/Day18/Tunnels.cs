using System.Collections.Generic;

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

            DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                tunnelsMap, (entrance.Value.X, entrance.Value.Y), visitedLocations, 0, ref minSteps);

            return minSteps;
        }

        private void DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
            char[,] tunnelsMap,
            (int X, int Y) currentLocation,
             Dictionary<(int X, int Y), int> visitedLocations,
            int steps,
            ref int minSteps
        )
        {
            // If better solution is already found
            if (steps >= minSteps)
            {
                return;
            }

            // If all keys are  collected
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
                if (char.IsLower(tunnelsMap[nextLocation.X, nextLocation.Y]))
                {
                    (int X, int Y)? door = GetCharacterLocation(
                        char.ToUpper(tunnelsMap[nextLocation.X, nextLocation.Y]), tunnelsMapCopy);

                    // Take key
                    tunnelsMapCopy[nextLocation.X, nextLocation.Y] = OPEN_PASSAGE;
                    if (door.HasValue)
                    {
                        // Unlock door
                        tunnelsMapCopy[door.Value.X, door.Value.Y] = OPEN_PASSAGE;
                    }

                    visitedLocationsCopy.Clear();
                }

                DoCountStepsOfShortestPathThatCollectsAllOfTheKeys(
                    tunnelsMapCopy, nextLocation, visitedLocationsCopy, steps, ref minSteps);
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
    }
}
