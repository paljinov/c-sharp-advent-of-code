using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2019.Day25
{
    public class Ship
    {
        private const int INSTRUCTION_MAX_ITERATIONS = 10000;

        private const int ASCII_NEWLINE = 10;

        private const string COMMAND = "Command?";

        private const string DOORS = "Doors here lead";

        private const string ITEMS = "Items here";

        private const string TAKE_ITEM = "take";

        private const string INFINITE_LOOP_ITEM = "infinite loop";

        public long FindThePasswordForTheMainAirlock(long[] integersArray)
        {
            Dictionary<long, long> integers = InitIntegersMemory(integersArray);
            Queue<int> inputs = new Queue<int>();

            // IntCode program current index and relative base value
            long index = 0;
            long relativeBase = 0;

            long? password = MoveDroid(
                integers,
                inputs,
                index,
                relativeBase,
                new HashSet<string>(),
                new HashSet<string>()
            );

            return password ?? 0;
        }

        private long? MoveDroid(
            Dictionary<long, long> integers,
            Queue<int> inputs,
            long index,
            long relativeBase,
            HashSet<string> takenItems,
            HashSet<string> statesCache
        )
        {
            Dictionary<long, long> integersCopy = integers.ToDictionary(i => i.Key, i => i.Value);
            string instruction = GetInstruction(integers, inputs, ref index, ref relativeBase);

            string room = GetRoom(instruction);

            string state = StringifyState(room, takenItems);
            // If state repeats
            if (statesCache.Contains(state))
            {
                return null;
            }
            statesCache.Add(state);

            long? password = GetPassword(instruction);
            // IF password is found
            if (password.HasValue)
            {
                return password;
            }

            // If game ended without needing to input command
            if (!instruction.Contains(COMMAND))
            {
                return null;
            }

            List<string> directions = GetChoices(instruction, DOORS);
            string item = GetChoices(instruction, ITEMS).FirstOrDefault();

            foreach (string direction in directions)
            {
                // Take item if not taken already and move droid
                if (!string.IsNullOrEmpty(item) && !takenItems.Contains(item))
                {
                    password = TakeItemAndMoveDroid(
                        integers.ToDictionary(i => i.Key, i => i.Value),
                        new Queue<int>(inputs),
                        index,
                        relativeBase,
                        takenItems.ToHashSet(),
                        statesCache,
                        direction,
                        item
                    );

                    if (password != null)
                    {
                        break;
                    }
                }

                password = JustMoveDroid(
                    integers.ToDictionary(i => i.Key, i => i.Value),
                    new Queue<int>(inputs),
                    index,
                    relativeBase,
                    takenItems.ToHashSet(),
                    statesCache,
                    direction
                );

                if (password != null)
                {
                    break;
                }
            }

            return password;
        }

        private string GetInstruction(
            Dictionary<long, long> integers,
            Queue<int> inputs,
            ref long index,
            ref long relativeBase
        )
        {
            Dictionary<long, long> previousIntegers = integers.ToDictionary(i => i.Key, i => i.Value);
            Queue<int> previousInputs = new Queue<int>(inputs);
            long previousIndex = index;
            long previousRelativeBase = relativeBase;

            int output;
            bool halted = false;

            StringBuilder instruction = new StringBuilder();

            while (!halted)
            {
                (output, halted) = CalculateOutputSignal(integers, inputs, ref index, ref relativeBase);

                // If instruction didn't finish
                if (!halted)
                {
                    instruction.Append((char)output);

                    if (output == ASCII_NEWLINE)
                    {
                        previousIntegers = integers.ToDictionary(i => i.Key, i => i.Value);
                        previousInputs = new Queue<int>(inputs);
                        previousIndex = index;
                        previousRelativeBase = relativeBase;

                        if (instruction.ToString().Contains(COMMAND))
                        {
                            halted = true;
                        }
                    }
                }
                // If instruction finished
                else
                {
                    integers.Clear();
                    foreach (KeyValuePair<long, long> previousInteger in previousIntegers)
                    {
                        integers[previousInteger.Key] = previousInteger.Value;
                    }

                    inputs.Clear();
                    foreach (int previousInput in previousInputs)
                    {
                        inputs.Enqueue(previousInput);
                    }

                    index = previousIndex;
                    relativeBase = previousRelativeBase;
                }
            }


            return instruction.ToString();
        }

        private long? GetPassword(string instruction)
        {
            long? password = null;

            Regex passwordRegex = new Regex(@"by\styping\s(\d+)\son\sthe\skeypad");

            Match passwordMatch = passwordRegex.Match(instruction);
            if (passwordMatch.Success)
            {
                GroupCollection passwordGroups = passwordMatch.Groups;
                password = long.Parse(passwordGroups[1].Value);
            }

            return password;
        }

        private string GetRoom(string instruction)
        {
            Regex roomRegex = new Regex(@"==\s(.+)?\s==");

            Match roomMatch = roomRegex.Match(instruction);
            GroupCollection roomGroups = roomMatch.Groups;

            string room = roomGroups[1].Value;

            return room;
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

        private long? TakeItemAndMoveDroid(
            Dictionary<long, long> integers,
            Queue<int> inputs,
            long index,
            long relativeBase,
            HashSet<string> takenItems,
            HashSet<string> statesCache,
            string direction,
            string item
        )
        {
            if (item == INFINITE_LOOP_ITEM)
            {
                return null;
            }

            List<int> takeItemCommandAsciiInput = ConvertInstructionToAsciiInputs($"{TAKE_ITEM} {item}");
            EnqueueCommandToInputs(inputs, takeItemCommandAsciiInput);

            string instruction = GetInstruction(integers, inputs, ref index, ref relativeBase);
            // If instruction doesn't contain command item did something bad to us
            if (!instruction.Contains(COMMAND))
            {
                return null;
            }

            HashSet<string> expandedTakenItems = takenItems.ToHashSet();
            expandedTakenItems.Add(item);

            List<int> moveCommandAsciiInput = ConvertInstructionToAsciiInputs(direction);
            EnqueueCommandToInputs(inputs, moveCommandAsciiInput);

            long? password = MoveDroid(integers, inputs, index, relativeBase, expandedTakenItems, statesCache);

            return password;
        }

        private long? JustMoveDroid(
            Dictionary<long, long> integers,
            Queue<int> inputs,
            long index,
            long relativeBase,
            HashSet<string> takenItems,
            HashSet<string> statesCache,
            string direction
        )
        {
            List<int> moveCommandAsciiInput = ConvertInstructionToAsciiInputs(direction);
            EnqueueCommandToInputs(inputs, moveCommandAsciiInput);

            long? password = MoveDroid(integers, inputs, index, relativeBase, takenItems, statesCache);

            return password;
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

        private string StringifyState(string room, HashSet<string> items)
        {
            List<string> sortedItems = items.ToList();
            sortedItems.Sort();
            string itemsString = string.Join(",", sortedItems);

            return $"({room}),({itemsString})";
        }

        private void EnqueueCommandToInputs(Queue<int> queue, List<int> list)
        {
            foreach (int input in list)
            {
                queue.Enqueue(input);
            }

            queue.Enqueue(ASCII_NEWLINE);
        }

        private (int, bool) CalculateOutputSignal(
            Dictionary<long, long> integers,
            Queue<int> inputs,
            ref long i,
            ref long relativeBase
        )
        {
            int outputSignal = -1;

            int iterations = 0;
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

                iterations++;
                if (iterations >= INSTRUCTION_MAX_ITERATIONS)
                {
                    return (0, true);
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
