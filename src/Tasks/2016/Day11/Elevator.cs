using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2016.Day11
{
    public class Elevator
    {
        private Dictionary<string, int> floorsObjectsArrangementStepsCache = new Dictionary<string, int>();

        public int CalculateMinimumNumberOfStepsToBringAllObjectsToLastFloor(
            Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangement
        )
        {
            int minFloor = floorsObjectsArrangement.Keys.Min();
            int maxFloor = floorsObjectsArrangement.Keys.Max();

            int minSteps = maxFloor - minFloor - 1;
            bool minStepsFound = false;
            while (!minStepsFound)
            {
                minSteps++;
                floorsObjectsArrangementStepsCache = new Dictionary<string, int>();
                MoveObjectsToTopFloor(floorsObjectsArrangement, minFloor, 0, minSteps, ref minStepsFound);
            }

            return minSteps;
        }

        private void MoveObjectsToTopFloor(
            Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangement,
            int elevatorFloor,
            int steps,
            int minSteps,
            ref bool minStepsFound
        )
        {
            if (minStepsFound == true || steps > minSteps)
            {
                return;
            }

            if (IsAnyChipFried(floorsObjectsArrangement))
            {
                return;
            }

            if (AreAllObjectsOnLastFloor(floorsObjectsArrangement))
            {
                minStepsFound = true;
                return;
            }

            string floorsObjectsArrangementString = FloorsObjectsArrangementToString(floorsObjectsArrangement);
            if (floorsObjectsArrangementStepsCache.ContainsKey(floorsObjectsArrangementString)
                && steps >= floorsObjectsArrangementStepsCache[floorsObjectsArrangementString])
            {
                return;
            }

            floorsObjectsArrangementStepsCache[floorsObjectsArrangementString] = steps;

            List<string> microchips = floorsObjectsArrangement[elevatorFloor].Microchips;
            List<string> generators = floorsObjectsArrangement[elevatorFloor].Generators;

            MoveObjects(floorsObjectsArrangement, elevatorFloor, steps, minSteps, ref minStepsFound, microchips, null);
            MoveObjects(floorsObjectsArrangement, elevatorFloor, steps, minSteps, ref minStepsFound, null, generators);
            MoveObjects(floorsObjectsArrangement, elevatorFloor, steps, minSteps, ref minStepsFound, microchips, generators);
        }


        private bool IsAnyChipFried(Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangement)
        {
            foreach (KeyValuePair<int, FloorObjectsArrangement> floorObjectsArrangement in floorsObjectsArrangement)
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

        private bool AreAllObjectsOnLastFloor(Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangement)
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

        private void MoveObjects(
            Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangement,
            int elevatorFloor,
            int steps,
            int minSteps,
            ref bool minStepsFound,
            List<string> microchips,
            List<string> generators
        )
        {
            int minFloor = floorsObjectsArrangement.Keys.Min();
            int maxFloor = floorsObjectsArrangement.Keys.Max();
            // Increase number of steps
            steps++;

            List<string> first = microchips;
            List<string> second = generators;
            int startFrom = 0;

            if (generators == null)
            {
                second = microchips;
                // Same object cannot be moved twice
                startFrom = 1;
            }
            else if (microchips == null)
            {
                first = generators;
                // Same object cannot be moved twice
                startFrom = 1;
            }

            for (int i = 0; i < first.Count; i++)
            {
                // If moving only microchips or only generators try to move one item also
                if (microchips == null || generators == null)
                {
                    string movedObject;
                    Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangementAfterMove =
                        CloneFloorsObjectsArrangement(floorsObjectsArrangement);

                    if (microchips == null)
                    {
                        movedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Generators[i];
                        floorsObjectsArrangementAfterMove[elevatorFloor].Generators.RemoveAt(i);
                    }
                    else
                    {
                        movedObject = floorsObjectsArrangementAfterMove[elevatorFloor].Microchips[i];
                        floorsObjectsArrangementAfterMove[elevatorFloor].Microchips.RemoveAt(i);
                    }

                    // Going up
                    if (elevatorFloor < maxFloor)
                    {
                        Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangementAfterUpMove =
                            CloneFloorsObjectsArrangement(floorsObjectsArrangementAfterMove);

                        if (microchips == null)
                        {
                            floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Generators.Add(movedObject);
                        }
                        else
                        {
                            floorsObjectsArrangementAfterUpMove[elevatorFloor + 1].Microchips.Add(movedObject);
                        }

                        MoveObjectsToTopFloor(
                            floorsObjectsArrangementAfterUpMove, elevatorFloor + 1, steps, minSteps, ref minStepsFound
                        );
                    }

                    // Going down
                    if (elevatorFloor > minFloor)
                    {
                        Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangementAfterDownMove =
                           CloneFloorsObjectsArrangement(floorsObjectsArrangementAfterMove);

                        if (microchips == null)
                        {
                            floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Generators.Add(movedObject);
                        }
                        else
                        {
                            floorsObjectsArrangementAfterDownMove[elevatorFloor - 1].Microchips.Add(movedObject);
                        }

                        MoveObjectsToTopFloor(
                            floorsObjectsArrangementAfterDownMove, elevatorFloor - 1, steps, minSteps, ref minStepsFound
                        );
                    }
                }

                // Move two objects
                for (int j = startFrom; j < second.Count; j++)
                {
                    string firstMovedObject;
                    string secondMovedObject;
                    Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangementAfterMove =
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
                    if (elevatorFloor < maxFloor)
                    {
                        Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangementAfterUpMove =
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

                        MoveObjectsToTopFloor(
                            floorsObjectsArrangementAfterUpMove, elevatorFloor + 1, steps, minSteps, ref minStepsFound
                        );
                    }
                }
            }
        }
        private Dictionary<int, FloorObjectsArrangement> CloneFloorsObjectsArrangement(
            Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangement
        )
        {
            Dictionary<int, FloorObjectsArrangement> clonedFloorObjectsArrangement =
                new Dictionary<int, FloorObjectsArrangement>();

            foreach (KeyValuePair<int, FloorObjectsArrangement> floorObjectsArrangement in floorsObjectsArrangement)
            {
                clonedFloorObjectsArrangement.Add(floorObjectsArrangement.Key, new FloorObjectsArrangement
                {
                    Microchips = new List<string>(floorObjectsArrangement.Value.Microchips),
                    Generators = new List<string>(floorObjectsArrangement.Value.Generators)
                });
            }

            return clonedFloorObjectsArrangement;
        }

        private string FloorsObjectsArrangementToString(
            Dictionary<int, FloorObjectsArrangement> floorsObjectsArrangement
        )
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<int, FloorObjectsArrangement> floorObjectsArrangement in floorsObjectsArrangement)
            {
                sb.Append(floorObjectsArrangement.Key);
                foreach (string microchip in floorObjectsArrangement.Value.Microchips)
                {
                    sb.Append($"M{microchip}");
                }
                foreach (string generator in floorObjectsArrangement.Value.Generators)
                {
                    sb.Append($"G{generator}");
                }
            }

            return sb.ToString();
        }
    }
}
