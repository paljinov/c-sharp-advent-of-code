using System.Collections.Generic;

namespace App.Tasks.Year2019.Day13
{
    public class ArcadeGame
    {
        public int CountBlockTiles(long[] integersArray)
        {
            int blockTiles = 0;

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            int output;
            bool halted = false;

            // IntCode program current index and relative base value
            long index = 0;
            long relativeBase = 0;

            while (!halted)
            {
                Tile tile = new Tile();
                for (int i = 0; i < 3; i++)
                {
                    (output, halted) = CalculateOutputSignal(integers, 0, ref index, ref relativeBase);

                    if (!halted)
                    {
                        if (i == 0)
                        {
                            tile.X = output;
                        }
                        else if (i == 1)
                        {
                            tile.Y = output;
                        }
                        else
                        {
                            tile.Id = output;
                        }
                    }
                }

                if (!halted && tile.Id == (int)TileType.Block)
                {
                    blockTiles++;
                }
            }

            return blockTiles;
        }

        public int CalculateScoreAfterTheLastBlockIsBroken(long[] integersArray)
        {
            int score = 0;

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            int ballX = 0;
            int horizontalPaddleX = 0;
            int output;
            bool halted = false;

            // IntCode program current index and relative base value
            long index = 0;
            long relativeBase = 0;

            while (!halted)
            {
                Tile tile = new Tile();
                for (int i = 0; i < 3; i++)
                {
                    int input = 0;
                    if (ballX > horizontalPaddleX)
                    {
                        input = 1;
                    }
                    else if (ballX < horizontalPaddleX)
                    {
                        input = -1;
                    }

                    (output, halted) = CalculateOutputSignal(integers, input, ref index, ref relativeBase);

                    if (!halted)
                    {
                        if (i == 0)
                        {
                            tile.X = output;
                        }
                        else if (i == 1)
                        {
                            tile.Y = output;
                        }
                        else
                        {
                            tile.Id = output;
                        }
                    }
                }

                if (!halted)
                {
                    if (tile.X == -1 && tile.Y == 0)
                    {
                        score = tile.Id;
                    }
                    else if (tile.Id == (int)TileType.HorizontalPaddle)
                    {
                        horizontalPaddleX = tile.X;
                    }
                    else if (tile.Id == (int)TileType.Ball)
                    {
                        ballX = tile.X;
                    }
                }
            }

            return score;
        }

        private (int, bool) CalculateOutputSignal(
            Dictionary<long, long> integers,
            int input,
            ref long i,
            ref long relativeBase
        )
        {
            int? output = null;

            while (integers[i] != (int)Operation.Halt)
            {
                if (output.HasValue)
                {
                    return (output.Value, false);
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
                            output = (int)firstParameter;
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

            return (0, true);
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
