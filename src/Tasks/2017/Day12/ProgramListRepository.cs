using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day12
{
    public class ProgramListRepository
    {
        public Dictionary<int, List<int>> GetProgramList(string input)
        {
            Dictionary<int, List<int>> programList = new Dictionary<int, List<int>>();

            string[] programListConnections = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string programConnectionsString in programListConnections)
            {
                string[] programConnectionsSplitted = programConnectionsString.Split("<->");

                int program = int.Parse(programConnectionsSplitted[0].Trim());

                List<int> programConnections = new List<int>();
                foreach (string connection in programConnectionsSplitted[1].Split(','))
                {
                    programConnections.Add(int.Parse(connection.Trim()));
                }

                programList.Add(program, programConnections);
            }

            return programList;
        }
    }
}
