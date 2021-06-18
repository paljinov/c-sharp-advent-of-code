using System.Linq;
using System.Text;

namespace App.Tasks.Year2019.Day8
{
    public class PasswordImage
    {
        private readonly int imageWidth = 25;

        private readonly int imageHeight = 6;

        private const char BLACK = '0';

        private const char WHITE = '1';

        private const char BLACK_PRINT = '.';

        private const char WHITE_PRINT = '#';

        public int CalculateNumberOneAndNumberTwoDigitsProductOfLayerThatContainsFewestZeroDigits(string password)
        {
            string[] layers = GetLayers(password);

            string layerThatContainsFewestZeroDigits = "";
            int layerThatContainsFewestZeroDigitsCount = int.MaxValue;

            for (int i = 0; i < layers.Length; i++)
            {
                int layerZeroDigitsCount = layers[i].Count(l => l == '0');

                if (layerZeroDigitsCount < layerThatContainsFewestZeroDigitsCount)
                {
                    layerThatContainsFewestZeroDigits = layers[i];
                    layerThatContainsFewestZeroDigitsCount = layerZeroDigitsCount;
                }
            }

            int numberOneAndNumberTwoDigitsProduct = layerThatContainsFewestZeroDigits.Count(l => l == '1')
                * layerThatContainsFewestZeroDigits.Count(l => l == '2');

            return numberOneAndNumberTwoDigitsProduct;
        }

        public string GetMessageProducedAfterDecodingImage(string password)
        {
            string[] layers = GetLayers(password);

            int layerLength = layers.First().Length;
            char[] image = new char[layerLength];

            StringBuilder message = new StringBuilder();
            message.AppendLine();

            int rowChars = 0;

            for (int j = 0; j < layerLength; j++)
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    if (rowChars == imageWidth)
                    {
                        message.AppendLine();
                        rowChars = 0;
                    }

                    if (layers[i][j] == BLACK)
                    {
                        image[j] = BLACK;
                        message.Append(BLACK_PRINT);
                        rowChars++;
                        break;
                    }

                    if (layers[i][j] == WHITE)
                    {
                        image[j] = WHITE;
                        message.Append(WHITE_PRINT);
                        rowChars++;
                        break;
                    }
                }
            }

            return message.ToString();
        }

        private string[] GetLayers(string password)
        {
            int size = imageWidth * imageHeight;
            int layersCount = password.Length / size;

            string[] layers = new string[layersCount];

            for (int i = 0; i < layersCount; i++)
            {
                string layer = password.Substring(i * size, size);
                layers[i] = layer;
            }

            return layers;
        }
    }
}
