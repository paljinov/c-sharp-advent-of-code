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

            string floorsObjectsArrangementCacheKey = FloorsObjectsInterchangeableArrangementsCacheKey(
                floorsObjectsArrangement,
                elevatorFloor
            );
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
                    Dictionary<int, FloorObjects> floorsObjectsArrangementAfterMove =
                        CloneFloorsObjectsArrangement(floorsObjectsArrangement);

                    switch (combination)
                    {
                        case MoveOneObjectCombination.OneMicrochip:
                            string moveMicrochip = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                            floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);

                            floorsObjectsArrangementAfterMove[nextFloor].Microchips.Add(moveMicrochip);
                            break;
                        default:
                            string moveGenerator = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[i];
                            floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(i);

                            floorsObjectsArrangementAfterMove[nextFloor].Generators.Add(moveGenerator);
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
                List<string> firstObjects;
                List<string> secondObjects;
                int startFrom = 0;

                switch (combination)
                {
                    case MoveTwoObjectsCombination.TwoMicrochips:
                        firstObjects = microchips;
                        secondObjects = microchips;
                        break;
                    case MoveTwoObjectsCombination.TwoGenerators:
                        firstObjects = generators;
                        secondObjects = generators;
                        break;
                    default:
                        firstObjects = microchips;
                        secondObjects = generators;
                        break;
                }

                for (int i = 0; i < firstObjects.Count; i++)
                {
                    for (int j = startFrom; j < secondObjects.Count; j++)
                    {
                        Dictionary<int, FloorObjects> floorsObjectsArrangementAfterMove =
                            CloneFloorsObjectsArrangement(floorsObjectsArrangement);

                        switch (combination)
                        {
                            case MoveTwoObjectsCombination.TwoMicrochips:
                                string moveFirstMicrochip =
                                    floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                                string moveSecondMicrochip =
                                    floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[j];

                                // There is no two same microchips
                                if (moveFirstMicrochip == moveSecondMicrochip)
                                {
                                    continue;
                                }

                                floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);
                                floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.Remove(moveSecondMicrochip);

                                floorsObjectsArrangementAfterMove[nextFloor].Microchips.Add(moveFirstMicrochip);
                                floorsObjectsArrangementAfterMove[nextFloor].Microchips.Add(moveSecondMicrochip);
                                break;
                            case MoveTwoObjectsCombination.TwoGenerators:
                                string moveFirstGenerator =
                                    floorsObjectsArrangementAfterMove[elevatorFloor].Generators[i];
                                string moveSecondGenerator =
                                    floorsObjectsArrangementAfterMove[elevatorFloor].Generators[j];

                                // There is no two same generators
                                if (moveFirstGenerator == moveSecondGenerator)
                                {
                                    continue;
                                }

                                floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(i);
                                floorsObjectsArrangementAfterMove[elevatorFloor].Generators.Remove(moveSecondGenerator);

                                floorsObjectsArrangementAfterMove[nextFloor].Generators.Add(moveFirstGenerator);
                                floorsObjectsArrangementAfterMove[nextFloor].Generators.Add(moveSecondGenerator);
                                break;
                            default:
                                string moveMicrochip = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                                floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);
                                string moveGenerator = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[j];
                                floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(j);

                                floorsObjectsArrangementAfterMove[nextFloor].Microchips.Add(moveMicrochip);
                                floorsObjectsArrangementAfterMove[nextFloor].Generators.Add(moveGenerator);
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

        /// <summary>
        /// All pairs are interchangeabls, e.g.:
        ///
        /// FLOOR 1: Lithium microchip,
        /// FLOOR 2: Hydrogen microchip,
        /// FLOOR 3: Hydrogen generator,
        /// FLOOR 4: Lithium generator
        ///
        /// requries the same number of steps to top floor as
        ///
        /// FLOOR 1: Hydrogen microchip,
        /// FLOOR 2: Lithium microchip,
        /// FLOOR 3: Lithium generator,
        /// FLOOR 4: Hydrogen generator
        ///
        /// Unique floor objects arrangement is defined by elevator position and microchip-generator pairs positions.
        /// </summary>
        /// <param name="floorsObjectsArrangement"></param>
        /// <param name="elevatorFloor"></param>
        /// <returns></returns>
        private string FloorsObjectsInterchangeableArrangementsCacheKey(
            Dictionary<int, FloorObjects> floorsObjectsArrangement,
            int elevatorFloor
        )
        {
            StringBuilder sb = new StringBuilder();

            Dictionary<string, int> microchipsFloors = new Dictionary<string, int>();
            Dictionary<string, int> generatorsFloors = new Dictionary<string, int>();

            foreach (KeyValuePair<int, FloorObjects> floorObjectsArrangement in floorsObjectsArrangement)
            {
                foreach (string microchip in floorObjectsArrangement.Value.Microchips)
                {
                    microchipsFloors.Add(microchip, floorObjectsArrangement.Key);
                }

                foreach (string generator in floorObjectsArrangement.Value.Generators)
                {
                    generatorsFloors.Add(generator, floorObjectsArrangement.Key);
                }
            }

            sb.Append($"(E{elevatorFloor}-");
            foreach (KeyValuePair<string, int> microchipsFloor in microchipsFloors)
            {
                sb.Append($"(M{microchipsFloor.Value}-G{generatorsFloors[microchipsFloor.Key]}),");
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
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
    }
}
