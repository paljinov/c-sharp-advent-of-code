using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day19
{
    public class SamplesAndTestProgramRepository
    {
        private const int TOTAL_REGISTERS = 4;

        public Sample[] GetSamples(string input)
        {
            string samplesInput = ParseInput(input)[0];

            string[] samplesString = samplesInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
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
                    sample.Before = new int[TOTAL_REGISTERS]
                    {
                        int.Parse(beforeMatch.Groups[1].Value),
                        int.Parse(beforeMatch.Groups[2].Value),
                        int.Parse(beforeMatch.Groups[3].Value),
                        int.Parse(beforeMatch.Groups[4].Value)
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
                    sample.After = new int[TOTAL_REGISTERS]
                    {
                        int.Parse(afterMatch.Groups[1].Value),
                        int.Parse(afterMatch.Groups[2].Value),
                        int.Parse(afterMatch.Groups[3].Value),
                        int.Parse(afterMatch.Groups[4].Value)
                    };

                    samples.Add(sample);
                    sample = new Sample { };
                }
            }

            return samples.ToArray();
        }

        public int[][] GetTestProgram(string input)
        {
            string testProgramInput = ParseInput(input)[1];

            string[] testProgramString = testProgramInput
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int[][] testProgram = new int[testProgramString.Length][];

            Regex testProgramRegex = new Regex(@"^(\d+)\s(\d)\s(\d)\s(\d)$");
            for (int i = 0; i < testProgramString.Length; i++)
            {
                Match testProgramMatch = testProgramRegex.Match(testProgramString[i]);

                int[] instruction = new int[] {
                    int.Parse(testProgramMatch.Groups[1].Value),
                    int.Parse(testProgramMatch.Groups[2].Value),
                    int.Parse(testProgramMatch.Groups[3].Value),
                    int.Parse(testProgramMatch.Groups[4].Value)
                };

                testProgram[i] = instruction;
            }

            return testProgram;
        }

        private string[] ParseInput(string input)
        {
            input = input.Replace("\r", "");

            string[] inputParts = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );

            return inputParts;
        }
    }
}
