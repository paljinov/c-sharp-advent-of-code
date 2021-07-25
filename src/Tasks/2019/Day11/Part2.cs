/*
--- Part Two ---

You're not sure what it's trying to paint, but it's definitely not a
registration identifier. The Space Police are getting impatient.

Checking your external ship cameras again, you notice a white panel marked
"emergency hull painting robot starting panel". The rest of the panels are still
black, but it looks like the robot was expecting to start on a white panel, not
a black one.

Based on the Space Law Space Brochure that the Space Police attached to one of
your windows, a valid registration identifier is always eight capital letters.
After starting the robot on a single white panel instead, what registration
identifier does it paint on your hull?
*/

namespace App.Tasks.Year2019.Day11
{
    public class Part2 : ITask<string>
    {
        private readonly IntegersRepository integersRepository;

        private readonly PaintingRobot paintingRobot;

        public Part2()
        {
            integersRepository = new IntegersRepository();
            paintingRobot = new PaintingRobot();
        }

        public string Solution(string input)
        {
            long[] integers = integersRepository.GetIntegers(input);
            string registrationIdentifier = paintingRobot.GetRegistrationIdentifierWhichIsPaintedOnHull(integers);

            return registrationIdentifier;
        }
    }
}
