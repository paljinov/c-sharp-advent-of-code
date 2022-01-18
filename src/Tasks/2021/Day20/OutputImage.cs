using System;
using System.Collections.Generic;

namespace App.Tasks.Year2021.Day20
{
    public class OutputImage
    {
        private const char DARK_PIXEL = '.';
        private const char LIGHT_PIXEL = '#';

        private static readonly Dictionary<char, char> turnPixelsInto = new Dictionary<char, char>()
        {
            { DARK_PIXEL, '0' },
            { LIGHT_PIXEL, '1' }
        };

        public int CountLitPixelsInTheResultingImage(
            char[,] inputImage,
            string imageEnhancementAlgorithm,
            int totalImageEnhancements
        )
        {
            char[,] outputImage = inputImage.Clone() as char[,];
            for (int i = 0; i < totalImageEnhancements; i++)
            {
                outputImage = EnhanceImage(outputImage, imageEnhancementAlgorithm);
            }

            int litPixels = CountImageLitPixels(outputImage);

            return litPixels;
        }

        private char[,] EnhanceImage(char[,] inputImage, string imageEnhancementAlgorithm)
        {
            char[,] outputImage = InitializeOutputImage(inputImage);
            char[,] enhancedOutputImage = outputImage.Clone() as char[,];

            for (int i = 0; i < outputImage.GetLength(0); i++)
            {
                for (int j = 0; j < outputImage.GetLength(1); j++)
                {
                    enhancedOutputImage[i, j] = GetOutputPixel((i, j), outputImage, imageEnhancementAlgorithm);
                }
            }

            return enhancedOutputImage;
        }

        private char[,] InitializeOutputImage(char[,] inputImage)
        {
            char[,] outputImage = new char[inputImage.GetLength(0) + 2, inputImage.GetLength(1) + 2];

            // Initialize output image
            for (int i = 0; i < outputImage.GetLength(0); i++)
            {
                for (int j = 0; j < outputImage.GetLength(1); j++)
                {
                    outputImage[i, j] = DARK_PIXEL;
                    // If inside input image area
                    if (i > 0 && i < outputImage.GetLength(0) - 1 && j > 0 && j < outputImage.GetLength(1) - 1)
                    {
                        outputImage[i, j] = inputImage[i - 1, j - 1];
                    }
                }
            }

            return outputImage;
        }

        private char GetOutputPixel((int X, int Y) pixel, char[,] image, string imageEnhancementAlgorithm)
        {
            List<char> pixels = new List<char>();

            // Start from the top-left and read across each row
            for (int i = pixel.X - 1; i <= pixel.X + 1; i++)
            {
                for (int j = pixel.Y - 1; j <= pixel.Y + 1; j++)
                {
                    // If outside image area
                    if (i < 0 || i >= image.GetLength(0) || j < 0 || j >= image.GetLength(1))
                    {
                        pixels.Add(DARK_PIXEL);
                    }
                    else
                    {
                        pixels.Add(image[i, j]);
                    }
                }
            }

            string pixelsString = string.Join("", pixels);
            string binaryNumber = ConvertPixelsToBinaryNumber(pixelsString);
            int decimalNumber = ConvertBinaryToDecimal(binaryNumber);

            char outputPixel = imageEnhancementAlgorithm[decimalNumber];

            return outputPixel;
        }

        public int CountImageLitPixels(char[,] image)
        {
            int litPixels = 0;

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    if (image[i, j] == LIGHT_PIXEL)
                    {
                        litPixels++;
                    }
                }
            }

            return litPixels;
        }

        private string ConvertPixelsToBinaryNumber(string pixels)
        {
            string binaryNumber = pixels
                .Replace(DARK_PIXEL, turnPixelsInto[DARK_PIXEL])
                .Replace(LIGHT_PIXEL, turnPixelsInto[LIGHT_PIXEL]);

            return binaryNumber;
        }

        private int ConvertBinaryToDecimal(string binary)
        {
            int integer = Convert.ToInt32(binary, 2);
            return integer;
        }
    }
}
