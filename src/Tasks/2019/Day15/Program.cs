using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day15
{
    public class Program
    {
        public int CalculateFewestNumberOfMovementCommandsToMoveRepairDroidFromStartToOxygenSystemLocation(
            long[] integersArray
        )
        {
            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            Dictionary<(int, int), (int, StatusCode)> minStepsToLocation = new Dictionary<(int, int), (int, StatusCode)>
            {
                { (0, 0), (0, StatusCode.Moved) }
            };

            GetMinStepsToLocation(integers, 0, 0, minStepsToLocation, 0, 0);

            // Find fewest number of movement commands required to move the repair droid
            // from its starting position to the location of the oxygen system
            int fewestNumberOfMovementCommands = int.MaxValue;
            foreach (KeyValuePair<(int, int), (int, StatusCode)> location in minStepsToLocation)
            {
                if (location.Value.Item2 == StatusCode.OxygenSystem)
                {
                    fewestNumberOfMovementCommands = Math.Min(location.Value.Item1, fewestNumberOfMovementCommands);
                }
            }

            return fewestNumberOfMovementCommands;
        }

        public int CalculateNumberOfMinutesToFillAreaWithOxygen(long[] integersArray)
        {
            int numberOfMinutesToFillAreaWithOxygen = -1;

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            Dictionary<(int, int), (int, StatusCode)> minStepsToLocation = new Dictionary<(int, int), (int, StatusCode)>
            {
                { (0, 0), (0, StatusCode.Moved) }
            };

            GetMinStepsToLocation(integers, 0, 0, minStepsToLocation, 0, 0);

            IEnumerable<MovementCommand> movementCommands =
                Enum.GetValues(typeof(MovementCommand)).Cast<MovementCommand>();

            // Collection of locations whose oxygen needs to be spread to adjacent locations
            HashSet<(int, int)> oxygenLocations = minStepsToLocation
                .Where(sl => sl.Value.Item2 == StatusCode.OxygenSystem)
                .Select(sl => sl.Key)
                .ToHashSet();

            while (minStepsToLocation.Count > 0)
            {
                // Spread oxygen from locations
                foreach ((int x, int y) in oxygenLocations.ToList())
                {
                    // Go in all directions
                    foreach (MovementCommand movementCommand in movementCommands)
                    {
                        int i = x;
                        int j = y;

                        switch (movementCommand)
                        {
                            case MovementCommand.North:
                                j++;
                                break;
                            case MovementCommand.South:
                                j--;
                                break;
                            case MovementCommand.West:
                                i--;
                                break;
                            case MovementCommand.East:
                                i++;
                                break;
                        }

                        // Spread oxygen to adjacent location
                        if (minStepsToLocation.ContainsKey((i, j)))
                        {
                            oxygenLocations.Add((i, j));
                        }
                    }

                    // This oxygen location is used so it can be removed
                    oxygenLocations.Remove((x, y));
                    minStepsToLocation.Remove((x, y));
                }

                numberOfMinutesToFillAreaWithOxygen++;
            }

            return numberOfMinutesToFillAreaWithOxygen;
        }

        private void GetMinStepsToLocation(
            Dictionary<long, long> integers,
            long index,
            long relativeBase,
            Dictionary<(int, int), (int, StatusCode)> minStepsToLocation,
            int i,
            int j
        )
        {
            IEnumerable<MovementCommand> minStepsToLocationEnumValues =
                Enum.GetValues(typeof(MovementCommand)).Cast<MovementCommand>();

            foreach (MovementCommand movementCommand in minStepsToLocationEnumValues)
            {
                DoMove(movementCommand, integers.ToDictionary(i => i.Key, i => i.Value), index,
                    relativeBase, minStepsToLocation, i, j);
            }
        }

        private void DoMove(
            MovementCommand movementCommand,
            Dictionary<long, long> integers,
            long index,
            long relativeBase,
            Dictionary<(int, int), (int, StatusCode)> minStepsToLocation,
            int i,
            int j
        )
        {
            // Movement commands to this location
            int minStepsToLocationCount = minStepsToLocation[(i, j)].Item1;

            (int output, bool halted) = CalculateOutputSignal(
                integers, (int)movementCommand, ref index, ref relativeBase);

            // If not halted and not wall
            if (!halted && output != (int)StatusCode.Wall)
            {
                switch (movementCommand)
                {
                    case MovementCommand.North:
                        j++;
                        break;
                    case MovementCommand.South:
                        j--;
                        break;
                    case MovementCommand.West:
                        i--;
                        break;
                    case MovementCommand.East:
                        i++;
                        break;
                }

                // If location is not discovered yet or can be reached in less movement commands
                if (!minStepsToLocation.ContainsKey((i, j))
                    || minStepsToLocationCount + 1 < minStepsToLocation[(i, j)].Item1)
                {
                    // If oxygen system is found
                    if (output == (int)StatusCode.OxygenSystem)
                    {
                        minStepsToLocation[(i, j)] = (minStepsToLocationCount + 1, StatusCode.OxygenSystem);
                    }
                    // If repair droid has moved one step in the requested direction
                    else
                    {
                        minStepsToLocation[(i, j)] = (minStepsToLocationCount + 1, StatusCode.Moved);
                    }

                    GetMinStepsToLocation(integers, index, relativeBase, minStepsToLocation, i, j);
                }
            }
        }

        private (int, bool) CalculateOutputSignal(
            Dictionary<long, long> integers,
            int input,
            ref long i,
            ref long relativeBase
        )
        {
            int outputSignal = -1;

            while (integers[i] != (int)Operation.Halt)
            {
                if (outputSignal != -1)
                {
                    return (outputSignal, false);
                }

                // Pad first instruction with leading zeros
                string instruction = integers[i].ToString("D5");

                int operation = (int)char.GetNumericValue(instruction[^1]);
                int firstParameterModeDigit = (int)char.GetNumericValue(instruction[2]);
                int secondParameterModeDigit = (int)char.GetNumericValue(instruction[1]);
                int thirdParameterModeDigit = (int)char.GetNumericValue(instruction[0]);

                ParameterMode firstParameterMode;
                ParameterMode secondParameterMode;
                ParameterMode thirdParameterMode;
                long firstParameter;
                long secondParameter;
                long thirdParameter;

                switch (operation)
                {
                    case (int)Operation.Addition:
                    case (int)Operation.Multiplication:
                    case (int)Operation.LessThan:
                    case (int)Operation.Equals:
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        secondParameterMode = GetParameterMode(secondParameterModeDigit);
                        thirdParameterMode = GetParameterMode(thirdParameterModeDigit);
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, relativeBase);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode, relativeBase);
                        thirdParameter = GetParameter(integers, i + 3, thirdParameterMode, relativeBase, true);
                        i += 4;

                        if (operation == (int)Operation.Addition)
                        {
                            integers[thirdParameter] = firstParameter + secondParameter;
                        }
                        else if (operation == (int)Operation.Multiplication)
                        {
                            integers[thirdParameter] = firstParameter * secondParameter;
                        }
                        else if (operation == (int)Operation.LessThan)
                        {
                            integers[thirdParameter] = 0;
                            if (firstParameter < secondParameter)
                            {
                                integers[thirdParameter] = 1;
                            }
                        }
                        else if (operation == (int)Operation.Equals)
                        {
                            integers[thirdParameter] = 0;
                            if (firstParameter == secondParameter)
                            {
                                integers[thirdParameter] = 1;
                            }
                        }
                        break;
                    case (int)Operation.Input:
                    case (int)Operation.Output:
                    case (int)Operation.RelativeBaseOffset:
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        firstParameter = GetParameter(
                            integers, i + 1, firstParameterMode, relativeBase, operation == (int)Operation.Input);
                        i += 2;

                        if (operation == (int)Operation.Input)
                        {
                            integers[firstParameter] = input;
                        }
                        else if (operation == (int)Operation.Output)
                        {
                            outputSignal = (int)firstParameter;
                        }
                        else if (operation == (int)Operation.RelativeBaseOffset)
                        {
                            relativeBase += firstParameter;
                        }
                        break;
                    case (int)Operation.JumpIfTrue:
                    case (int)Operation.JumpIfFalse:
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        secondParameterMode = GetParameterMode(secondParameterModeDigit);
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, relativeBase);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode, relativeBase);
                        i += 3;

                        if (operation == (int)Operation.JumpIfTrue)
                        {
                            if (firstParameter != 0)
                            {
                                i = secondParameter;
                            }
                        }
                        else if (operation == (int)Operation.JumpIfFalse)
                        {
                            if (firstParameter == 0)
                            {
                                i = secondParameter;
                            }
                        }
                        break;
                }
            }

            return (outputSignal, true);
        }

        private Dictionary<long, long> InitIntegersMemory(long[] integersArray)
        {
            Dictionary<long, long> integers = new Dictionary<long, long>();
            for (int i = 0; i < integersArray.Length; i++)
            {
                integers.Add(i, integersArray[i]);
            }

            return integers;
        }

        private ParameterMode GetParameterMode(int mode)
        {
            ParameterMode parameterMode;

            switch (mode)
            {
                case 1:
                    parameterMode = ParameterMode.ImmediateMode;
                    break;
                case 2:
                    parameterMode = ParameterMode.RelativeMode;
                    break;
                default:
                    parameterMode = ParameterMode.PositionMode;
                    break;
            }

            return parameterMode;
        }

        private long GetParameter(
            Dictionary<long, long> integers,
            long i,
            ParameterMode mode,
            long relativeBase,
            bool instructionWritesTo = false
        )
        {
            long parameter;

            if (instructionWritesTo)
            {
                if (mode == ParameterMode.RelativeMode)
                {
                    return relativeBase + integers[i];
                }
                else
                {
                    return integers[i];
                }
            }

            switch (mode)
            {
                case ParameterMode.ImmediateMode:
                    parameter = integers[i];
                    break;
                case ParameterMode.RelativeMode:
                    parameter = integers.ContainsKey(relativeBase + integers[i])
                        ? integers[relativeBase + integers[i]] : 0;
                    break;
                default:
                    parameter = integers.ContainsKey(integers[i]) ? integers[integers[i]] : 0;
                    break;
            }

            return parameter;
        }
    }
}
