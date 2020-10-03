/*
--- Part Two ---

Now, take the signal you got on wire a, override wire b to that signal, and
reset the other wires (including wire a). What new signal is ultimately provided
to wire a?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day7
{
    class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            Dictionary<string, string> instructions = InstructionsRepository.GetInstructions(input);
            SortedDictionary<string, ushort> wireSignals = (new WireSignals()).CalculateWireSignals(instructions);

            wireSignals.TryGetValue("a", out ushort wireA);
            instructions["b"] = $"{wireA} -> b";

            wireSignals = (new WireSignals()).CalculateWireSignals(instructions);
            wireSignals.TryGetValue("a", out wireA);

            return wireA;
        }
    }
}
