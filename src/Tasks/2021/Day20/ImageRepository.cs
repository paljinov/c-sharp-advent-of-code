using System;

namespace App.Tasks.Year2021.Day20
{
    public class ImageRepository
    {
        public char[] GetImageEnhancementAlgorithm(string input)
        {
            string[] inputParts = ParseInput(input);
            char[] imageEnhancementAlgorithm = inputParts[0].ToCharArray();

            return imageEnhancementAlgorithm;
        }

        public char[,] GetInputImage(string input)
        {
            string[] inputParts = ParseInput(input);

            string[] inputImageString = inputParts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = inputImageString.Length;
            int columns = inputImageString[0].Length;
            char[,] inputImage = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    inputImage[i, j] = inputImageString[i][j];
                }
            }

            return inputImage;
        }

        private string[] ParseInput(string input)
        {
            string[] inputParts = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );

            return inputParts;
        }
    }
}
