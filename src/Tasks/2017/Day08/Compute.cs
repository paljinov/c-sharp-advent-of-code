using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day8
{
    public class Compute
    {
        public int FindLargestValueInAnyRegister(List<Instruction> instructions)
        {
            Dictionary<string, int> registers = InitializeRegisters(instructions);

            foreach (Instruction instruction in instructions)
            {
                bool conditionPass = false;

                switch (instruction.ComparisonOperator)
                {
                    case ComparisonOperator.Equal:
                        if (registers[instruction.ConditionRegister] == instruction.ConditionAmount)
                        {
                            conditionPass = true;
                        }
                        break;
                    case ComparisonOperator.NotEqual:
                        if (registers[instruction.ConditionRegister] != instruction.ConditionAmount)
                        {
                            conditionPass = true;
                        }
                        break;
                    case ComparisonOperator.LessThan:
                        if (registers[instruction.ConditionRegister] < instruction.ConditionAmount)
                        {
                            conditionPass = true;
                        }
                        break;
                    case ComparisonOperator.LessThanOrEqual:
                        if (registers[instruction.ConditionRegister] <= instruction.ConditionAmount)
                        {
                            conditionPass = true;
                        }
                        break;
                    case ComparisonOperator.GreaterThan:
                        if (registers[instruction.ConditionRegister] > instruction.ConditionAmount)
                        {
                            conditionPass = true;
                        }
                        break;
                    case ComparisonOperator.GreaterThanOrEqual:
                        if (registers[instruction.ConditionRegister] >= instruction.ConditionAmount)
                        {
                            conditionPass = true;
                        }
                        break;
                }

                if (conditionPass)
                {
                    if (instruction.InstructionType == InstructionType.Increase)
                    {
                        registers[instruction.Register] += instruction.Amount;
                    }
                    else
                    {
                        registers[instruction.Register] -= instruction.Amount;
                    }
                }
            }

            int largestValueInAnyRegister = registers.Values.Max();

            return largestValueInAnyRegister;
        }

        private Dictionary<string, int> InitializeRegisters(List<Instruction> instructions)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            foreach (Instruction instruction in instructions)
            {
                if (!registers.ContainsKey(instruction.Register))
                {
                    registers[instruction.Register] = 0;
                }

                if (!registers.ContainsKey(instruction.ConditionRegister))
                {
                    registers[instruction.ConditionRegister] = 0;
                }
            }

            return registers;
        }
    }
}
