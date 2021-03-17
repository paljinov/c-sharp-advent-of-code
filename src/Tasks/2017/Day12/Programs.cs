using System.Collections.Generic;

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

            DoCount(programList, PROGRAM_WITH_ID_0, programsInGroup);

            return programsInGroup.Count;
        }

        private void DoCount(Dictionary<int, List<int>> programList, int program, HashSet<int> programsInGroup)
        {
            foreach (int programConnection in programList[program])
            {
                if (!programsInGroup.Contains(programConnection))
                {
                    programsInGroup.Add(programConnection);
                    DoCount(programList, programConnection, programsInGroup);
                }
            }
        }
    }
}
