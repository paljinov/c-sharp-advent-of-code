using System;

namespace App.Tasks.Year2021.Day17
{
    public class ProbeLauncher
    {
        public int CalculateHighestPositionProbeReachesOnThisTrajectory(TargetArea targetArea)
        {
            (int highestPosition, _) = DoProbesLaunchWithDistinctVelocities(targetArea);
            return highestPosition;
        }

        public int CountDistinctInitialVelocitiesWhichCauseProbeToBeWithinTargetArea(TargetArea targetArea)
        {
            (_, int distinctInitialVelocitiesWhichCauseProbeToBeWithinTargetArea) =
                DoProbesLaunchWithDistinctVelocities(targetArea);
            return distinctInitialVelocitiesWhichCauseProbeToBeWithinTargetArea;
        }

        private (int, int) DoProbesLaunchWithDistinctVelocities(TargetArea targetArea)
        {
            int highestPosition = 0;
            int distinctInitialVelocitiesWhichCauseProbeToBeWithinTargetArea = 0;

            int xMinVelocity = (int)Math.Sqrt(2 * targetArea.X.From);
            int xMaxVelocity = targetArea.X.To;
            int yMinVelocity = targetArea.Y.From;
            int yMaxVelocity = targetArea.X.To - targetArea.Y.To;

            for (int initialVelocityX = xMinVelocity; initialVelocityX <= xMaxVelocity; initialVelocityX++)
            {
                for (int initialVelocityY = yMinVelocity; initialVelocityY <= yMaxVelocity; initialVelocityY++)
                {
                    (int highestPositionForVelocity, bool probeToBeWithinTargetArea) =
                        DoProbeLaunchWithVelocity(targetArea, initialVelocityX, initialVelocityY);

                    // If probe was within the target area after any step
                    if (probeToBeWithinTargetArea)
                    {
                        highestPosition = Math.Max(highestPosition, highestPositionForVelocity);
                        distinctInitialVelocitiesWhichCauseProbeToBeWithinTargetArea++;
                    }
                }
            }

            return (highestPosition, distinctInitialVelocitiesWhichCauseProbeToBeWithinTargetArea);
        }

        private (int, bool) DoProbeLaunchWithVelocity(TargetArea targetArea, int xVelocity, int yVelocity)
        {
            int highestPosition = 0;
            bool probeToBeWithinTargetArea = false;

            // The probe's x,y position starts at 0,0
            int x = 0;
            int y = 0;

            // While target is reachable
            while (xVelocity > 0 || y > targetArea.Y.From)
            {
                // Update position and height
                x += xVelocity;
                y += yVelocity;
                highestPosition = Math.Max(highestPosition, y);

                // Due to drag, the probe's x velocity changes by 1 toward the value 0;
                // that is, it decreases by 1 if it is greater than 0,increases by 1 if
                // it is less than 0, or does not change if it is already 0
                xVelocity = Math.Max(0, xVelocity - 1);
                // Due to gravity, the probe's y velocity decreases by 1
                yVelocity--;

                // If probe passes target area
                if (x >= targetArea.X.From && x <= targetArea.X.To
                    && y >= targetArea.Y.From && y <= targetArea.Y.To)
                {
                    probeToBeWithinTargetArea = true;
                    break;
                }
            }

            return (highestPosition, probeToBeWithinTargetArea);
        }
    }
}
