using System;
using System.Collections.Generic;
using System.Text;

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
            // Pixels which are outside input image area
            char outsidePixelsType = DARK_PIXEL;

            for (int i = 0; i < totalImageEnhancements; i++)
            {
                outputImage = EnhanceImage(outputImage, imageEnhancementAlgorithm, outsidePixelsType);
                outsidePixelsType = CalculateOutsidePixelsType(outsidePixelsType, imageEnhancementAlgorithm);
            }

            int litPixels = CountImageLitPixels(outputImage);

            return litPixels;
        }

        private char[,] EnhanceImage(char[,] inputImage, string imageEnhancementAlgorithm, char outsidePixelsType)
        {
            char[,] outputImage = InitializeOutputImage(inputImage, outsidePixelsType);
            char[,] enhancedOutputImage = outputImage.Clone() as char[,];

            for (int i = 0; i < outputImage.GetLength(0); i++)
            {
                for (int j = 0; j < outputImage.GetLength(1); j++)
                {
                    enhancedOutputImage[i, j] =
                        GetOutputPixel((i, j), outputImage, imageEnhancementAlgorithm, outsidePixelsType);
                }
            }

            return enhancedOutputImage;
        }

        private char[,] InitializeOutputImage(char[,] inputImage, char outsidePixelsType)
        {
            char[,] outputImage = new char[inputImage.GetLength(0) + 2, inputImage.GetLength(1) + 2];

            // Initialize output image
            for (int i = 0; i < outputImage.GetLength(0); i++)
            {
                for (int j = 0; j < outputImage.GetLength(1); j++)
                {
                    outputImage[i, j] = outsidePixelsType;
                    // If inside input image area
                    if (i > 0 && i < outputImage.GetLength(0) - 1 && j > 0 && j < outputImage.GetLength(1) - 1)
                    {
                        outputImage[i, j] = inputImage[i - 1, j - 1];
                    }
                }
            }

            return outputImage;
        }

        private char GetOutputPixel(
            (int X, int Y) pixelCoordinates,
            char[,] image,
            string imageEnhancementAlgorithm,
            char outsidePixelsType
        )
        {
            StringBuilder binaryNumber = new StringBuilder();

            // Start from the top-left and read across each row
            for (int i = pixelCoordinates.X - 1; i <= pixelCoordinates.X + 1; i++)
            {
                for (int j = pixelCoordinates.Y - 1; j <= pixelCoordinates.Y + 1; j++)
                {
                    // If inside image area
                    if (i >= 0 && i < image.GetLength(0) && j >= 0 && j < image.GetLength(1))
                    {
                        binaryNumber.Append(turnPixelsInto[image[i, j]]);
                    }
                    // If outside image area
                    else
                    {
                        binaryNumber.Append(turnPixelsInto[outsidePixelsType]);
                    }
                }
            }

            int decimalNumber = ConvertBinaryToDecimal(binaryNumber.ToString());
            char outputPixel = imageEnhancementAlgorithm[decimalNumber];

            return outputPixel;
        }

        private char CalculateOutsidePixelsType(char outsidePixelsType, string imageEnhancementAlgorithm)
        {
            // If pixel at first location is dark, outside pixels always remain dark, also
            // note that combination of first light pixel and last light pixel doesn't make
            // sense because there would be infinite lit pixels

            // If pixel at first location is light and pixel at last location is dark
            if (imageEnhancementAlgorithm[0] == LIGHT_PIXEL && imageEnhancementAlgorithm[^1] == DARK_PIXEL)
            {
                outsidePixelsType = outsidePixelsType == DARK_PIXEL ? LIGHT_PIXEL : DARK_PIXEL;
            }

            return outsidePixelsType;
        }

        private int CountImageLitPixels(char[,] image)
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

        private int ConvertBinaryToDecimal(string binary)
        {
            int integer = Convert.ToInt32(binary, 2);
            return integer;
        }
    }
}
