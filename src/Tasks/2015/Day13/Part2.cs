/*
--- Part Two ---

In all the commotion, you realize that you forgot to seat yourself. At this
point, you're pretty apathetic toward the whole thing, and your happiness
wouldn't really go up or down regardless of who you sit next to. You assume
everyone else would be just as ambivalent about sitting next to you, too.

So, add yourself to the list, and give all happiness relationships that involve
you a score of 0.

What is the total change in happiness for the optimal seating arrangement that
actually includes yourself?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day13
{
    public class Part2 : ITask<int>
    {
        private readonly SittingHappinessRepository sittingHappinessRepository;

        private readonly SittingArrangements sittingArrangements;

        public Part2()
        {
            sittingHappinessRepository = new SittingHappinessRepository();
            sittingArrangements = new SittingArrangements();
        }

        public int Solution(string input)
        {
            Dictionary<string, int> sittingHappiness = sittingHappinessRepository.Parse(input);
            List<string> attendees = sittingHappinessRepository.GetDinnerAttendees(sittingHappiness);

            // Add yourself to the dinner table
            AddYourselfToTheDinnerTable(sittingHappiness, attendees);

            List<Dictionary<string, int>> sittingArrangements =
                this.sittingArrangements.GetSittingArrangements(sittingHappiness, attendees);

            int optimalTotalChangeInHappiness =
                this.sittingArrangements.CalculateOptimalSeatingArrangement(sittingArrangements);

            return optimalTotalChangeInHappiness;
        }

        private void AddYourselfToTheDinnerTable(Dictionary<string, int> sittingHappiness, List<string> attendees)
        {
            foreach (string attendee in attendees)
            {
                sittingHappiness.Add($"Me->{attendee}", 0);
                sittingHappiness.Add($"{attendee}->Me", 0);
            }

            attendees.Add("Me");
        }
    }
}
