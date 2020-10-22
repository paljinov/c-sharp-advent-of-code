using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day7
{
    class InstructionsRepository
    {
        /// <summary>
        /// Returns instructions in dictionary where key represents line where
        /// wire value is calculated.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetInstructions(string input)
        {
            Dictionary<string, string> instructions = new Dictionary<string, string>();

            string[] instructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var instructionString in instructionsString)
            {
                string wireName = instructionString.Split(' ').Last();
                instructions.Add(wireName, instructionString);
            }

            return instructions;
        }
    }
}
