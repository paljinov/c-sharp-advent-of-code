using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day9
{
    class PossibleRoutes
    {
        public Dictionary<string, int> GetPossibleRoutes(string input)
        {
            Dictionary<string, int> possibleRoutes = new Dictionary<string, int>();

            string[] distancesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, List<LocationsDistance>> groupedLocationsDistances =
                GroupByStartLocation(distancesString);

            foreach (var locationsDistances in groupedLocationsDistances)
            {
                possibleRoutes.Add(locationsDistances.Key, 0);
                CalculatePossibleRoutesStartingFromLocation(
                    locationsDistances.Key,
                    groupedLocationsDistances,
                    possibleRoutes
                );

            }

            return possibleRoutes;
        }

        private Dictionary<string, List<LocationsDistance>> GroupByStartLocation(string[] distancesString)
        {
            Dictionary<string, List<LocationsDistance>> groupedLocationsDistances =
                new Dictionary<string, List<LocationsDistance>>();

            Regex distanceRegex = new Regex(@"^(\w+)\sto\s(\w+)\s\=\s(\d+)$");

            foreach (var distanceString in distancesString)
            {
                Match distanceMatches = distanceRegex.Match(distanceString);
                GroupCollection groups = distanceMatches.Groups;

                // Each location can be starting and ending
                string[][] locationsCombinations = new string[2][]{
                    new string[2] {groups[1].Value, groups[2].Value},
                    new string[2] {groups[2].Value, groups[1].Value}
                };

                // Iterating through both combinations
                for (int i = 0; i < locationsCombinations.Length; i++)
                {
                    var locationsCombination = locationsCombinations[i];

                    LocationsDistance distance = new LocationsDistance
                    {
                        StartLocation = locationsCombination[0],
                        EndLocation = locationsCombination[1],
                        Distance = int.Parse(groups[3].Value)
                    };

                    // If distances starting from this location still don't exist
                    if (!groupedLocationsDistances.ContainsKey(locationsCombination[0]))
                    {
                        List<LocationsDistance> distancesStartingFromLocation = new List<LocationsDistance>();
                        groupedLocationsDistances.Add(locationsCombination[0], distancesStartingFromLocation);
                    }

                    groupedLocationsDistances[locationsCombination[0]].Add(distance);
                }
            }

            return groupedLocationsDistances;
        }

        /// <summary>
        /// Recursively finds all routes combinations from current route (location).
        /// Result is stored as possible routes.
        /// </summary>
        /// <param name="currentRoute"></param>
        /// <param name="groupedLocationsDistances"></param>
        /// <param name="possibleRoutes"></param>
        private void CalculatePossibleRoutesStartingFromLocation(
            string currentRoute,
            Dictionary<string, List<LocationsDistance>> groupedLocationsDistances,
            Dictionary<string, int> possibleRoutes
        )
        {
            string currentLocation = currentRoute.Split("->").Last();
            int currentDistance = possibleRoutes[currentRoute];

            // If route can be continued
            if (groupedLocationsDistances.ContainsKey(currentLocation))
            {
                // Iterating possible routes from current location
                foreach (LocationsDistance locationsDinstance in groupedLocationsDistances[currentLocation])
                {
                    // If route doesn't already contain next location (location can't repeat in route)
                    if (!currentRoute.Contains(locationsDinstance.EndLocation))
                    {
                        string extendedRoute = $"{currentRoute}->{locationsDinstance.EndLocation}";
                        int extendedDistance = currentDistance + locationsDinstance.Distance;

                        // Remove current route which didn't come to the end location
                        possibleRoutes.Remove(currentRoute);
                        // Add new extended route
                        possibleRoutes.Add(extendedRoute, extendedDistance);

                        // Try to calculate new extended route from last location
                        CalculatePossibleRoutesStartingFromLocation(
                            extendedRoute,
                            groupedLocationsDistances,
                            possibleRoutes
                        );
                    }
                }
            }
        }
    }
}
