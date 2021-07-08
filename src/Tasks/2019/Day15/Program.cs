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
            int minMovementCommands = int.MaxValue;

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            Dictionary<(int, int), int> movementCommands = new Dictionary<(int, int), int>
            {
                { (0, 0), 0 }
            };

            long index = 0;
            long relativeBase = 0;

            DoCalculateFewestNumberOfMovementCommands(
                integers, ref index, ref relativeBase, movementCommands, 0, 0, ref minMovementCommands);

            return minMovementCommands;
        }

        private void DoCalculateFewestNumberOfMovementCommands(
            Dictionary<long, long> integers,
            ref long index,
            ref long relativeBase,
            Dictionary<(int, int), int> movementCommands,
            int i,
            int j,
            ref int minMovementCommands
        )
        {
            IEnumerable<MovementCommand> movementCommandsEnumValues =
                Enum.GetValues(typeof(MovementCommand)).Cast<MovementCommand>();

            // Movement commands to this location
            int movementCommandsCount = movementCommands[(i, j)];

            foreach (MovementCommand movementCommandEnumValue in movementCommandsEnumValues)
            {
                // Copy variables for current iteration
                Dictionary<long, long> integersCopy = integers.ToDictionary(i => i.Key, i => i.Value);
                long indexCopy = index;
                long relativeBaseCopy = relativeBase;
                int k = i;
                int h = j;

                (int output, bool halted) = CalculateOutputSignal(
                    integersCopy, (int)movementCommandEnumValue, ref indexCopy, ref relativeBaseCopy);

                // If not halted and not wall
                if (!halted && output != (int)StatusCode.Wall)
                {
                    switch (movementCommandEnumValue)
                    {
                        case MovementCommand.North:
                            h++;
                            break;
                        case MovementCommand.South:
                            h--;
                            break;
                        case MovementCommand.West:
                            k--;
                            break;
                        case MovementCommand.East:
                            k++;
                            break;
                    }

                    // If location is not discovered yet or can be reached in less movement commands
                    if (!movementCommands.ContainsKey((k, h)) || movementCommandsCount + 1 < movementCommands[(k, h)])
                    {
                        movementCommands[(k, h)] = movementCommandsCount + 1;

                        // If oxygen system is found
                        if (output == (int)StatusCode.OxygenSystem)
                        {
                            minMovementCommands = movementCommands[(k, h)];
                        }
                        // If repair droid has moved one step in the requested direction
                        else
                        {
                            DoCalculateFewestNumberOfMovementCommands(integersCopy, ref indexCopy, ref relativeBaseCopy,
                                movementCommands, k, h, ref minMovementCommands);
                        }
                    }
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
