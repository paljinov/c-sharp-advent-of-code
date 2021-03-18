using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day12
{
    public class Programs
    {
        private const int PROGRAM_WITH_ID_0 = 0;

        public int CountProgramsInTheGroupThatContainsProgramWithId0(Dictionary<int, List<int>> programList)
        {
            HashSet<int> programsInGroup = new HashSet<int>
            {
                PROGRAM_WITH_ID_0
            };

            FindProgramsInGroup(programList, PROGRAM_WITH_ID_0, programsInGroup);

            return programsInGroup.Count;
        }

        public int CountTotalGroups(Dictionary<int, List<int>> programList)
        {
            int totalGroups = 0;

            while (programList.Count > 0)
            {
                int program = programList.Keys.First();
                HashSet<int> programsInGroup = new HashSet<int>
                {
                    program
                };

                FindProgramsInGroup(programList, program, programsInGroup);

                // Remove programs from list which are found in this group
                foreach (int groupProgram in programsInGroup)
                {
                    programList.Remove(groupProgram);
                }

                totalGroups++;
            }

            return totalGroups;
        }

        private void FindProgramsInGroup(
            Dictionary<int, List<int>> programList,
            int program,
            HashSet<int> programsInGroup
        )
        {
            foreach (int programConnection in programList[program])
            {
                if (!programsInGroup.Contains(programConnection))
                {
                    programsInGroup.Add(programConnection);
                    FindProgramsInGroup(programList, programConnection, programsInGroup);
                }
            }
        }
    }
}
