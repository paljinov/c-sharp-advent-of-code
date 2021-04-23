using System.Collections.Generic;

namespace App.Tasks.Year2017.Day25
{
    public class TuringMachine
    {
        public int CalculateDiagnosticChecksum(
            char startState,
            int steps,
            Dictionary<char, ConditionInstructions> instructions
        )
        {
            char currentState = startState;
            int currentSlotPosition = 0;
            HashSet<int> slotsWithValueOnePositions = new HashSet<int>();

            for (int step = 0; step < steps; step++)
            {
                Instruction instruction;

                // If current slot value is 1
                if (slotsWithValueOnePositions.Contains(currentSlotPosition))
                {
                    instruction = instructions[currentState].CurrentValueIsOne;
                }
                // If current slot value is 0
                else
                {
                    instruction = instructions[currentState].CurrentValueIsZero;
                }

                // Write value 1 for current slot
                if (instruction.WriteValue == 1)
                {
                    slotsWithValueOnePositions.Add(currentSlotPosition);
                }
                // Write value 0 for current slot
                else
                {
                    slotsWithValueOnePositions.Remove(currentSlotPosition);
                }

                // Move Slot
                currentSlotPosition += instruction.MoveSlot == Direction.LEFT ? -1 : 1;

                // Update current state
                currentState = instruction.ContinueWithState;
            }

            int diagnosticChecksum = slotsWithValueOnePositions.Count;

            return diagnosticChecksum;
        }
    }
}
