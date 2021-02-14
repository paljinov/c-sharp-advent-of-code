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

            List<string> microchips = floorsObjectsArrangement[elevatorFloor].Microchips;
            List<string> generators = floorsObjectsArrangement[elevatorFloor].Generators;

            // Increase number of steps
            steps++;

            bool twoObjectsUp = false;
            // Move two microchips up
            MoveTwoObjects(floorsObjectsArrangement,
                elevatorFloor,
                steps,
                ref minSteps,
                microchips,
                null,
                true,
                ref twoObjectsUp
            );
            // Move two generators up
            MoveTwoObjects(floorsObjectsArrangement,
                elevatorFloor,
                steps,
                ref minSteps,
                null,
                generators,
                true,
                ref twoObjectsUp
            );
            // Move one microchip and one generator up
            MoveTwoObjects(floorsObjectsArrangement,
                elevatorFloor,
                steps,
                ref minSteps,
                microchips,
                generators,
                true,
                ref twoObjectsUp
            );

            bool oneObjectDown = false;
            // Move one microchip down
            MoveOneObject(floorsObjectsArrangement,
                elevatorFloor,
                steps,
                ref minSteps,
                microchips,
                true,
                false,
                ref oneObjectDown
            );
            // Move one generator down
            MoveOneObject(floorsObjectsArrangement,
                elevatorFloor,
                steps,
                ref minSteps,
                generators,
                false,
                false,
                ref oneObjectDown
            );

            // Move two objects down only if it is not possible to move one object down
            if (!oneObjectDown)
            {
                MoveTwoObjects(floorsObjectsArrangement,
                    elevatorFloor,
                    steps,
                    ref minSteps,
                    microchips,
                    null,
                    false,
                    ref twoObjectsUp
                );
                MoveTwoObjects(floorsObjectsArrangement,
                    elevatorFloor,
                    steps,
                    ref minSteps,
                    null,
                    generators,
                    false,
                    ref twoObjectsUp
                );
                MoveTwoObjects(floorsObjectsArrangement,
                    elevatorFloor,
                    steps,
                    ref minSteps,
                    microchips,
                    generators,
                    false,
                    ref twoObjectsUp
                );
            }

            // Move one object up only if it is not possible to move two objects up
            if (!twoObjectsUp)
            {
                MoveOneObject(floorsObjectsArrangement,
                    elevatorFloor,
                    steps,
                    ref minSteps,
                    microchips,
                    true,
                    true,
                    ref oneObjectDown
                );
                MoveOneObject(floorsObjectsArrangement,
                    elevatorFloor,
                    steps,
                    ref minSteps,
                    generators,
                    false,
                    true,
                    ref oneObjectDown
                );
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
            List<string> @object,
            bool moveMicrochip,
            bool up,
            ref bool oneObjectDown
        )
        {
            int minFloor = floorsObjectsArrangement.Keys.Min();
            int maxFloor = floorsObjectsArrangement.Keys.Max();

            IEnumerable<string> pairs = floorsObjectsArrangement[elevatorFloor].Microchips
                .Intersect(floorsObjectsArrangement[elevatorFloor].Generators);

            bool moveAny = false;
            if (pairs.Count() * 2 == @object.Count)
            {
                moveAny = true;
            }

            for (int i = 0; i < @object.Count; i++)
            {
                string movedObject;
                Dictionary<int, FloorObjects> floorsObjectsArrangementAfterMove =
                    CloneFloorsObjectsArrangement(floorsObjectsArrangement);

                if (moveMicrochip)
                {
                    movedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                    floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);
                }
                else
                {
                    movedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[i];
                    floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(i);
                }

                // Going up
                if (up && elevatorFloor < maxFloor)
                {
                    Dictionary<int, FloorObjects> floorsObjectsArrangementAfterUpMove =
                        CloneFloorsObjectsArrangement(floorsObjectsArrangementAfterMove);

                    if (moveMicrochip)
                    {
                        floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Microchips.Add(movedObject);
                    }
                    else
                    {
                        floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Generators.Add(movedObject);
                    }

                    // If no chip is fried with new arrangement
                    if (!IsAnyChipFried(floorsObjectsArrangementAfterUpMove))
                    {
                        MoveObjectsToTopFloor(
                            floorsObjectsArrangementAfterUpMove, elevatorFloor + 1, steps, ref minSteps
                        );
                        if (moveAny)
                        {
                            break;
                        }
                    }
                }

                // Check if all floors below the current floor are empty,
                // if yes there is no need to move object down
                bool allFloorsBelowAreEmpty = CheckIfAllFloorsBellowAreEmpty(
                    floorsObjectsArrangement,
                    elevatorFloor
                );

                // Going down
                if (!up && elevatorFloor > minFloor && !allFloorsBelowAreEmpty)
                {
                    Dictionary<int, FloorObjects> floorsObjectsArrangementAfterDownMove =
                        CloneFloorsObjectsArrangement(floorsObjectsArrangementAfterMove);

                    if (moveMicrochip)
                    {
                        floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Microchips.Add(movedObject);
                    }
                    else
                    {
                        floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Generators.Add(movedObject);
                    }

                    // If no chip is fried with new arrangement
                    if (!IsAnyChipFried(floorsObjectsArrangementAfterDownMove))
                    {
                        oneObjectDown = true;
                        MoveObjectsToTopFloor(
                            floorsObjectsArrangementAfterDownMove, elevatorFloor - 1, steps, ref minSteps
                        );
                        if (moveAny)
                        {
                            break;
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
            List<string> microchips,
            List<string> generators,
            bool up,
            ref bool twoObjectsUp
        )
        {
            int minFloor = floorsObjectsArrangement.Keys.Min();
            int maxFloor = floorsObjectsArrangement.Keys.Max();

            IEnumerable<string> pairs = floorsObjectsArrangement[elevatorFloor].Microchips
                .Intersect(floorsObjectsArrangement[elevatorFloor].Generators);

            bool moveAny = false;
            if (pairs.Count() * 2 == floorsObjectsArrangement[elevatorFloor].Microchips.Count
                + floorsObjectsArrangement[elevatorFloor].Generators.Count)
            {
                moveAny = true;
            }

            List<string> first = microchips;
            List<string> second = generators;
            int startFrom = 0;

            if (generators == null)
            {
                startFrom = 1;
                second = microchips;
            }
            else if (microchips == null)
            {
                startFrom = 1;
                first = generators;
            }

            for (int i = 0; i < first.Count; i++)
            {
                for (int j = startFrom; j < second.Count; j++)
                {
                    string firstMovedObject;
                    string secondMovedObject;
                    Dictionary<int, FloorObjects> floorsObjectsArrangementAfterMove =
                        CloneFloorsObjectsArrangement(floorsObjectsArrangement);

                    if (microchips == null)
                    {
                        if (floorsObjectsArrangementAfterMove[elevatorFloor].Generators.Count < 2)
                        {
                            continue;
                        }

                        firstMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[i];
                        secondMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[j];
                        floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(i);
                        floorsObjectsArrangementAfterMove[elevatorFloor].Generators.Remove(secondMovedObject);
                    }
                    else if (generators == null)
                    {
                        if (floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.Count < 2)
                        {
                            continue;
                        }

                        firstMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                        secondMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[j];
                        floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);
                        floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.Remove(secondMovedObject);
                    }
                    else
                    {
                        firstMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                        floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);
                        secondMovedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[j];
                        floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(j);
                    }

                    // Going up
                    if (up && elevatorFloor < maxFloor)
                    {
                        Dictionary<int, FloorObjects> floorsObjectsArrangementAfterUpMove =
                            CloneFloorsObjectsArrangement(floorsObjectsArrangementAfterMove);

                        if (microchips == null)
                        {
                            floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Generators.Add(firstMovedObject);
                            floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Generators.Add(secondMovedObject);
                        }
                        else if (generators == null)
                        {
                            floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Microchips.Add(firstMovedObject);
                            floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Microchips.Add(secondMovedObject);
                        }
                        else
                        {
                            floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Microchips.Add(firstMovedObject);
                            floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Generators.Add(secondMovedObject);
                        }

                        // If no chip is fried with new arrangement
                        if (!IsAnyChipFried(floorsObjectsArrangementAfterUpMove))
                        {
                            twoObjectsUp = true;
                            MoveObjectsToTopFloor(
                                floorsObjectsArrangementAfterUpMove, elevatorFloor + 1, steps, ref minSteps
                            );
                            if (moveAny)
                            {
                                break;
                            }
                        }
                    }

                    // Check if all floors below the current floor are empty,
                    // if yes there is no need to move object down
                    bool allFloorsBelowAreEmpty = CheckIfAllFloorsBellowAreEmpty(
                        floorsObjectsArrangement,
                        elevatorFloor
                    );

                    // Going down
                    if (!up && elevatorFloor > minFloor && !allFloorsBelowAreEmpty)
                    {
                        Dictionary<int, FloorObjects> floorsObjectsArrangementAfterDownMove =
                            CloneFloorsObjectsArrangement(floorsObjectsArrangementAfterMove);

                        if (microchips == null)
                        {
                            floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Generators.Add(firstMovedObject);
                            floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Generators.Add(secondMovedObject);
                        }
                        else if (generators == null)
                        {
                            floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Microchips.Add(firstMovedObject);
                            floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Microchips.Add(secondMovedObject);
                        }
                        else
                        {
                            floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Microchips.Add(firstMovedObject);
                            floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Generators.Add(secondMovedObject);
                        }

                        // If no chip is fried with new arrangement
                        if (!IsAnyChipFried(floorsObjectsArrangementAfterDownMove))
                        {
                            MoveObjectsToTopFloor(
                                floorsObjectsArrangementAfterDownMove, elevatorFloor - 1, steps, ref minSteps
                            );
                            if (moveAny)
                            {
                                break;
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
