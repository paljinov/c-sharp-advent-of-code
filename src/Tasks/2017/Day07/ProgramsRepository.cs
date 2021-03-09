using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day7
{
    public class ProgramsRepository
    {
        public Dictionary<string, Program> GetPrograms(string input)
        {
            Dictionary<string, Program> programs = new Dictionary<string, Program>();

            string[] programsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex programsRegex = new Regex(@"^(\w+)\s\((\d+)\)(?:\s->\s(.+))?$");

            foreach (string programString in programsString)
            {
                Match programMatch = programsRegex.Match(programString);
                GroupCollection programGroups = programMatch.Groups;

                List<string> programsAbove = new List<string>();
                if (programGroups[3].Captures.Count > 0)
                {
                    programsAbove = programGroups[3].Value.Split(',').Select(pn => pn.Trim()).ToList();
                }

                Program program = new Program
                {
                    Name = programGroups[1].Value,
                    Weight = int.Parse(programGroups[2].Value),
                    ProgramsAbove = programsAbove,
                };

                programs.Add(program.Name, program);
            }

            return programs;
        }
    }
}
