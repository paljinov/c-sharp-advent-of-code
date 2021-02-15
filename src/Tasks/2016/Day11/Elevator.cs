using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2016.Day11
{
    public class Elevator
    {
        private Dictionary<string, int> floorsObjectsArrangementStepsCache = new Dictionary<string, int>();

        public int CalculateMinimumNumberOfStepsToBringAllObjectsToLastFloor(
            Dictionary<int, FloorObjects> floorsObjectsArrangement
        )
        {
            int minFloor = floorsObjectsArrangement.Keys.Min();
            int minSteps = int.MaxValue;

            floorsObjectsArrangementStepsCache = new Dictionary<string, int>();
            MoveObjectsToTopFloor(floorsObjectsArrangement, minFloor, 0, ref minSteps);

            return minSteps;
        }

        private void MoveObjectsToTopFloor(
            Dictionary<int, FloorObjects> floorsObjectsArrangement,
            int elevatorFloor,
            int steps,
            ref int minSteps
        )
        {
            if (steps >= minSteps)
            {
                return;
            }

            if (AreAllObjectsOnLastFloor(floorsObjectsArrangement))
            {
                minSteps = Math.Min(minSteps, steps);
                return;
            }

            string floorsObjectsArrangementCacheKey = FloorsObjectsArrangementCacheKey(floorsObjectsArrangement);
            if (floorsObjectsArrangementStepsCache.ContainsKey(floorsObjectsArrangementCacheKey)
                && steps >= floorsObjectsArrangementStepsCache[floorsObjectsArrangementCacheKey])
            {
                return;
            }
            floorsObjectsArrangementStepsCache[floorsObjectsArrangementCacheKey] = steps;

            // Increase number of steps
            steps++;

            bool twoObjectsUp = false;
            // Move two objects up
            MoveTwoObjects(floorsObjectsArrangement, elevatorFloor, steps, ref minSteps, true, ref twoObjectsUp);

            bool oneObjectDown = false;
            // Move one object down
            MoveOneObject(floorsObjectsArrangement, elevatorFloor, steps, ref minSteps, false, ref oneObjectDown);

            // Move two objects down only if it was not possible to move one object down
            if (!oneObjectDown)
            {
                MoveTwoObjects(floorsObjectsArrangement, elevatorFloor, steps, ref minSteps, false, ref twoObjectsUp);
            }

            // Move one object up only if it was not possible to move two objects up
            if (!twoObjectsUp)
            {
                MoveOneObject(floorsObjectsArrangement, elevatorFloor, steps, ref minSteps, true, ref oneObjectDown);
            }
        }


        private bool IsAnyChipFried(Dictionary<int, FloorObjects> floorsObjectsArrangement)
        {
            foreach (KeyValuePair<int, FloorObjects> floorObjectsArrangement in floorsObjectsArrangement)
            {
                List<string> microchips = floorObjectsArrangement.Value.Microchips;
                List<string> generators = floorObjectsArrangement.Value.Generators;

                if (microchips.Count > 0 && generators.Count > 0)
                {
                    IEnumerable<string> unpairedMicrochips = microchips.Except(generators);
                    if (unpairedMicrochips.Any())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool AreAllObjectsOnLastFloor(Dictionary<int, FloorObjects> floorsObjectsArrangement)
        {
            int minFloor = floorsObjectsArrangement.Keys.Min();
            int maxFloor = floorsObjectsArrangement.Keys.Max();

            for (int floor = minFloor; floor < maxFloor; floor++)
            {
                List<string> microchips = floorsObjectsArrangement[floor].Microchips;
                List<string> generators = floorsObjectsArrangement[floor].Generators;

                if (microchips.Count > 0 || generators.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void MoveOneObject(
            Dictionary<int, FloorObjects> floorsObjectsArrangement,
            int elevatorFloor,
            int steps,
            ref int minSteps,
            bool up,
            ref bool oneObjectDown
        )
        {
            int minFloor = floorsObjectsArrangement.Keys.Min();
            int maxFloor = floorsObjectsArrangement.Keys.Max();
            int nextFloor = up ? elevatorFloor + 1 : elevatorFloor - 1;
            if (nextFloor > maxFloor || nextFloor < minFloor)
            {
                return;
            }

            IEnumerable<MoveOneObjectCombination> combinations =
                Enum.GetValues(typeof(MoveOneObjectCombination)).Cast<MoveOneObjectCombination>();

            foreach (MoveOneObjectCombination combination in combinations)
            {
                List<string> objects;

                switch (combination)
                {
                    case MoveOneObjectCombination.OneMicrochip:
                        objects = floorsObjectsArrangement[elevatorFloor].Microchips;
                        break;
                    default:
                        objects = floorsObjectsArrangement[elevatorFloor].Generators;
                        break;
                }

                for (int i = 0; i < objects.Count; i++)
                {
                    string movedObject;
                    Dictionary<int, FloorObjects> floorsObjectsArrangementAfterMove =
                        CloneFloorsObjectsArrangement(floorsObjectsArrangement);

                    switch (combination)
                    {
                        case MoveOneObjectCombination.OneMicrochip:
                            movedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                            floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);

                            floorsObjectsArrangementAfterMove[nextFloor].Microchips.Add(movedObject);
                            break;
                        default:
                            movedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[i];
                            floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(i);

                            floorsObjectsArrangementAfterMove[nextFloor].Generators.Add(movedObject);
                            break;
                    }

                    // Going up
                    if (up)
                    {
                        // If no chip is fried with new arrangement
                        if (!IsAnyChipFried(floorsObjectsArrangementAfterMove))
                        {
                            MoveObjectsToTopFloor(
                                floorsObjectsArrangementAfterMove, nextFloor, steps, ref minSteps
                            );
                        }
                    }
                    else
                    {
                        // Check if all floors below the current floor are empty,
                        // if yes there is no need to move object down
                        bool allFloorsBelowAreEmpty = CheckIfAllFloorsBellowAreEmpty(
                            floorsObjectsArrangement,
                            elevatorFloor
                        );

                        // Going down
                        if (!up && !allFloorsBelowAreEmpty)
                        {
                            // If no chip is fried with new arrangement
                            if (!IsAnyChipFried(floorsObjectsArrangementAfterMove))
                            {
                                oneObjectDown = true;
                                MoveObjectsToTopFloor(
                                    floorsObjectsArrangementAfterMove, elevatorFloor - 1, steps, ref minSteps
                                );
                            }
                        }
                    }
                }
            }
        }

        private void MoveTwoObjects(
            Dictionary<int, FloorObjects> floorsObjectsArrangement,
            int elevatorFloor,
            int steps,
            ref int minSteps,
            bool up,
            ref bool twoObjectsUp
        )
        {
            int minFloor = floorsObjectsArrangement.Keys.Min();
            int maxFloor = floorsObjectsArrangement.Keys.Max();
            int nextFloor = up ? elevatorFloor + 1 : elevatorFloor - 1;
            if (nextFloor > maxFloor || nextFloor < minFloor)
            {
                return;
            }

            List<string> microchips = floorsObjectsArrangement[elevatorFloor].Microchips;
            List<string> generators = floorsObjectsArrangement[elevatorFloor].Generators;

            IEnumerable<MoveTwoObjectsCombination> combinations =
                Enum.GetValues(typeof(MoveTwoObjectsCombination)).Cast<MoveTwoObjectsCombination>();

            foreach (MoveTwoObjectsCombination combination in combinations)
            {
                List<string> first;
                List<string> second;
                int startFrom = 1;

                switch (combination)
                {
                    case MoveTwoObjectsCombination.TwoMicrochips:
                        first = microchips;
                        second = microchips;
                        break;
                    case MoveTwoObjectsCombination.TwoGenerators:
                        first = generators;
                        second = generators;
                        break;
                    default:
                        first = microchips;
                        second = generators;
                        startFrom = 0;
                        break;
                }

                for (int i = 0; i < first.Count; i++)
                {
                    for (int j = startFrom; j < second.Count; j++)
                    {
                        string firstMovedObject;
                        string secondMovedObject;
                        Dictionary<int, FloorObjects> floorsObjectsArrangementAfterMove =
                            CloneFloorsObjectsArrangement(floorsObjectsArrangement);

                        switch (combination)
                        {
                            case MoveTwoObjectsCombination.TwoMicrochips:
                                if (floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.Count < 2)
                                {
                                    continue;
                                }

                                firstMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                                secondMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[j];
                                floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);
                                floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.Remove(secondMovedObject);

                                floorsObjectsArrangementAfterMove[nextFloor].Microchips.Add(firstMovedObject);
                                floorsObjectsArrangementAfterMove[nextFloor].Microchips.Add(secondMovedObject);
                                break;
                            case MoveTwoObjectsCombination.TwoGenerators:
                                if (floorsObjectsArrangementAfterMove[elevatorFloor].Generators.Count < 2)
                                {
                                    continue;
                                }

                                firstMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[i];
                                secondMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[j];
                                floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(i);
                                floorsObjectsArrangementAfterMove[elevatorFloor].Generators.Remove(secondMovedObject);

                                floorsObjectsArrangementAfterMove[nextFloor].Generators.Add(firstMovedObject);
                                floorsObjectsArrangementAfterMove[nextFloor].Generators.Add(secondMovedObject);
                                break;
                            default:
                                firstMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                                floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);
                                secondMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[j];
                                floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(j);

                                floorsObjectsArrangementAfterMove[nextFloor].Microchips.Add(firstMovedObject);
                                floorsObjectsArrangementAfterMove[nextFloor].Generators.Add(secondMovedObject);
                                break;
                        }

                        // Going up
                        if (up)
                        {
                            // If no chip is fried with new arrangement
                            if (!IsAnyChipFried(floorsObjectsArrangementAfterMove))
                            {
                                twoObjectsUp = true;
                                MoveObjectsToTopFloor(
                                    floorsObjectsArrangementAfterMove, nextFloor, steps, ref minSteps
                                );
                            }
                        }
                        else
                        {
                            // Check if all floors below the current floor are empty,
                            // if yes there is no need to move object down
                            bool allFloorsBelowAreEmpty = CheckIfAllFloorsBellowAreEmpty(
                                floorsObjectsArrangement,
                                elevatorFloor
                            );

                            // Going down
                            if (!up && !allFloorsBelowAreEmpty)
                            {
                                // If no chip is fried with new arrangement
                                if (!IsAnyChipFried(floorsObjectsArrangementAfterMove))
                                {
                                    MoveObjectsToTopFloor(
                                        floorsObjectsArrangementAfterMove, nextFloor, steps, ref minSteps
                                    );
                                }
                            }
                        }
                    }
                }

            }
        }

        private Dictionary<int, FloorObjects> CloneFloorsObjectsArrangement(
            Dictionary<int, FloorObjects> floorsObjectsArrangement
        )
        {
            Dictionary<int, FloorObjects> clonedFloorObjects =
                new Dictionary<int, FloorObjects>();

            foreach (KeyValuePair<int, FloorObjects> floorObjectsArrangement in floorsObjectsArrangement)
            {
                clonedFloorObjects.Add(floorObjectsArrangement.Key, new FloorObjects
                {
                    Microchips = new List<string>(floorObjectsArrangement.Value.Microchips),
                    Generators = new List<string>(floorObjectsArrangement.Value.Generators)
                });
            }

            return clonedFloorObjects;
        }

        private bool CheckIfAllFloorsBellowAreEmpty(
            Dictionary<int, FloorObjects> floorsObjectsArrangement,
            int elevatorFloor
        )
        {
            // Check if all floors below the current floor are empty,
            // if yes there is no need to move object down
            for (int floor = 1; floor < elevatorFloor; floor++)
            {
                if (floorsObjectsArrangement[floor].Microchips.Count > 0
                    || floorsObjectsArrangement[floor].Generators.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        private string FloorsObjectsArrangementCacheKey(
            Dictionary<int, FloorObjects> floorsObjectsArrangement
        )
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<int, FloorObjects> floorObjectsArrangement in floorsObjectsArrangement)
            {
                List<string> microchips = floorObjectsArrangement.Value.Microchips;
                List<string> generators = floorObjectsArrangement.Value.Generators;

                IEnumerable<string> pairs = microchips.Intersect(generators);
                IEnumerable<string> unpairedMicrochips = microchips.Except(generators);
                IEnumerable<string> unpairedGenerators = generators.Except(microchips);

                sb.Append($"Floor-{floorObjectsArrangement.Key}:");
                sb.Append($"pairs-{string.Join(',', pairs)},");
                sb.Append($"microchips-{string.Join(',', unpairedMicrochips)},");
                sb.Append($"generators-{string.Join(',', unpairedGenerators)}");
            }

            return sb.ToString();
        }
    }
}
