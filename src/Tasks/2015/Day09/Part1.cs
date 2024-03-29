/*
--- Day 9: All in a Single Night ---

Every year, Santa manages to deliver all of his presents in a single night.

This year, however, he has some new locations to visit; his elves have provided
him the distances between every pair of locations. He can start and end at any
two (different) locations he wants, but he must visit each location exactly
once. What is the shortest distance he can travel to achieve this?

For example, given the following distances:

London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141

The possible routes are therefore:

Dublin -> London -> Belfast = 982
London -> Dublin -> Belfast = 605
London -> Belfast -> Dublin = 659
Dublin -> Belfast -> London = 659
Belfast -> Dublin -> London = 605
Belfast -> London -> Dublin = 982

The shortest of these is London -> Dublin -> Belfast = 605, and so the answer is
605 in this example.

What is the distance of the shortest route?
*/

namespace App.Tasks.Year2015.Day9
{
    public class Part1 : ITask<int>
    {
        private readonly DistancesRepository distancesRepository;

        private readonly PossibleRoutes possibleRoutes;

        public Part1()
        {
            distancesRepository = new DistancesRepository();
            possibleRoutes = new PossibleRoutes();
        }

        public int Solution(string input)
        {
            LocationsDistance[] distances = distancesRepository.GetDistances(input);
            int shortestRouteDistance = possibleRoutes.CalculateDistanceOfTheShortestRoute(distances);

            return shortestRouteDistance;
        }
    }
}
