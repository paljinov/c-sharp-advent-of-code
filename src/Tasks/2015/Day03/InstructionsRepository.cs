namespace App.Tasks.Year2015.Day3
{
    public class InstructionsRepository
    {
        private const char NORTH = '^';
        private const char SOUTH = 'v';
        private const char EAST = '>';

        public CardinalDirection[] GetInstructions(string input)
        {
            CardinalDirection[] instructions = new CardinalDirection[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                CardinalDirection cardinalDirection;

                switch (input[i])
                {
                    case NORTH:
                        cardinalDirection = CardinalDirection.North;
                        break;
                    case SOUTH:
                        cardinalDirection = CardinalDirection.South;
                        break;
                    case EAST:
                        cardinalDirection = CardinalDirection.East;
                        break;
                    default:
                        cardinalDirection = CardinalDirection.West;
                        break;
                }

                instructions[i] = cardinalDirection;
            }

            return instructions;
        }
    }
}
