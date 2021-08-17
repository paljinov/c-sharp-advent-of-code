using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day17
{
    public class VacuumRobot
    {
        private const char SCAFFOLD = '#';

        private const char OPEN_SPACE = '.';

        private const char UP = '^';

        private const char DOWN = 'v';

        private const char LEFT = '<';

        private const char RIGHT = '>';

        private const int MOVEMENT_FUNCTIONS_MAX_CHARACTERS = 20;

        public int CalculateSumOfTheAlignmentParametersForTheScaffoldIntersections(long[] integersArray)
        {
            int sumOfTheAlignmentParameters = 0;

            char[,] image = GetImage(integersArray);

            for (int i = 1; i < image.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < image.GetLength(1) - 1; j++)
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

        public int CalculateDustCollectedByTheVacuumRobot(long[] integersArray)
        {
            int dust = 0;

            integersArray[0] = 2;
            char[,] image = GetImage(integersArray);

            PrintImage(image);

            return dust;
        }

        private char[,] GetImage(long[] integersArray)
        {
            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            List<int> outputs = new List<int>();
            int outputSignal;
            bool halted = false;

            // IntCode program current index and relative base value
            long index = 0;
            long relativeBase = 0;

            int i = 0;
            int j = 0;

            while (!halted)
            {
                (outputSignal, halted) = CalculateOutputSignal(integers, 0, ref index, ref relativeBase);
                outputs.Add(outputSignal);
            }

            int rows = outputs.Where(o => o == (int)CameraOutput.NewLine).Count();
            int columns = outputs.IndexOf((int)CameraOutput.NewLine);

            char[,] image = new char[rows, columns];
            foreach (int output in outputs)
            {
                if (output == (int)CameraOutput.NewLine)
                {
                    i++;
                    j = 0;
                }
                else
                {
                    if (output == (int)CameraOutput.Scaffold)
                    {
                        image[i, j] = SCAFFOLD;
                    }
                    else if (output == (int)CameraOutput.OpenSpace)
                    {
                        image[i, j] = OPEN_SPACE;
                    }
                    else
                    {
                        switch (output)
                        {
                            case UP:
                                image[i, j] = UP;
                                break;
                            case DOWN:
                                image[i, j] = DOWN;
                                break;
                            case LEFT:
                                image[i, j] = LEFT;
                                break;
                            case RIGHT:
                                image[i, j] = RIGHT;
                                break;
                        }
                    }

                    j++;
                }
            }

            return image;
        }

        private bool IsScaffoldIntersection(int i, int j, char[,] image)
        {
            for (int k = i - 1; k <= i + 1; k++)
            {
                if (k < 0 || k >= image.GetLength(0) || image[k, j] != SCAFFOLD)
                {
                    return false;
                }
            }

            for (int h = j - 1; h <= j + 1; h++)
            {
                if (h < 0 || h >= image.GetLength(1) || image[i, h] != SCAFFOLD)
                {
                    return false;
                }
            }

            return true;
        }

        private void PrintImage(char[,] image)
        {
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Console.Write(image[i, j]);
                }

                Console.WriteLine();
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
