using System.Collections.Generic;

namespace App.Tasks.Year2021.Day25
{
    public class SeaCucumbers
    {
        public int FindFirstStepOnWhichNoSeaCucumbersMove(SeaCucumberLocation[,] seaCucumbersLocationsMap)
        {
            bool seaCucucumbersMoved = true;

            int step = 0;
            while (seaCucucumbersMoved)
            {
                (seaCucumbersLocationsMap, seaCucucumbersMoved) = MakeStep(seaCucumbersLocationsMap);
                step++;
            }

            return step;
        }

        private (SeaCucumberLocation[,], bool) MakeStep(SeaCucumberLocation[,] seaCucumbersLocationsMap)
        {
            bool seaCucucumbersMoved = false;

            SeaCucumberLocation[,] updatedSeaCucumbersLocationsMap =
                seaCucumbersLocationsMap.Clone() as SeaCucumberLocation[,];

            // East-facing herd moves first
            for (int i = 0; i < seaCucumbersLocationsMap.GetLength(0); i++)
            {
                for (int j = 0; j < seaCucumbersLocationsMap.GetLength(1); j++)
                {
                    // If sea cucumber didn't already move and is moving east
                    if (seaCucumbersLocationsMap[i, j] == SeaCucumberLocation.MovesEast)
                    {
                        int eastLocation = j + 1;
                        if (eastLocation == seaCucumbersLocationsMap.GetLength(1))
                        {
                            eastLocation = 0;
                        }

                        if (seaCucumbersLocationsMap[i, eastLocation] == SeaCucumberLocation.Empty)
                        {
                            updatedSeaCucumbersLocationsMap[i, j] = SeaCucumberLocation.Empty;
                            updatedSeaCucumbersLocationsMap[i, eastLocation] = SeaCucumberLocation.MovesEast;
                            seaCucucumbersMoved = true;
                        }
                    }
                }
            }

            seaCucumbersLocationsMap = updatedSeaCucumbersLocationsMap.Clone() as SeaCucumberLocation[,];

            // South-facing herd moves second
            for (int i = 0; i < seaCucumbersLocationsMap.GetLength(0); i++)
            {
                for (int j = 0; j < seaCucumbersLocationsMap.GetLength(1); j++)
                {
                    // If sea cucumber didn't already move and is moving south
                    if (seaCucumbersLocationsMap[i, j] == SeaCucumberLocation.MovesSouth)
                    {
                        int southLocation = i + 1;
                        if (southLocation == seaCucumbersLocationsMap.GetLength(0))
                        {
                            southLocation = 0;
                        }

                        if (seaCucumbersLocationsMap[southLocation, j] == SeaCucumberLocation.Empty)
                        {
                            updatedSeaCucumbersLocationsMap[i, j] = SeaCucumberLocation.Empty;
                            updatedSeaCucumbersLocationsMap[southLocation, j] = SeaCucumberLocation.MovesSouth;
                            seaCucucumbersMoved = true;
                        }
                    }
                }
            }

            return (updatedSeaCucumbersLocationsMap, seaCucucumbersMoved);
        }
    }
}
