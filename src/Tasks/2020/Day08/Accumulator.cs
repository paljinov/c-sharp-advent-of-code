using System.Collections.Generic;

namespace App.Tasks.Year2020.Day8
{
    public class Accumulator
    {
        public int AccumulatorValueBeforeAnyInstructionIsExecutedSecondTime(List<Instruction> instructions)
        {
            int accumulatorValue = 0;

            bool instructionExecutedSecondTime = false;
            bool[] executedInstructions = new bool[instructions.Count];

            int i = 0;
            while (!instructionExecutedSecondTime)
            {
                if (executedInstructions[i])
                {
                    instructionExecutedSecondTime = true;
                }
                else
                {
                    executedInstructions[i] = true;

                    Instruction instruction = instructions[i];
                    switch (instruction.Operation)
                    {
                        case Operation.Accumulator:
                            accumulatorValue += instruction.Argument;
                            i++;
                            break;
                        case Operation.Jump:
                            i += instruction.Argument;
                            break;
                        default:
                            i++;
                            break;
                    }
                }
            }

            return accumulatorValue;
        }

        public int AccumulatorValueAfterProgramTerminates(List<Instruction> instructions)
        {
            int accumulatorValue = 0;

            List<int> changedOperationJumpOrNop = new List<int>();
            bool terminatedNormally = false;

            while (!terminatedNormally)
            {
                bool instructionExecutedSecondTime = false;
                bool[] executedInstructions = new bool[instructions.Count];
                bool operationJumpOrNopChanged = false;
                accumulatorValue = 0;

                int i = 0;
                while (!instructionExecutedSecondTime && !terminatedNormally)
                {
                    if (executedInstructions[i])
                    {
                        instructionExecutedSecondTime = true;
                    }
                    else
                    {
                        executedInstructions[i] = true;

                        Instruction instruction = new Instruction
                        {
                            Operation = instructions[i].Operation,
                            Argument = instructions[i].Argument
                        };

                        // If operation wasn't already changed
                        if (!operationJumpOrNopChanged && !changedOperationJumpOrNop.Contains(i))
                        {
                            if (instruction.Operation == Operation.Jump)
                            {
                                instruction.Operation = Operation.NoOperation;
                                operationJumpOrNopChanged = true;
                                changedOperationJumpOrNop.Add(i);

                            }
                            else if (instruction.Operation == Operation.NoOperation)
                            {
                                instruction.Operation = Operation.Jump;
                                operationJumpOrNopChanged = true;
                                changedOperationJumpOrNop.Add(i);
                            }
                        }

                        switch (instruction.Operation)
                        {
                            case Operation.Accumulator:
                                accumulatorValue += instruction.Argument;
                                i++;
                                break;
                            case Operation.Jump:
                                i += instruction.Argument;
                                break;
                            default:
                                i++;
                                break;
                        }
                    }

                    if (i == instructions.Count)
                    {
                        terminatedNormally = true;
                    }
                }
            }

            return accumulatorValue;
        }
    }
}
