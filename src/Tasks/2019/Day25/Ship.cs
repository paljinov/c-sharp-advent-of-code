using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.Tasks.Year2019.Day25
{
    public class Ship
    {
        private const int ASCII_NEWLINE = 10;

        private const string COMMAND = "Command?";

        private const string DOORS = "Doors here lead";

        private const string ITEMS = "Items here";

        private const string TAKE = "take";

        private const string DROP = "drop";

        private const string LIST_ITEMS = "inv";

        private readonly Random random;

        public Ship()
        {
            random = new Random();
        }

        public long FindThePasswordForTheMainAirlock(long[] integersArray)
        {
            StringBuilder password = new StringBuilder();

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);
            Queue<int> inputs = new Queue<int>();

            int output;
            bool halted = false;

            // IntCode program current index and relative base value
            long index = 0;
            long relativeBase = 0;

            StringBuilder instructionSb = new StringBuilder();

            while (!halted)
            {
                (output, halted) = CalculateOutputSignal(integers, inputs, ref index, ref relativeBase);
                instructionSb.Append((char)output);

                if (instructionSb.Length >= COMMAND.Length
                    && instructionSb.ToString(instructionSb.Length - COMMAND.Length, COMMAND.Length) == COMMAND)
                {
                    string instruction = instructionSb.ToString();
                    instructionSb.Clear();
                    // Console.WriteLine(instruction);

                    List<string> answers = new List<string>();
                    if (instruction.Contains(DOORS))
                    {
                        answers = GetChoices(instruction, DOORS);
                    }
                    else if (instruction.Contains(ITEMS))
                    {
                        answers = GetChoices(instruction, ITEMS);
                        for (int i = 0; i < answers.Count; i++)
                        {
                            answers[i] = $"{TAKE} {answers[i]}";
                        }
                    }
                    else
                    {

                    }

                    if (answers.Count > 0)
                    {
                        int answerIndex = random.Next(answers.Count);
                        string commandInput = answers[answerIndex];
                        List<int> commandAsciiInput = ConvertInstructionToAsciiInputs(commandInput);
                        inputs = new Queue<int>(commandAsciiInput);
                        inputs.Enqueue(ASCII_NEWLINE);
                    }
                }
            }

            return long.Parse(password.ToString());
        }

        private List<string> GetChoices(string instruction, string choiceType)
        {
            List<string> choices = new List<string>();

            StringReader strReader = new StringReader(instruction);
            string line = strReader.ReadLine();

            while (line != COMMAND)
            {
                if (line.Contains(choiceType))
                {
                    line = strReader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        choices.Add(line.TrimStart(' ', '-'));
                        line = strReader.ReadLine();
                    }
                }

                line = strReader.ReadLine();
            }

            return choices;
        }

        private List<int> ConvertInstructionToAsciiInputs(string instruction)
        {
            List<int> asciiInput = new List<int>();
            for (int i = 0; i < instruction.Length; i++)
            {
                asciiInput.Add(instruction[i]);
            }

            return asciiInput;
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
