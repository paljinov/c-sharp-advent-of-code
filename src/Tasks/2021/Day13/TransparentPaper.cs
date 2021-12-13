using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2021.Day13
{
    public class TransparentPaper
    {
        private const char EMPTY_POSITION = '.';

        private const char DOT_POSITION = '#';

        public int CountDotsAfterCompletingFirstFoldInstruction((int, int)[] dots, (char, int)[] foldInstructions)
        {
            HashSet<(int, int)> dotsAfterFolding = FoldTransparentPaper(dots.ToHashSet(), foldInstructions[0]);

            return dotsAfterFolding.Count;
        }

        public string FindCodeNeededToActivateTheInfraredThermalImagingCameraSystem(
            (int, int)[] dots,
            (char, int)[] foldInstructions
        )
        {
            HashSet<(int, int)> dotsAfterFolding = dots.ToHashSet();
            foreach ((char, int) foldInstruction in foldInstructions)
            {
                dotsAfterFolding = FoldTransparentPaper(dotsAfterFolding, foldInstruction);
            }

            string codeNeededToActivateTheInfraredThermalImagingCameraSystem = PrepareCodeForPrint(dotsAfterFolding);

            return codeNeededToActivateTheInfraredThermalImagingCameraSystem;
        }

        private HashSet<(int, int)> FoldTransparentPaper(
            HashSet<(int X, int Y)> dots,
            (char Direction, int Line) foldInstruction
        )
        {
            HashSet<(int, int)> dotsAfterFolding = new HashSet<(int, int)>();

            int minX = dots.Select(d => d.X).Min();
            int maxX = dots.Select(d => d.X).Max();
            int minY = dots.Select(d => d.Y).Min();
            int maxY = dots.Select(d => d.Y).Max();

            // Fold the paper up
            if (foldInstruction.Direction == 'y')
            {
                for (int i = minY; i <= maxY; i++)
                {
                    if (i == foldInstruction.Line)
                    {
                        continue;
                    }

                    int y = i;
                    if (i > foldInstruction.Line)
                    {
                        y -= 2 * (y - foldInstruction.Line);
                    }

                    for (int j = minX; j <= maxX; j++)
                    {
                        int x = j;
                        if (dots.Contains((j, i)))
                        {
                            dotsAfterFolding.Add((x, y));
                        }
                    }
                }
            }
            // Fold the paper left
            else
            {
                for (int i = minY; i <= maxY; i++)
                {
                    int y = i;

                    for (int j = minX; j <= maxX; j++)
                    {
                        if (j == foldInstruction.Line)
                        {
                            continue;
                        }

                        int x = j;
                        if (j > foldInstruction.Line)
                        {
                            x -= 2 * (x - foldInstruction.Line);
                        }

                        if (dots.Contains((j, i)))
                        {
                            dotsAfterFolding.Add((x, y));
                        }
                    }
                }
            }

            return dotsAfterFolding;
        }

        private string PrepareCodeForPrint(HashSet<(int X, int Y)> dots)
        {
            StringBuilder codeNeededToActivateTheInfraredThermalImagingCameraSystem = new StringBuilder();

            int minX = dots.Select(d => d.X).Min();
            int maxX = dots.Select(d => d.X).Max();
            int minY = dots.Select(d => d.Y).Min();
            int maxY = dots.Select(d => d.Y).Max();

            for (int i = minY; i <= maxY; i++)
            {
                codeNeededToActivateTheInfraredThermalImagingCameraSystem.AppendLine();
                for (int j = minX; j <= maxX; j++)
                {
                    if (dots.Contains((j, i)))
                    {
                        codeNeededToActivateTheInfraredThermalImagingCameraSystem.Append(DOT_POSITION);
                    }
                    else
                    {
                        codeNeededToActivateTheInfraredThermalImagingCameraSystem.Append(EMPTY_POSITION);
                    }
                }
            }

            return codeNeededToActivateTheInfraredThermalImagingCameraSystem.ToString();
        }
    }
}
