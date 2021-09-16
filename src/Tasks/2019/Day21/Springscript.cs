using System.Collections.Generic;

namespace App.Tasks.Year2019.Day21
{
    public class Springscript
    {
        private const int ASCII_NEWLINE = 10;

        private const string WALK = "WALK";

        private const string RUN = "RUN";

        public int CalculateAmountOfReportedHullDamage(long[] integersArray)
        {
            string[] springscript = new string[] {
                $"{SringscriptInstruction.NOT} {Register.A} {Register.J}",
                $"{SringscriptInstruction.NOT} {Register.J} {Register.J}",
                $"{SringscriptInstruction.AND} {Register.B} {Register.J}",
                $"{SringscriptInstruction.AND} {Register.C} {Register.J}",
                $"{SringscriptInstruction.NOT} {Register.J} {Register.J}",
                $"{SringscriptInstruction.AND} {Register.D} {Register.J}",
                WALK
            };

            Queue<int> inputs = GetInputs(springscript);
            int amountOfReportedHullDamage = DoCalculateAmountOfReportedHullDamage(integersArray, inputs);

            return amountOfReportedHullDamage;
        }

        public int CalculateAmountOfReportedHullDamageForExtendedSensorMode(long[] integersArray)
        {
            string[] springscript = new string[] {
                $"{SringscriptInstruction.NOT} {Register.C} {Register.J}",
                $"{SringscriptInstruction.AND} {Register.H} {Register.J}",
                $"{SringscriptInstruction.NOT} {Register.B} {Register.T}",
                $"{SringscriptInstruction.OR} {Register.T} {Register.J}",
                $"{SringscriptInstruction.NOT} {Register.A} {Register.T}",
                $"{SringscriptInstruction.OR} {Register.T} {Register.J}",
                $"{SringscriptInstruction.AND} {Register.D} {Register.J}",
                RUN
            };

            Queue<int> inputs = GetInputs(springscript);
            int amountOfReportedHullDamage = DoCalculateAmountOfReportedHullDamage(integersArray, inputs);

            return amountOfReportedHullDamage;
        }

        private int DoCalculateAmountOfReportedHullDamage(long[] integersArray, Queue<int> inputs)
        {
            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            int outputSignal = 0;
            bool halted = false;

            // IntCode program current index and relative base value
            long index = 0;
            long relativeBase = 0;

            while (!halted)
            {
                (outputSignal, halted) = CalculateOutputSignal(integers, inputs, ref index, ref relativeBase);
            }

            return outputSignal;
        }

        private Queue<int> GetInputs(string[] springscript)
        {
            Queue<int> inputs = new Queue<int>();

            for (int i = 0; i < springscript.Length; i++)
            {
                for (int j = 0; j < springscript[i].Length; j++)
                {
                    inputs.Enqueue(springscript[i][j]);
                }

                inputs.Enqueue(ASCII_NEWLINE);
            }

            return inputs;
        }

        private (int, bool) CalculateOutputSignal(
            Dictionary<long, long> integers,
            Queue<int> inputs,
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
