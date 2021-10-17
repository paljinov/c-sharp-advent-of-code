using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day16
{
    public class SamplesRepository
    {
        public Sample[] GetSamples(string input)
        {
            string[] samplesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            List<Sample> samples = new List<Sample>();

            Regex beforeRegex = new Regex(@"^Before:\s\[(\d),\s(\d),\s(\d),\s(\d)\]$");
            Regex instructionRegex = new Regex(@"^(\d+)\s(\d)\s(\d)\s(\d)$");
            Regex afterRegex = new Regex(@"^After:\s+\[(\d),\s(\d),\s(\d),\s(\d)\]$");

            Sample sample = new Sample { };

            for (int i = 0; i < samplesString.Length; i++)
            {
                string sampleString = samplesString[i].Trim();

                Match beforeMatch = beforeRegex.Match(sampleString);
                Match instructionMatch = instructionRegex.Match(sampleString);
                Match afterMatch = afterRegex.Match(sampleString);

                if (beforeMatch.Success)
                {
                    sample = new Sample { };

                    sample.Before = new RegistersValues
                    {
                        Zero = int.Parse(beforeMatch.Groups[1].Value),
                        One = int.Parse(beforeMatch.Groups[2].Value),
                        Two = int.Parse(beforeMatch.Groups[3].Value),
                        Three = int.Parse(beforeMatch.Groups[4].Value)
                    };
                }
                else if (instructionMatch.Success)
                {
                    sample.Instruction = new Instruction
                    {
                        Opcode = int.Parse(instructionMatch.Groups[1].Value),
                        InputA = int.Parse(instructionMatch.Groups[2].Value),
                        InputB = int.Parse(instructionMatch.Groups[3].Value),
                        OutputC = int.Parse(instructionMatch.Groups[4].Value)
                    };
                }
                else if (afterMatch.Success)
                {
                    sample.After = new RegistersValues
                    {
                        Zero = int.Parse(afterMatch.Groups[1].Value),
                        One = int.Parse(afterMatch.Groups[2].Value),
                        Two = int.Parse(afterMatch.Groups[3].Value),
                        Three = int.Parse(afterMatch.Groups[4].Value)
                    };

                    samples.Add(sample);
                }
            }

            return samples.ToArray();
        }
    }
}
