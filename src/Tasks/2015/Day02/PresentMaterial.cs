using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day2
{
    public class PresentMaterial
    {
        public int CalculateTotalSquareFeetOfWrappingPaper(List<Box> boxes)
        {
            int wrappingPaperTotalSquareFeet = 0;

            foreach (Box box in boxes)
            {
                int wrappingPaperBoxSquareFeet =
                    2 * box.Length * box.Width
                    + 2 * box.Width * box.Height
                    + 2 * box.Height * box.Length
                    + (new int[] { box.Length * box.Width, box.Width * box.Height, box.Height * box.Length }).Min();

                wrappingPaperTotalSquareFeet += wrappingPaperBoxSquareFeet;
            }

            return wrappingPaperTotalSquareFeet;
        }

        public int CalculateTotalFeetOfRribbon(List<Box> boxes)
        {
            int ribbonTotalFeet = 0;

            foreach (Box box in boxes)
            {
                int[] boxSides = new int[] { box.Length, box.Width, box.Height };
                Array.Sort(boxSides);

                int ribbonBoxFeet =
                    2 * boxSides[0]
                    + 2 * boxSides[1]
                    + box.Length * box.Width * box.Height;

                ribbonTotalFeet += ribbonBoxFeet;
            }

            return ribbonTotalFeet;
        }
    }
}
