using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2016.Day3
{
    public class Triangle
    {
        public int CountTriangles(List<Sides> designDocument)
        {
            int triangles = 0;

            foreach (Sides sides in designDocument)
            {
                if (IsTriangle(sides))
                {
                    triangles++;
                }
            }

            return triangles;
        }

        private bool IsTriangle(Sides sides)
        {
            List<int> sidesList = new List<int>()
            {
                sides.A,
                sides.B,
                sides.C
            };

            int largestSide = sidesList.Max();
            sidesList.Remove(largestSide);

            if (sidesList.Sum() > largestSide)
            {
                return true;
            }

            return false;
        }
    }
}
