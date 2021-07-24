using System.Collections.Generic;

namespace App.Tasks.Year2019.Day19
{
    public class Program
    {
        public int CountPointsWhichAreAffectedByTheTractorBeam(long[] integersArray, int sizeOfAreaClosestToEmitter)
        {
            int pointsWhichAreAffectedByTheTractorBeam = 0;

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            for (int i = 0; i < sizeOfAreaClosestToEmitter; i++)
            {
                for (int j = 0; j < sizeOfAreaClosestToEmitter; j++)
                {
                    if (IsPointAffectedByTheTractorBeam(new Dictionary<long, long>(integers), i, j))
                    {
                        pointsWhichAreAffectedByTheTractorBeam++;
                    }
                }
            }

            return pointsWhichAreAffectedByTheTractorBeam;
        }

        public int CalculateResultForClosestPointToEmitterOfSquareThatFitsEntirelyWithinTractorBeam(
            long[] integersArray,
            int squareSize,
            int multiplier
        )
        {
            int x = 0;
            int y = 100;
            bool squareFound = false;

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            // While square that fits entirely within the tractor beam is not found
            while (!squareFound)
            {
                int? firstAffectedX = FindFirstXForPointInRowAffectedByTheTractorBeam(integers, squareSize, x, y);

                // If current row has point affected by tractor beam
                if (firstAffectedX.HasValue)
                {
                    x = firstAffectedX.Value;
                    int xAffected = x;

                    bool isSquareTopRightPointAffectedByTheTractorBeam = true;
                    while (isSquareTopRightPointAffectedByTheTractorBeam)
                    {
                        isSquareTopRightPointAffectedByTheTractorBeam = IsPointAffectedByTheTractorBeam(
                            new Dictionary<long, long>(integers), xAffected + squareSize - 1, y);

                        bool isSquareBottomLeftPointAffectedByTheTractorBeam = IsPointAffectedByTheTractorBeam(
                            new Dictionary<long, long>(integers), xAffected, y + squareSize - 1);

                        if (isSquareTopRightPointAffectedByTheTractorBeam
                            && isSquareBottomLeftPointAffectedByTheTractorBeam)
                        {
                            squareFound = true;
                            x = xAffected;
                            break;
                        }

                        xAffected++;
                    }

                    if (!squareFound)
                    {
                        y++;
                    }
                }
                // If current row doesn't have point affected by tractor beam
                else
                {
                    y++;
                }
            }

            return x * multiplier + y;
        }

        private bool IsPointAffectedByTheTractorBeam(Dictionary<long, long> integers, int i, int j)
        {
            Queue<int> inputs = new Queue<int>();
            inputs.Enqueue(i);
            inputs.Enqueue(j);

            (int output, _) = CalculateOutputSignal(integers, inputs);

            return output == 1;
        }

        private int? FindFirstXForPointInRowAffectedByTheTractorBeam(
            Dictionary<long, long> integers,
            int squareSize,
            int x,
            int y
        )
        {
            bool isPointAffectedByTheTractorBeam = false;
            // Find first point in current row which is affected by the tractor beam
            while (!isPointAffectedByTheTractorBeam)
            {
                isPointAffectedByTheTractorBeam = IsPointAffectedByTheTractorBeam(
                    new Dictionary<long, long>(integers), x, y);

                if (!isPointAffectedByTheTractorBeam)
                {
                    x++;
                }

                // If x and y diff is larger than square side required square is not possible
                if (x - y > squareSize)
                {
                    return null;
                }
            }

            return x;
        }

        private (int, bool) CalculateOutputSignal(Dictionary<long, long> integers, Queue<int> inputs)
        {
            int outputSignal = -1;

            long i = 0;
            long relativeBase = 0;

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
                            integers[firstParameter] = inputs.Count > 0 ? inputs.Dequeue() : 0;
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
