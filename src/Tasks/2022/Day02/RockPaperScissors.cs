namespace App.Tasks.Year2022.Day2
{
    public class RockPaperScissors
    {
        public int CalculateTotalScore((Shape OpponentShape, Shape ResponseShape)[] strategyGuide)
        {
            int totalScore = 0;

            for (int i = 0; i < strategyGuide.Length; i++)
            {
                Outcome roundOutcome = GetRoundOutcome(strategyGuide[i].OpponentShape, strategyGuide[i].ResponseShape);

                // Score for the shape you selected plus the score for the outcome of the round
                totalScore += (int)strategyGuide[i].ResponseShape + (int)roundOutcome;
            }

            return totalScore;
        }

        private Outcome GetRoundOutcome(Shape opponentShape, Shape responseShape)
        {
            Outcome roundOutcome;

            if (opponentShape == Shape.Rock && responseShape == Shape.Scissors
                || opponentShape == Shape.Scissors && responseShape == Shape.Paper
                || opponentShape == Shape.Paper && responseShape == Shape.Rock)
            {
                roundOutcome = Outcome.Defeat;
            }
            else if (opponentShape == responseShape)
            {
                roundOutcome = Outcome.Draw;
            }
            else
            {
                roundOutcome = Outcome.Win;
            }

            return roundOutcome;
        }

        private Shape GetResponseShape(Shape opponentShape, Outcome roundOutcome)
        {
            Shape responseShape;

            if (opponentShape == Shape.Rock && roundOutcome == Outcome.Defeat)
            {
                responseShape = Shape.Scissors;
            }
            else if (opponentShape == Shape.Scissors && roundOutcome == Outcome.Defeat)
            {
                responseShape = Shape.Paper;
            }
            else if (opponentShape == Shape.Paper && roundOutcome == Outcome.Defeat)
            {
                responseShape = Shape.Rock;
            }
            else if (opponentShape == Shape.Rock && roundOutcome == Outcome.Draw)
            {
                responseShape = Shape.Rock;
            }
            else if (opponentShape == Shape.Scissors && roundOutcome == Outcome.Draw)
            {
                responseShape = Shape.Scissors;
            }
            else if (opponentShape == Shape.Paper && roundOutcome == Outcome.Draw)
            {
                responseShape = Shape.Paper;
            }
            else if (opponentShape == Shape.Rock && roundOutcome == Outcome.Win)
            {
                responseShape = Shape.Paper;
            }
            else if (opponentShape == Shape.Scissors && roundOutcome == Outcome.Win)
            {
                responseShape = Shape.Rock;
            }
            else
            {
                responseShape = Shape.Scissors;
            }

            return responseShape;
        }
    }
}
