/*
--- Part Two ---

Finish folding the transparent paper according to the instructions. The manual
says the code is always eight capital letters.

What code do you use to activate the infrared thermal imaging camera system?
*/

namespace App.Tasks.Year2021.Day13
{
    public class Part2 : ITask<string>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly TransparentPaper transparentPaper;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            transparentPaper = new TransparentPaper();
        }

        public string Solution(string input)
        {
            (int, int)[] dots = instructionsRepository.GetDots(input);
            (char, int)[] foldInstructions = instructionsRepository.GetFoldInstructions(input);

            string codeNeededToActivateTheInfraredThermalImagingCameraSystem =
                transparentPaper.FindCodeNeededToActivateTheInfraredThermalImagingCameraSystem(dots, foldInstructions);

            return codeNeededToActivateTheInfraredThermalImagingCameraSystem;
        }
    }
}
