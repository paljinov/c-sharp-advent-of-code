using System;

namespace App.Tasks.Year2022.Day2
{
    public class StrategyGuideRepository
    {
        private const char OPPONENT_PAPER = 'B';

        private const char OPPONENT_SCISSORS = 'C';

        private const char RESPONSE_PAPER = 'Y';

        private const char RESPONSE_SCISSORS = 'Z';

        private const char DRAW_ROUND_OUTCOME = 'Y';

        private const char WIN_ROUND_OUTCOME = 'Z';


        public (Shape OpponentShape, Shape ResponseShape)[] GetStrategyGuide(string input)
        {
            string[] strategyGuideString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            (Shape OpponentShape, Shape ResponseShape)[] strategyGuide = new (Shape, Shape)[strategyGuideString.Length];

            for (int i = 0; i < strategyGuideString.Length; i++)
            {
                string[] roundString = strategyGuideString[i].Split(' ');
                Shape opponentShape = GetOpponentShape(roundString[0][0]);
                Shape responseShape = GetResponseShape(roundString[1][0]);

                strategyGuide[i] = (opponentShape, responseShape);
            }

            return strategyGuide;
        }

        public (Shape OpponentShape, Outcome RoundOutcome)[] GetStrategyGuideWhenSecondColumnSaysHowRoundNeedsToEnd(
            string input
        )
        {
            string[] strategyGuideString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            (Shape OpponentShape, Outcome RoundOutcome)[] strategyGuide =
                new (Shape, Outcome)[strategyGuideString.Length];

            for (int i = 0; i < strategyGuideString.Length; i++)
            {
                string[] roundString = strategyGuideString[i].Split(' ');
                Shape opponentShape = GetOpponentShape(roundString[0][0]);
                Outcome roundOutcome = GetRoundOutcome(roundString[1][0]);

                strategyGuide[i] = (opponentShape, roundOutcome);
            }

            return strategyGuide;
        }

        private Shape GetOpponentShape(char shapeChar)
        {
            Shape shape = Shape.Rock;
            switch (shapeChar)
            {
                case OPPONENT_PAPER:
                    shape = Shape.Paper;
                    break;
                case OPPONENT_SCISSORS:
                    shape = Shape.Scissors;
                    break;
            }

            return shape;
        }

        private Shape GetResponseShape(char shapeChar)
        {
            Shape shape = Shape.Rock;
            switch (shapeChar)
            {
                case RESPONSE_PAPER:
                    shape = Shape.Paper;
                    break;
                case RESPONSE_SCISSORS:
                    shape = Shape.Scissors;
                    break;
            }

            return shape;
        }

        private Outcome GetRoundOutcome(char outcomeChar)
        {
            Outcome roundOutcome = Outcome.Defeat;
            switch (outcomeChar)
            {
                case DRAW_ROUND_OUTCOME:
                    roundOutcome = Outcome.Draw;
                    break;
                case WIN_ROUND_OUTCOME:
                    roundOutcome = Outcome.Win;
                    break;
            }

            return roundOutcome;
        }
    }
}
