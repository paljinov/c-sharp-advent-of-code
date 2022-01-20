/*
--- Part Two ---

You still can't quite make out the details in the image. Maybe you just didn't
enhance it enough.

If you enhance the starting input image in the above example a total of 50
times, 3351 pixels are lit in the final output image.

Start again with the original input image and apply the image enhancement
algorithm 50 times. How many pixels are lit in the resulting image?
*/

namespace App.Tasks.Year2021.Day20
{
    public class Part2 : ITask<int>
    {
        private const int TOTAL_IMAGE_ENHANCEMENTS = 50;

        private readonly ImageRepository imageRepository;

        private readonly OutputImage outputImage;

        public Part2()
        {
            imageRepository = new ImageRepository();
            outputImage = new OutputImage();
        }

        public int Solution(string input)
        {
            char[,] inputImage = imageRepository.GetInputImage(input);
            string imageEnhancementAlgorithm = imageRepository.GetImageEnhancementAlgorithm(input);

            int litPixels = outputImage.CountLitPixelsInTheResultingImage(
                inputImage, imageEnhancementAlgorithm, TOTAL_IMAGE_ENHANCEMENTS);

            return litPixels;
        }
    }
}
