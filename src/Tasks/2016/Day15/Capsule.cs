using System.Linq;

namespace App.Tasks.Year2016.Day15
{
    public class Capsule
    {
        public int CalculateFirstTimeYouCanPressButtonAndGetCapsule(Disc[] discs)
        {
            int time = 0;

            while (!AreAllDiscsSequentiallyAtPositionZero(discs, 1, time + 1))
            {
                time++;
            }

            return time;
        }

        private bool AreAllDiscsSequentiallyAtPositionZero(Disc[] discs, int discNumber, int time)
        {
            Disc disc = discs[discNumber - 1];
            int currentPosition = (disc.InitialPosition + time) % disc.TotalPositions;

            if (currentPosition > 0)
            {
                return false;
            }

            if (discNumber + 1 <= discs.Length)
            {
                if (!AreAllDiscsSequentiallyAtPositionZero(discs, discNumber + 1, time + 1))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
