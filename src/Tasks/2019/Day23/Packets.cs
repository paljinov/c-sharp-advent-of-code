using System.Collections.Generic;

namespace App.Tasks.Year2019.Day23
{
    public class Packets
    {
        private const int TOTAL_COMPUTERS = 50;

        public long FindYValueOfTheFirstPacketSentToWantedAddress(long[] integersArray, int wantedAddress)
        {
            int? address = 0;
            long? x;
            long? y = 0;

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);
            List<Computer> computers = new List<Computer>();

            for (int networkAddress = 0; networkAddress < TOTAL_COMPUTERS; networkAddress++)
            {
                Queue<long> inputs = new Queue<long>();
                inputs.Enqueue(networkAddress);

                Computer computer = new Computer
                {
                    NetworkAddress = networkAddress,
                    Integers = new Dictionary<long, long>(integers),
                    Inputs = inputs,
                    Index = 0,
                    RelativeBase = 0
                };

                computers.Add(computer);
            }

            while (address != wantedAddress)
            {
                foreach (Computer computer in computers)
                {
                    // IntCode program current index and relative base value
                    long index = computer.Index;
                    long relativeBase = computer.RelativeBase;

                    address = (int?)CalculateOutputSignal(
                        computer.Integers, computer.Inputs, ref index, ref relativeBase);

                    if (address.HasValue)
                    {
                        x = CalculateOutputSignal(computer.Integers, computer.Inputs, ref index, ref relativeBase);
                        y = CalculateOutputSignal(computer.Integers, computer.Inputs, ref index, ref relativeBase);

                        if (address == wantedAddress)
                        {
                            break;
                        }

                        computers[address.Value].Inputs.Enqueue(x.Value);
                        computers[address.Value].Inputs.Enqueue(y.Value);
                    }

                    computer.Index = index;
                    computer.RelativeBase = relativeBase;
                }
            }

            return y.Value;
        }

        public int FindFirstYValueDeliveredByTheNatToTheComputerAtAddressZeroTwiceInARow(long[] integersArray)
        {
            return integersArray.Length;
        }

        private long? CalculateOutputSignal(
            Dictionary<long, long> integers,
            Queue<long> inputs,
            ref long i,
            ref long relativeBase
        )
        {
            long? outputSignal = null;

            while (integers[i] != (int)Operation.Halt)
            {
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
                            integers[firstParameter] = inputs.Count > 0 ? inputs.Dequeue() : -1;
                            return null;
                        }
                        else if (operation == (int)Operation.Output)
                        {
                            outputSignal = firstParameter;
                            return outputSignal;
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

            return outputSignal;
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
