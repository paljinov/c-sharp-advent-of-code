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
                Dictionary<int, List<string>> programsCountByWeight = new Dictionary<int, List<string>>();
                foreach (string programAbove in program.Value.ProgramsAbove)
                {
                    int programAboveWeight = towerWeights[programAbove];
                    if (programsCountByWeight.ContainsKey(programAboveWeight))
                    {
                        programsCountByWeight[programAboveWeight].Add(programAbove);
                    }
                    else
                    {
                        programsCountByWeight[programAboveWeight] = new List<string> { programAbove };
                    }
                }

                if (programsCountByWeight.Count > 1)
                {
                    int balancedWeight = 0;
                    int unbalancedWeight = 0;
                    string unbalancedProgram = string.Empty;
                    foreach (KeyValuePair<int, List<string>> programCountByWeight in programsCountByWeight)
                    {
                        if (programCountByWeight.Value.Count == 1)
                        {
                            unbalancedWeight = programCountByWeight.Key;
                            unbalancedProgram = programCountByWeight.Value.First();
                        }
                        else
                        {
                            balancedWeight = programCountByWeight.Key;
                        }
                    }

                    unbalancedProgramProperWeight =
                        programs[unbalancedProgram].Weight - (unbalancedWeight - balancedWeight);
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
