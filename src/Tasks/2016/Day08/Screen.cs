using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2016.Day8
{
    public class Screen
    {
        private const int SCREEN_WIDTH = 50;

        private const int SCREEN_HEIGHT = 6;

        public int CountLitPixels(List<RectangleInstructions> instructions)
        {
            bool[,] screen = new bool[SCREEN_HEIGHT, SCREEN_WIDTH];

            foreach (RectangleInstructions instruction in instructions)
            {
                screen = CreateRectangle(instruction.Rectangle, screen);
                foreach (Axis axis in instruction.AxesRotations)
                {
                    if (axis.Name == 'x')
                    {
                        screen = RotateColumn(axis, screen);
                    }
                    else if (axis.Name == 'y')
                    {
                        screen = RotateRow(axis, screen);
                    }
                }
            }

            int litPixels = DoCountLitPixels(screen);

            return litPixels;
        }

        private bool[,] CreateRectangle(Rectangle rectangle, bool[,] screen)
        {
            for (int i = 0; i < rectangle.X; i++)
            {
                for (int j = 0; j < rectangle.Y; j++)
                {
                    screen[i, j] = true;
                }
            }

            return screen;
        }

        private bool[,] RotateRow(Axis axis, bool[,] screen)
        {
            bool[] row = new bool[SCREEN_WIDTH];
            for (int j = 0; j < row.Length; j++)
            {
                row[j] = screen[axis.Value, j];
            }

            bool[] rotatedRow = RotateAxis(row, axis.RotateBy);
            for (int j = 0; j < rotatedRow.Length; j++)
            {
                screen[axis.Value, j] = rotatedRow[j];
            }

            return screen;
        }

        private bool[,] RotateColumn(Axis axis, bool[,] screen)
        {
            bool[] column = new bool[SCREEN_HEIGHT];
            for (int i = 0; i < column.Length; i++)
            {
                column[i] = screen[i, axis.Value];
            }

            bool[] rotatedColumn = RotateAxis(column, axis.RotateBy);
            for (int i = 0; i < rotatedColumn.Length; i++)
            {
                screen[i, axis.Value] = rotatedColumn[i];
            }

            return screen;
        }

        private bool[] RotateAxis(bool[] axis, int rotateBy)
        {
            bool[] rotatedAxis = new bool[axis.Length];

            for (int i = 0; i < axis.Length; i++)
            {
                int shiftedIndex = i + rotateBy;
                if (shiftedIndex >= axis.Length)
                {
                    shiftedIndex -= axis.Length;
                }

                rotatedAxis[shiftedIndex] = axis[i];
            }

            return rotatedAxis;
        }

        private int DoCountLitPixels(bool[,] screen)
        {
            int litPixels = 0;

            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int j = 0; j < screen.GetLength(1); j++)
                {
                    if (screen[i, j])
                    {
                        litPixels++;
                    }
                }
            }

            return litPixels;
        }
    }
}
