using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day7
{
    public class ProgramsStructure
    {
        public string FindBottomProgramName(Dictionary<string, Program> programs)
        {
            List<string> allProgramsAbove = new List<string>();

            foreach (KeyValuePair<string, Program> program in programs)
            {
                foreach (string programAbove in program.Value.ProgramsAbove)
                {
                    allProgramsAbove.Add(programAbove);
                }
            }

            string bottomProgramName = programs.Keys.Except(allProgramsAbove).First();

            return bottomProgramName;
        }

        public int FindUnbalancedProgramProperWeight(Dictionary<string, Program> programs)
        {
            int unbalancedProgramProperWeight = 0;

            Dictionary<string, int> towerWeights = new Dictionary<string, int>();
            foreach (KeyValuePair<string, Program> program in programs)
            {
                int towerWeight = GetTowerWeight(program.Value, programs);
                towerWeights.Add(program.Value.Name, towerWeight);
            }

            foreach (KeyValuePair<string, Program> program in programs)
            {
                Dictionary<int, List<string>> programsByWeight = new Dictionary<int, List<string>>();
                foreach (string programAbove in program.Value.ProgramsAbove)
                {
                    int programAboveWeight = towerWeights[programAbove];
                    if (programsByWeight.ContainsKey(programAboveWeight))
                    {
                        programsByWeight[programAboveWeight].Add(programAbove);
                    }
                    else
                    {
                        programsByWeight[programAboveWeight] = new List<string> { programAbove };
                    }
                }

                // If the program is unbalanced it will have different weight
                if (programsByWeight.Count > 1)
                {
                    int balancedWeight = programsByWeight
                        .Where(p => p.Value.Count > 1).Select(p => p.Key).Single();
                    int unbalancedWeight = programsByWeight
                        .Where(p => p.Value.Count == 1).Select(p => p.Key).Single();
                    string unbalancedProgram = programsByWeight[unbalancedWeight].Single();

                    unbalancedProgramProperWeight = programs[unbalancedProgram].Weight
                        - (unbalancedWeight - balancedWeight);

                    break;
                }
            }

            return unbalancedProgramProperWeight;
        }

        private int GetTowerWeight(Program program, Dictionary<string, Program> programs)
        {
            int towerWeight = program.Weight;
            foreach (string programAbove in program.ProgramsAbove)
            {
                towerWeight += GetTowerWeight(programs[programAbove], programs);
            }

            return towerWeight;
        }
    }
}
