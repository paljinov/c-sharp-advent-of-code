using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day8
{
    public class InstructionsRepository
    {
        public List<Instruction> GetInstructions(string input)
        {
            List<Instruction> instructions = new List<Instruction>();

            string[] instructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex instructionRegex = new Regex(@"^(\w+)\s(inc|dec)\s(-?\d+)\sif\s(\w+)\s([=!<>]+)\s(-?\d+)$");

            foreach (string instructionString in instructionsString)
            {
                Match instructionMatch = instructionRegex.Match(instructionString);
                GroupCollection instructionGroups = instructionMatch.Groups;

                InstructionType instructionType;
                switch (instructionGroups[2].Value)
                {
                    case "inc":
                        instructionType = InstructionType.Increase;
                        break;
                    default:
                        instructionType = InstructionType.Decrease;
                        break;
                }

                ComparisonOperator comparisonOperator;
                switch (instructionGroups[5].Value)
                {
                    case "!=":
                        comparisonOperator = ComparisonOperator.NotEqual;
                        break;
                    case "<":
                        comparisonOperator = ComparisonOperator.LessThan;
                        break;
                    case "<=":
                        comparisonOperator = ComparisonOperator.LessThanOrEqual;
                        break;
                    case ">":
                        comparisonOperator = ComparisonOperator.GreaterThan;
                        break;
                    case ">=":
                        comparisonOperator = ComparisonOperator.GreaterThanOrEqual;
                        break;
                    default:
                        comparisonOperator = ComparisonOperator.Equal;
                        break;
                }

                Instruction instruction = new Instruction
                {
                    Register = instructionGroups[1].Value,
                    InstructionType = instructionType,
                    Amount = int.Parse(instructionGroups[3].Value),
                    ConditionRegister = instructionGroups[4].Value,
                    ComparisonOperator = comparisonOperator,
                    ConditionAmount = int.Parse(instructionGroups[6].Value),
                };

                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}
