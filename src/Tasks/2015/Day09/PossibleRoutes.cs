using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day9
{
    public class PossibleRoutes
    {
        public int CalculateDistanceOfTheShortestRoute(LocationsDistance[] distances)
        {
            int shortestRouteDistance = int.MaxValue;

            var possibleRoutes = GetPossibleRoutes(distances);
            foreach (var route in possibleRoutes)
            {
                if (route.Value < shortestRouteDistance)
                {
                    shortestRouteDistance = route.Value;
                }
            }

            return shortestRouteDistance;
        }

        public int CalculateDistanceOfTheLongestRoute(LocationsDistance[] distances)
        {
            int longestRouteDistance = 0;

            var possibleRoutes = GetPossibleRoutes(distances);
            foreach (var route in possibleRoutes)
            {
                if (route.Value > longestRouteDistance)
                {
                    longestRouteDistance = route.Value;
                }
            }

            return longestRouteDistance;
        }

        public Dictionary<string, int> GetPossibleRoutes(LocationsDistance[] distances)
        {
            Dictionary<string, int> possibleRoutes = new Dictionary<string, int>();

            Dictionary<string, List<LocationsDistance>> groupedLocationsDistances = GroupByStartLocation(distances);

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

        private Dictionary<string, List<LocationsDistance>> GroupByStartLocation(LocationsDistance[] distances)
        {
            Dictionary<string, List<LocationsDistance>> groupedLocationsDistances =
                new Dictionary<string, List<LocationsDistance>>();

            foreach (LocationsDistance distance in distances)
            {
                // Each location can be starting and ending
                string[][] locationsCombinations = new string[2][]{
                    new string[2] {distance.StartLocation, distance.EndLocation},
                    new string[2] {distance.EndLocation, distance.StartLocation}
                };

                // Iterating through both combinations
                for (int i = 0; i < locationsCombinations.Length; i++)
                {
                    var locationsCombination = locationsCombinations[i];

                    LocationsDistance directedDistance = new LocationsDistance
                    {
                        StartLocation = locationsCombination[0],
                        EndLocation = locationsCombination[1],
                        Distance = distance.Distance
                    };

                    // If distances starting from this location still don't exist
                    if (!groupedLocationsDistances.ContainsKey(locationsCombination[0]))
                    {
                        List<LocationsDistance> distancesStartingFromLocation = new List<LocationsDistance>();
                        groupedLocationsDistances.Add(locationsCombination[0], distancesStartingFromLocation);
                    }

                    groupedLocationsDistances[locationsCombination[0]].Add(directedDistance);
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
