/*
--- Part Two ---

There are more programs than just the ones in the group containing program ID 0.
The rest of them have no way of reaching that group, and still might have no way
of reaching each other.

A group is a collection of programs that can all communicate via pipes either
directly or indirectly. The programs you identified just a moment ago are all
part of the same group. Now, they would like you to determine the total number
of groups.

In the example above, there were 2 groups: one consisting of programs
0,2,3,4,5,6, and the other consisting solely of program 1.

How many groups are there in total?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2017.Day12
{
    public class Part2 : ITask<int>
    {
        private readonly ProgramListRepository programListRepository;

        private readonly Programs programs;

        public Part2()
        {
            programListRepository = new ProgramListRepository();
            programs = new Programs();
        }

        public int Solution(string input)
        {
            Dictionary<int, List<int>> programList = programListRepository.GetProgramList(input);
            int totalGroups = programs.CountTotalGroups(programList);

            return totalGroups;
        }
    }
}
