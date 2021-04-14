/*
--- Part Two ---

The packet is curious how many steps it needs to go.

For example, using the same routing diagram from the example above...

     |
     |  +--+
     A  |  C
 F---|--|-E---+
     |  |  |  D
     +B-+  +--+

...the packet would go:

- 6 steps down (including the first line at the top of the diagram).
- 3 steps right.
- 4 steps up.
- 3 steps right.
- 4 steps down.
- 3 steps right.
- 2 steps up.
- 13 steps left (including the F it stops on).

This would result in a total of 38 steps.

How many steps does the packet need to go?
*/

namespace App.Tasks.Year2017.Day19
{
    public class Part2 : ITask<int>
    {
        private readonly RoutingDiagramRepository routingDiagramRepository;

        private readonly FollowPath followPath;

        public Part2()
        {
            routingDiagramRepository = new RoutingDiagramRepository();
            followPath = new FollowPath();
        }

        public int Solution(string input)
        {
            char[,] routingDiagram = routingDiagramRepository.GetRoutingDiagram(input);
            (_, int steps) = followPath.FindSeenLettersAndCountStepsMadeByLittlePacket(routingDiagram);

            return steps;
        }
    }
}
