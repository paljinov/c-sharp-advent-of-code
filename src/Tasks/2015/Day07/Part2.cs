/*
--- Part Two ---

Now, take the signal you got on wire a, override wire b to that signal, and
reset the other wires (including wire a). What new signal is ultimately provided
to wire a?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day7
{
    public class Part2 : ITask<int>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly WireSignals wireSignals;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            wireSignals = new WireSignals();
        }

        public int Solution(string input)
        {
            Dictionary<string, string> instructions = instructionsRepository.GetInstructions(input);
            SortedDictionary<string, ushort> wireSignals = this.wireSignals.CalculateWireSignals(instructions);

            wireSignals.TryGetValue("a", out ushort wireA);
            instructions["b"] = $"{wireA} -> b";

            wireSignals = this.wireSignals.CalculateWireSignals(instructions);
            wireSignals.TryGetValue("a", out wireA);

            return wireA;
        }
    }
}
