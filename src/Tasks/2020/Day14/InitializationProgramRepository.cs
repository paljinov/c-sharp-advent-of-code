using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day14
{
    public class InitializationProgramRepository
    {
        public List<ProgramSequence> GetInitializationProgram(string input)
        {
            List<ProgramSequence> initializationProgram = new List<ProgramSequence>();

            Regex maskRegex = new Regex(@"^mask\s=\s([01X]{36})$");
            Regex memoryAddressRegex = new Regex(@"^mem\[(\d+)\]\s=\s(\d+)$");

            string[] initializationProgramString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in initializationProgramString)
            {
                Match maskMatch = maskRegex.Match(line);
                if (maskMatch.Success)
                {
                    GroupCollection maskGroups = maskMatch.Groups;

                    ProgramSequence programSequence = new ProgramSequence
                    {
                        Bitmask = maskGroups[1].Value,
                        MemoryAddresses = new List<MemoryAddress>()
                    };

                    initializationProgram.Add(programSequence);
                }
                else
                {
                    Match memoryAddressMatch = memoryAddressRegex.Match(line);
                    GroupCollection memoryAddressGroups = memoryAddressMatch.Groups;

                    MemoryAddress memoryAddress = new MemoryAddress
                    {
                        Address = int.Parse(memoryAddressGroups[1].Value),
                        Value = int.Parse(memoryAddressGroups[2].Value)
                    };

                    initializationProgram[^1].MemoryAddresses.Add(memoryAddress);
                }
            }

            return initializationProgram;
        }
    }
}
