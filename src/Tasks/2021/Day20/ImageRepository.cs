using System;

namespace App.Tasks.Year2021.Day20
{
    public class ImageRepository
    {
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

        public string GetImageEnhancementAlgorithm(string input)
        {
            string[] inputParts = ParseInput(input);
            string imageEnhancementAlgorithm = inputParts[0].Replace(Environment.NewLine, string.Empty);

            return imageEnhancementAlgorithm;
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
