/*
--- Day 12: Rain Risk ---

Your ferry made decent progress toward the island, but the storm came in faster
than anyone expected. The ferry needs to take evasive navigationInstructions!

Unfortunately, the ship's navigation computer seems to be malfunctioning; rather
than giving a route directly to safety, it produced extremely circuitous
instructions. When the captain uses the PA system to ask if anyone can help, you
quickly volunteer.

The navigation instructions (your puzzle input) consists of a sequence of
single-character navigationInstructions paired with integer input values. After staring at them
for a few minutes, you work out what they probably mean:

- NavigationInstruction N means to move north by the given value.
- NavigationInstruction S means to move south by the given value.
- NavigationInstruction E means to move east by the given value.
- NavigationInstruction W means to move west by the given value.
- NavigationInstruction L means to turn left the given number of degrees.
- NavigationInstruction R means to turn right the given number of degrees.
- NavigationInstruction F means to move forward by the given value in the action the ship is
  currently facing.

The ship starts by facing east. Only the L and R navigationInstructions change the action
the ship is facing. (That is, if the ship is facing east and the next
instruction is N10, the ship would move north 10 units, but would still move
east if the following navigationInstruction were F.)

For example:

F10
N3
F7
R90
F11

These instructions would be handled as follows:

- F10 would move the ship 10 units east (because the ship starts by facing east)
  to east 10, north 0.
- N3 would move the ship 3 units north to east 10, north 3.
- F7 would move the ship another 7 units east (because the ship is still facing
  east) to east 17, north 3.
- R90 would cause the ship to turn right by 90 degrees and face south; it
  remains at east 17, north 3.
- F11 would move the ship 11 units south to east 17, south 8.

At the end of these instructions, the ship's Manhattan distance (sum of the
absolute values of its east/west position and its north/south position) from its
starting position is 17 + 8 = 25.

Figure out where the navigation instructions lead. What is the Manhattan
distance between that location and the ship's starting position?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2020.Day12
{
    public class Part1 : ITask<int>
    {
        private readonly ShipNavigationInstructionsRepository shipNavigationInstructionsRepository;

        private readonly ManhattanDistance manhattanDistance;

        public Part1()
        {
            shipNavigationInstructionsRepository = new ShipNavigationInstructionsRepository();
            manhattanDistance = new ManhattanDistance();
        }

        public int Solution(string input)
        {
            List<NavigationInstruction> navigationInstructions =
                shipNavigationInstructionsRepository.GetNavigationInstructions(input);
            int manhattanDistance = this.manhattanDistance.CalculateBetweenStartAndEndPosition(navigationInstructions);

            return manhattanDistance;
        }
    }
}
