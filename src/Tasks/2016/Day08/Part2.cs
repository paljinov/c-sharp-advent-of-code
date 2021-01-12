/*
--- Part Two ---

You notice that the screen is only capable of displaying capital letters; in the
font it uses, each letter is 5 pixels wide and 6 tall.

After you swipe your card, what code is the screen trying to display?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2016.Day8
{
    public class Part2 : ITask<string>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly Screen screen;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            screen = new Screen();
        }

        public string Solution(string input)
        {
            List<RectangleInstructions> instructions = instructionsRepository.GetInstructions(input);
            string screenCode = screen.GetScreenCode(instructions);

            return screenCode;
        }
    }
}
