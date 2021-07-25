using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day17
{
    public class VacuumRobot
    {
        public int CalculateSumOfTheAlignmentParametersForTheScaffoldIntersections(long[] integersArray)
        {
            int sumOfTheAlignmentParameters = 0;

            Dictionary<(int, int), CameraOutput> image = GetImage(integersArray);

            int iMax = image.Keys.Select(k => k.Item1).Max();
            int jMax = image.Keys.Select(k => k.Item2).Max();

            for (int i = 1; i < iMax; i++)
            {
                for (int j = 1; j < jMax; j++)
                {
                    // If this is scaffold intersection
                    if (IsScaffoldIntersection(i, j, image))
                    {
                        int alignmentParameter = i * j;
                        sumOfTheAlignmentParameters += alignmentParameter;
                    }
                }
            }

            return sumOfTheAlignmentParameters;
        }

        private Dictionary<(int, int), CameraOutput> GetImage(long[] integersArray)
        {
            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            int output;
            bool halted = false;

            // IntCode program current index and relative base value
            long index = 0;
            long relativeBase = 0;

            int i = 0;
            int j = 0;
            Dictionary<(int, int), CameraOutput> image = new Dictionary<(int, int), CameraOutput>();

            while (!halted)
            {
                (output, halted) = CalculateOutputSignal(integers, 0, ref index, ref relativeBase);
                if (output == (int)CameraOutput.NewLine)
                {
                    i++;
                    j = 0;
                }
                else
                {
                    image[(i, j)] = output == (int)CameraOutput.Scaffold ? CameraOutput.Scaffold : CameraOutput.NewLine;
                    j++;
                }
            }

            return image;
        }

        private bool IsScaffoldIntersection(int i, int j, Dictionary<(int, int), CameraOutput> image)
        {
            for (int k = i - 1; k <= i + 1; k++)
            {
                if (!image.ContainsKey((k, j)) || image[(k, j)] != CameraOutput.Scaffold)
                {
                    return false;
                }
            }

            for (int h = j - 1; h <= j + 1; h++)
            {
                if (!image.ContainsKey((i, h)) || image[(i, h)] != CameraOutput.Scaffold)
                {
                    return false;
                }
            }

            return true;
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
