using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day9
{
    public class DecompressFile
    {
        private const string MARKER_PATTERN = @"(\(\d+x\d+\))";

        public int GetVersionOneDecompressedFileLength(string file)
        {
            int decompressedFileLength = 0;

            string[] fileParts = GetFileParts(file);
            int sequenceLength = 0;

            for (int i = 0; i < fileParts.Length; i++)
            {
                string subsequence = fileParts[i];

                // If whole or part of subsequence is already decompressed
                if (sequenceLength > 0)
                {
                    // If whole subsequence is already decompressed we skip it
                    if (sequenceLength - subsequence.Length >= 0)
                    {
                        sequenceLength -= subsequence.Length;
                        continue;
                    }
                    // If part of subsequence is already decompressed
                    else
                    {
                        subsequence = subsequence[sequenceLength..];
                        sequenceLength = 0;
                    }
                }

                Match markerMatch = Regex.Match(subsequence, MARKER_PATTERN);
                // If subsequence is marker
                if (markerMatch.Success)
                {
                    string[] markerParts = subsequence[1..^1].Split('x');
                    sequenceLength = int.Parse(markerParts[0]);
                    int repeat = int.Parse(markerParts[1]);

                    decompressedFileLength += sequenceLength * repeat;
                }
                // If subsequence is normal data
                else
                {
                    decompressedFileLength += subsequence.Length;
                }

            }

            return decompressedFileLength;
        }
        private string[] GetFileParts(string file)
        {
            string[] fileParts = Regex.Split(file, MARKER_PATTERN).Where(s => s != string.Empty).ToArray();
            return fileParts;
        }
    }
}
