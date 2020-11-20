/*
--- Part Two ---

You finally arrive at the bathroom (it's a several minute walk from the lobby so
visitors can behold the many fancy conference rooms and water coolers on this
floor) and go to punch in the code. Much to your bladder's dismay, the keypad is
not at all like you imagined it. Instead, you are confronted with the result of
hundreds of man-hours of bathroom-keypad-design meetings:

    1
  2 3 4
5 6 7 8 9
  A B C
    D

You still start at "5" and stop when you're at an edge, but given the same
instructions as above, the outcome is very different:

- You start at "5" and don't move at all (up and left are both edges), ending at
  5.
- Continuing from "5", you move right twice and down three times (through "6",
  "7", "B", "D", "D"), ending at D.
- Then, from "D", you move five more times (through "D", "B", "C", "C", "B"),
  ending at B.
- Finally, after five more moves, you end at 3.

So, given the actual keypad layout, the code would be 5DB3.

Using the same instructions in your puzzle input, what is the correct bathroom
code?
*/

namespace App.Tasks.Year2016.Day2
{
    public class Part2 : ITask<string>
    {
        private readonly char[,] keypad = new char[,] {
            { Code.NotExistingKey, Code.NotExistingKey, '1', Code.NotExistingKey, Code.NotExistingKey },
            { Code.NotExistingKey, '2', '3', '4', Code.NotExistingKey },
            { '5', '6', '7', '8', '9' },
            { Code.NotExistingKey, 'A', 'B', 'C', Code.NotExistingKey },
            { Code.NotExistingKey, Code.NotExistingKey, 'D', Code.NotExistingKey, Code.NotExistingKey }
        };

        private readonly InstructionsRepository instructionsRepository;

        private readonly Code code;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            code = new Code();
        }

        public string Solution(string input)
        {
            string[] instructions = instructionsRepository.ParseInput(input);
            string code = this.code.FindCode(keypad, instructions);

            return code;
        }
    }
}
