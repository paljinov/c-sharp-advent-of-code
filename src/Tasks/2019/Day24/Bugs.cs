using System;
using System.Collections.Generic;
using System.Linq;
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
                        int adjacentBugs = CountAdjacentBugs(x, y, bugGrid);

                        // Bug
                        if (bugGrid[x, y])
                        {
                            if (BugDies(adjacentBugs))
                            {
                                nextBugGridLayout[x, y] = false;
                            }
                        }
                        // Empty space
                        else
                        {
                            if (EmptySpaceBecomesInfested(adjacentBugs))
                            {
                                nextBugGridLayout[x, y] = true;
                            }
                        }
                    }
                }

                bugGrid = nextBugGridLayout;
            }

            int biodiversityRating = CalculateBiodiversityRatingForGridLayout(bugGrid);

            return biodiversityRating;
        }

        public int CountBugsWhichArePresentAfter(bool[,] bugGrid, int totalMinutes)
        {
            int rows = bugGrid.GetLength(0);
            int columns = bugGrid.GetLength(1);

            SortedDictionary<int, bool[,]> bugGrids = new SortedDictionary<int, bool[,]>
            {
                { 0, bugGrid }
            };

            for (int minute = 1; minute <= totalMinutes; minute++)
            {
                int[] levels = bugGrids.Keys.ToArray();
                foreach (int depth in levels)
                {
                    // If outer grid level doesn't exists
                    if (!bugGrids.ContainsKey(depth - 1))
                    {
                        bugGrids[depth - 1] = new bool[rows, columns];
                    }
                    // If inner grid level doesn't exists
                    if (!bugGrids.ContainsKey(depth + 1))
                    {
                        bugGrids[depth + 1] = new bool[rows, columns];
                    }

                    UpdateBugGridForLevel(bugGrids, depth);
                }
            }

            int presentBugs = CountPresentBugs(bugGrids);

            return presentBugs;
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

        private bool BugDies(int adjacentBugs)
        {
            if (adjacentBugs == 1)
            {
                return false;
            }

            return true;
        }

        private bool EmptySpaceBecomesInfested(int adjacentBugs)
        {
            if (adjacentBugs == 1 || adjacentBugs == 2)
            {
                return true;
            }

            return false;
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

        private void UpdateBugGridForLevel(SortedDictionary<int, bool[,]> bugGrids, int depth)
        {
            int rows = bugGrids[0].GetLength(0);
            int columns = bugGrids[0].GetLength(1);

            (int midX, int midY) = (rows / 2, columns / 2);
            bool[,] nextBugGridLayout = bugGrids[depth].Clone() as bool[,];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    // Center tile is skipped
                    if (x == midX && y == midY)
                    {
                        continue;
                    }

                    int adjacentBugs = CountAdjacentBugsForNestedGrids(x, y, depth, bugGrids);

                    // Bug
                    if (bugGrids[depth][x, y])
                    {
                        if (BugDies(adjacentBugs))
                        {
                            nextBugGridLayout[x, y] = false;
                        }
                    }
                    // Empty space
                    else
                    {
                        if (EmptySpaceBecomesInfested(adjacentBugs))
                        {
                            nextBugGridLayout[x, y] = true;
                        }
                    }
                }
            }

            bugGrids[depth] = nextBugGridLayout;
        }

        private int CountAdjacentBugsForNestedGrids(
            int x,
            int y,
            int depth,
            SortedDictionary<int, bool[,]> bugGrids
        )
        {
            int adjacentBugs = 0;

            bool[,] bugGrid = bugGrids[depth];
            bool[,] innerBugGrid = bugGrids[depth + 1];
            bool[,] outerBugGrid = bugGrids[depth - 1];

            int rows = bugGrid.GetLength(0);
            int columns = bugGrid.GetLength(1);
            (int midX, int midY) = (rows / 2, columns / 2);

            // Top is inner
            if (x - 1 == midX && y == midY)
            {
                for (int i = 0; i < columns; i++)
                {
                    if (innerBugGrid[rows - 1, i])
                    {
                        adjacentBugs++;
                    }
                }
            }
            // Top is outer
            else if (x - 1 < 0 && outerBugGrid[midX - 1, midY])
            {
                adjacentBugs++;
            }
            else if (x - 1 >= 0 && bugGrid[x - 1, y])
            {
                adjacentBugs++;
            }

            // Left is inner
            if (x == midX && y - 1 == midY)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (innerBugGrid[i, columns - 1])
                    {
                        adjacentBugs++;
                    }
                }
            }
            // Left is outer
            else if (y - 1 < 0 && outerBugGrid[midX, midY - 1])
            {
                adjacentBugs++;
            }
            else if (y - 1 >= 0 && bugGrid[x, y - 1])
            {
                adjacentBugs++;
            }

            // Bottom is inner
            if (x + 1 == midX && y == midY)
            {
                for (int i = 0; i < columns; i++)
                {
                    if (innerBugGrid[0, i])
                    {
                        adjacentBugs++;
                    }
                }
            }
            // Bottom is outer
            else if (x + 1 == bugGrid.GetLength(0) && outerBugGrid[midX + 1, midY])
            {
                adjacentBugs++;
            }
            else if (x + 1 < bugGrid.GetLength(0) && bugGrid[x + 1, y])
            {
                adjacentBugs++;
            }

            // Right is inner
            if (x == midX && y + 1 == midY)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (innerBugGrid[i, 0])
                    {
                        adjacentBugs++;
                    }
                }
            }
            // Right is outer
            else if (y + 1 == bugGrid.GetLength(1) && outerBugGrid[midX, midY + 1])
            {
                adjacentBugs++;
            }
            else if (y + 1 < bugGrid.GetLength(1) && bugGrid[x, y + 1])
            {
                adjacentBugs++;
            }

            return adjacentBugs;
        }

        private int CountPresentBugs(SortedDictionary<int, bool[,]> bugGrids)
        {
            int presentBugs = 0;

            foreach (KeyValuePair<int, bool[,]> bugGrid in bugGrids)
            {
                for (int x = 0; x < bugGrid.Value.GetLength(0); x++)
                {
                    for (int y = 0; y < bugGrid.Value.GetLength(1); y++)
                    {
                        if (bugGrid.Value[x, y])
                        {
                            presentBugs++;
                        }
                    }
                }
            }

            return presentBugs;
        }
    }
}
