/*
--- Part Two ---

The bridge you've built isn't long enough; you can't jump the rest of the way.

In the example above, there are two longest bridges:

0/2--2/2--2/3--3/4
0/2--2/2--2/3--3/5

Of them, the one which uses the 3/5 component is stronger; its strength is 0+2 +
2+2 + 2+3 + 3+5 = 19.

What is the strength of the longest bridge you can make? If you can make
multiple bridges of the longest length, pick the strongest one.
*/

namespace App.Tasks.Year2017.Day24
{
    public class Part2 : ITask<int>
    {
        private readonly ComponentRepository componentRepository;

        private readonly Bridges bridges;

        public Part2()
        {
            componentRepository = new ComponentRepository();
            bridges = new Bridges();
        }

        public int Solution(string input)
        {
            Component[] components = componentRepository.GetComponents(input);
            int strengthOfStrongestBridgeWhichLengthIsLongest =
                bridges.CalculateStrengthOfStrongestBridgeWhichLengthIsLongest(components);

            return strengthOfStrongestBridgeWhichLengthIsLongest;
        }
    }
}
