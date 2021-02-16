/*
--- Part Two ---

You step into the cleanroom separating the lobby from the isolated area and put
on the hazmat suit.

Upon entering the isolated containment area, however, you notice some extra
parts on the first floor that weren't listed on the record outside:

- An elerium generator.
- An elerium-compatible microchip.
- A dilithium generator.
- A dilithium-compatible microchip.

These work just like the other generators and microchips. You'll have to get
them up to assembly as well.

What is the minimum number of steps required to bring all of the objects,
including these four new ones, to the fourth floor?
*/

using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2016.Day11
{
    public class Part2 : ITask<int>
    {
        private readonly string[] additionalPairsOnMinFloor = new string[] { "elerium", "dilithium" };

        private readonly FloorsArrangementRepository floorsArrangementRepository;

        private readonly Elevator elevator;

        public Part2()
        {
            floorsArrangementRepository = new FloorsArrangementRepository();
            elevator = new Elevator();
        }

        public int Solution(string input)
        {
            Dictionary<int, FloorObjects> floorsObjectsArrangement =
                floorsArrangementRepository.GetObjectsArrangementByFloors(input);

            int minFloor = floorsObjectsArrangement.Keys.Min();
            foreach (string pair in additionalPairsOnMinFloor)
            {
                floorsObjectsArrangement[minFloor].Microchips.Add(pair);
                floorsObjectsArrangement[minFloor].Generators.Add(pair);
            }

            int minimumNumberOfSteps =
                elevator.CalculateMinimumNumberOfStepsToBringAllObjectsToLastFloor(floorsObjectsArrangement);

            return minimumNumberOfSteps;
        }
    }
}
