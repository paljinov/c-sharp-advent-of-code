using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day7
{
    public class ProgramsStructure
    {
        public string FindBottomProgramName(List<Program> programs)
        {
            List<string> allPrograms = new List<string>();
            List<string> programsAbove = new List<string>();

            foreach (Program program in programs)
            {
                allPrograms.Add(program.Name);
                foreach (string programAbove in program.ProgramsAbove)
                {
                    programsAbove.Add(programAbove);
                }
            }

            string bottomProgramName = allPrograms.Except(programsAbove).First();

            return bottomProgramName;
        }
    }
}
