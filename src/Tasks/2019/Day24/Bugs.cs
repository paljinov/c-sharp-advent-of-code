using System;
using System.Collections.Generic;
using System.Text;

namespace App.Tasks.Year2019.Day24
{
    public class Bugs
    {
        public int CalculateBiodiversityRatingForTheFirstLayoutThatAppearsTwice(bool[,] bugGrid)
        {
            Dictionary<string, bool[,]> bugGridLayouts = new Dictionary<string, bool[,]>();

            while (!IsBugGridLayoutRepetead(bugGrid, bugGridLayouts))
            {
                bugGridLayouts.Add(FlattenBugGridLayout(bugGrid), bugGrid);

                bool[,] nextBugGridLayout = bugGrid.Clone() as bool[,];

                for (int x = 0; x < bugGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < bugGrid.GetLength(1); y++)
                    {
                        // Bug
                        if (bugGrid[x, y])
                        {
                            if (BugDies(x, y, bugGrid))
                            {
                                nextBugGridLayout[x, y] = false;
                            }
                        }
                        // Empty space
                        else
                        {
                            if (EmptySpaceBecomesInfested(x, y, bugGrid))
                            {
                                nextBugGridLayout[x, y] = true;
                            }
                        }
                    }
                }

                bugGrid = nextBugGridLayout.Clone() as bool[,];
            }

            int biodiversityRating = CalculateBiodiversityRatingForGridLayout(bugGrid);

            return biodiversityRating;
        }

        public int CountBugsWhichArePresentAfter(bool[,] bugGrid, int minutes)
        {
            return bugGrid.Length;
        }

        private string FlattenBugGridLayout(bool[,] bugGrid)
        {
            StringBuilder flattenedBugGridLayout = new StringBuilder();

            for (int x = 0; x < bugGrid.GetLength(0); x++)
            {
                for (int y = 0; y < bugGrid.GetLength(1); y++)
                {
                    flattenedBugGridLayout.Append(bugGrid[x, y] ? 1 : 0);
                }
            }

            return flattenedBugGridLayout.ToString();
        }

        private bool BugDies(int x, int y, bool[,] bugGrid)
        {
            int adjacentBugs = CountAdjacentBugs(x, y, bugGrid);

            if (adjacentBugs == 1)
            {
                return false;
            }

            return true;
        }

        private bool EmptySpaceBecomesInfested(int x, int y, bool[,] bugGrid)
        {
            int adjacentBugs = CountAdjacentBugs(x, y, bugGrid);

            if (adjacentBugs == 1 || adjacentBugs == 2)
            {
                return true;
            }

            return false;
        }

        private int CountAdjacentBugs(int x, int y, bool[,] bugGrid)
        {
            int adjacentBugs = 0;

            if (x - 1 >= 0 && bugGrid[x - 1, y])
            {
                adjacentBugs++;
            }
            if (y - 1 >= 0 && bugGrid[x, y - 1])
            {
                adjacentBugs++;
            }
            if (x + 1 < bugGrid.GetLength(0) && bugGrid[x + 1, y])
            {
                adjacentBugs++;
            }
            if (y + 1 < bugGrid.GetLength(1) && bugGrid[x, y + 1])
            {
                adjacentBugs++;
            }

            return adjacentBugs;
        }

        private bool IsBugGridLayoutRepetead(bool[,] bugGrid, Dictionary<string, bool[,]> bugGridLayouts)
        {
            string flattenedBugGridLayout = FlattenBugGridLayout(bugGrid);

            foreach (KeyValuePair<string, bool[,]> formerBugGridLayout in bugGridLayouts)
            {
                if (flattenedBugGridLayout == formerBugGridLayout.Key)
                {
                    return true;
                }
            }

            return false;
        }

        private int CalculateBiodiversityRatingForGridLayout(bool[,] bugGrid)
        {
            int biodiversityRating = 0;
            int @base = 2;

            for (int x = 0; x < bugGrid.GetLength(0); x++)
            {
                for (int y = 0; y < bugGrid.GetLength(1); y++)
                {
                    if (bugGrid[x, y])
                    {
                        int power = x * bugGrid.GetLength(0) + y;
                        biodiversityRating += (int)Math.Pow(@base, power);
                    }
                }
            }

            return biodiversityRating;
        }
    }
}
