using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day25
{
    public class BlueprintRepository
    {
        public char GetStartState(string input)
        {
            char startState = 'A';

            Regex startStateRegex = new Regex(@"^Begin\sin\sstate\s(\w)\.$");

            string[] instructionsLines = GetInstructionsLines(input);
            foreach (string line in instructionsLines)
            {
                Match startStateMatch = startStateRegex.Match(line);
                if (startStateMatch.Success)
                {
                    startState = startStateMatch.Groups[1].Value[0];
                }
            }

            return startState;
        }

        public int GetStepsAfterWhichDiagnosticChecksumIsPerformed(string input)
        {
            int steps = 0;

            Regex stepsRegex = new Regex(@"^Perform\sa\sdiagnostic\schecksum\safter\s(\d+)\ssteps\.$");

            string[] instructionsLines = GetInstructionsLines(input);
            foreach (string line in instructionsLines)
            {
                Match stepsMatch = stepsRegex.Match(line);
                if (stepsMatch.Success)
                {
                    steps = int.Parse(stepsMatch.Groups[1].Value);
                }
            }

            return steps;
        }

        public Dictionary<char, ConditionInstructions> GetInstructions(string input)
        {
            Dictionary<char, ConditionInstructions> instructions = new Dictionary<char, ConditionInstructions>();

            Regex stateRegex = new Regex(@"^In\sstate\s(\w):$");
            Regex currentValueRegex = new Regex(@"If\sthe\scurrent\svalue\sis\s(0|1):$");
            Regex writeValueRegex = new Regex(@"-\sWrite\sthe\svalue\s(0|1)\.$");
            Regex moveSlotRegex = new Regex(@"-\sMove\sone\sslot\sto\sthe\s(left|right)\.$");
            Regex continueWithStateRegex = new Regex(@"-\sContinue\swith\sstate\s(\w)\.$");

            string[] instructionsLines = GetInstructionsLines(input);

            char? state = null;
            int? currentValue = null;
            int? writeValue = null;
            string moveSlot = null;
            char? continueWithState = null;

            foreach (string line in instructionsLines)
            {
                Match stateMatch = stateRegex.Match(line);
                if (stateMatch.Success)
                {
                    state = stateMatch.Groups[1].Value[0];
                }

                Match currentValueMatch = currentValueRegex.Match(line);
                if (currentValueMatch.Success)
                {
                    currentValue = int.Parse(currentValueMatch.Groups[1].Value);
                }

                Match writeValueMatch = writeValueRegex.Match(line);
                if (writeValueMatch.Success)
                {
                    writeValue = int.Parse(writeValueMatch.Groups[1].Value);
                }

                Match moveSlotMatch = moveSlotRegex.Match(line);
                if (moveSlotMatch.Success)
                {
                    moveSlot = moveSlotMatch.Groups[1].Value;
                }

                Match continueWithStateMatch = continueWithStateRegex.Match(line);
                if (continueWithStateMatch.Success)
                {
                    continueWithState = continueWithStateMatch.Groups[1].Value[0];
                }

                if (state.HasValue && currentValue.HasValue && writeValue.HasValue
                    && !string.IsNullOrEmpty(moveSlot) && continueWithState.HasValue)
                {
                    Instruction instruction = new Instruction
                    {
                        WriteValue = writeValue.Value,
                        MoveSlot = moveSlot == "left" ? Direction.LEFT : Direction.RIGHT,
                        ContinueWithState = continueWithState.Value
                    };

                    if (!instructions.ContainsKey(state.Value))
                    {
                        instructions[state.Value] = new ConditionInstructions { };
                    }

                    if (currentValue == 0)
                    {
                        instructions[state.Value].CurrentValueIsZero = instruction;
                    }
                    else
                    {
                        instructions[state.Value].CurrentValueIsOne = instruction;
                    }

                    if (instructions[state.Value].CurrentValueIsZero is not null
                        && instructions[state.Value].CurrentValueIsOne is not null)
                    {
                        state = null;
                    }

                    currentValue = null;
                    writeValue = null;
                    moveSlot = null;
                    continueWithState = null;
                }
            }

            return instructions;
        }

        private string[] GetInstructionsLines(string input)
        {
            string[] instructions = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return instructions;
        }
    }
}
