/*
--- Part Two ---

Seeing how reindeer move in bursts, Santa decides he's not pleased with the old
scoring system.

Instead, at the end of each second, he awards one point to the reindeer
currently in the lead. (If there are multiple reindeer tied for the lead, they
each get one point.) He keeps the traditional 2503 second time limit, of course,
as doing otherwise would be entirely ridiculous.

Given the example reindeer from above, after the first second, Dancer is in the
lead and gets one point. He stays in the lead until several seconds into Comet's
second burst: after the 140th second, Comet pulls into the lead and gets his
first point. Of course, since Dancer had been in the lead for the 139 seconds
before that, he has accumulated 139 points by the 140th second.

After the 1000th second, Dancer has accumulated 689 points, while poor Comet,
our old champion, only has 312. So, with the new scoring system, Dancer would
win (if the race ended at 1000 seconds).

Again given the descriptions of each reindeer (in your puzzle input), after
exactly 2503 seconds, how many points does the winning reindeer have?
*/

using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day14
{
    public class Part2 : ITask<int>
    {
        private readonly ReindeersDescriptionsRepository reindeersDescriptionsRepository;

        private readonly ReindeersFlyingRace reindeersFlyingRace;

        public Part2()
        {
            reindeersDescriptionsRepository = new ReindeersDescriptionsRepository();
            reindeersFlyingRace = new ReindeersFlyingRace();
        }

        public int Solution(string input)
        {
            Dictionary<string, ReindeerDescription> reindeersDescriptions =
                reindeersDescriptionsRepository.GetReindeersDescriptions(input);
            Dictionary<int, Dictionary<string, int>> traveledDistancesAfterEachSecond =
                reindeersFlyingRace.CalculateReindeersTraveledDistancesAfterEachSecond(reindeersDescriptions);

            Dictionary<string, int> reindeersPoints = new Dictionary<string, int>();
            // Initialize reindeers points
            foreach (string reindeersNames in traveledDistancesAfterEachSecond[1].Keys)
            {
                reindeersPoints.Add(reindeersNames, 0);
            }

            // Calculate points for each reindeer
            foreach (KeyValuePair<int, Dictionary<string, int>> reindeerAfterSeconds
                in traveledDistancesAfterEachSecond)
            {
                int longestDistance = reindeerAfterSeconds.Value.Values.Max();

                foreach (KeyValuePair<string, int> reindeer in reindeerAfterSeconds.Value)
                {
                    if (reindeer.Value == longestDistance)
                    {
                        reindeersPoints[reindeer.Key] += 1;
                    }
                }
            }

            int winningReindeerPoints = reindeersPoints.Values.Max();

            return winningReindeerPoints;
        }
    }
}
