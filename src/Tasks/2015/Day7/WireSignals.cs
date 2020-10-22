using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day7
{
    class WireSignals
    {
        /// <summary>
        /// Calculate wire signals.
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public SortedDictionary<string, ushort> CalculateWireSignals(Dictionary<string, string> instructions)
        {
            SortedDictionary<string, ushort> wireSignals = new SortedDictionary<string, ushort>();

            foreach (KeyValuePair<string, string> instruction in instructions)
            {
                if (!wireSignals.ContainsKey(instruction.Key))
                {
                    (string wire, ushort signal) = CalculateWireSignal(instruction.Value, instructions, wireSignals);
                    wireSignals.Add(wire, signal);
                }
            }

            return wireSignals;
        }

        /// <summary>
        /// Calculate wire signal.
        /// </summary>
        /// <param name="instructionString"></param>
        /// <param name="instructions"></param>
        /// <param name="wireSignals"></param>
        /// <returns>Wire name and signal.</returns>
        private (string, ushort) CalculateWireSignal(
            string instructionString,
            Dictionary<string, string> instructions,
            SortedDictionary<string, ushort> wireSignals
        )
        {
            Regex assignmentRegex = new Regex(@"^(\w+) -> (\w+)$");
            Match assignmentMatch = assignmentRegex.Match(instructionString);

            // Assignment
            if (assignmentMatch.Success)
            {
                GroupCollection groups = assignmentMatch.Groups;
                ushort wire = GetWireSignal(groups[1].Value, instructions, wireSignals);

                return (groups[2].Value, wire);
            }
            // Bitwise complement
            else if (instructionString.Contains("NOT"))
            {
                Regex bitwiseComplementRegex = new Regex(@"^NOT (\w+) -> (\w+)$");
                Match bitwiseComplementMatch = bitwiseComplementRegex.Match(instructionString);

                GroupCollection groups = bitwiseComplementMatch.Groups;
                ushort wire = GetWireSignal(groups[1].Value, instructions, wireSignals);

                return (groups[2].Value, (ushort)(~wire));
            }
            // Left or right SHIFT
            else if (instructionString.Contains("SHIFT"))
            {
                Regex shiftRegex = new Regex(@"^(\w+) (\w+) (\w+) -> (\w+)$");
                Match shiftMatch = shiftRegex.Match(instructionString);

                GroupCollection groups = shiftMatch.Groups;
                ushort wire = GetWireSignal(groups[1].Value, instructions, wireSignals);

                ushort shiftCount = ushort.Parse(groups[3].Value);
                if (groups[2].Value.Equals("LSHIFT"))
                {
                    return (groups[4].Value, (ushort)(wire << shiftCount));
                }
                else
                {
                    return (groups[4].Value, (ushort)(wire >> shiftCount));
                }
            }
            // Bitwise AND or OR
            else
            {
                Regex bitwiseRegex = new Regex(@"^(\w+) (\w+) (\w+) -> (\w+)$");
                Match bitwiseMatch = bitwiseRegex.Match(instructionString);

                GroupCollection groups = bitwiseMatch.Groups;
                ushort firstWire = GetWireSignal(groups[1].Value, instructions, wireSignals);
                ushort secondWire = GetWireSignal(groups[3].Value, instructions, wireSignals);

                if (groups[2].Value == "AND")
                {
                    return (groups[4].Value, (ushort)(firstWire & secondWire));
                }
                else
                {
                    return (groups[4].Value, (ushort)(firstWire | secondWire));
                }
            }
        }

        /// <summary>
        /// Recives wire name or signal. If input is wire signal, returns
        /// result. If input is wire name, first tries to fetch it from already
        /// pre-calculated wire signals, and if still not found wire signal is
        /// finally calculated - this is actually recursion.
        /// </summary>
        /// <param name="wireInput"></param>
        /// <param name="instructions"></param>
        /// <param name="wireSignals"></param>
        /// <returns>Wire signal.</returns>
        private ushort GetWireSignal(
            string wireInput,
            Dictionary<string, string> instructions,
            SortedDictionary<string, ushort> wireSignals
        )
        {
            // Check is wire input signal (value integer)
            if (ushort.TryParse(wireInput, out ushort wireSignal))
            {
                return wireSignal;
            }

            // Check if wire signal is already calculated
            if (wireSignals.TryGetValue(wireInput, out wireSignal))
            {
                return wireSignal;
            }

            // Calculate wire signal (recursion)
            wireSignal = CalculateWireSignal(instructions[wireInput], instructions, wireSignals).Item2;
            wireSignals.Add(wireInput, wireSignal);

            return wireSignal;
        }
    }
}
