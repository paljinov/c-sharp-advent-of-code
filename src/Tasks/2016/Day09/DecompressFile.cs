using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day9
{
    public class DecompressFile
    {
        private const string MARKER_PATTERN = @"(\(\d+x\d+\))";

        public long GetDecompressedFileLength(string file, FileCompressionFormat fileCompressionFormat)
        {
            long decompressedFileLength = 0;

            string[] fileParts = SplitFileByMarkers(file);
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

                    string nextSequence = GetNextSequenceAfterCurrentMarker(i, fileParts, sequenceLength);

                    // If compression format is version two and next sequence contains markers
                    if (fileCompressionFormat == FileCompressionFormat.VersionTwo &&
                        Regex.Match(nextSequence, MARKER_PATTERN).Success)
                    {
                        decompressedFileLength +=
                            repeat * GetDecompressedFileLength(nextSequence, fileCompressionFormat);
                    }
                    // If compression format is version one or next sequence doesn't contain markers
                    else
                    {
                        decompressedFileLength += sequenceLength * repeat;
                    }
                }
                // If subsequence is normal data
                else
                {
                    decompressedFileLength += subsequence.Length;
                }
            }

            return decompressedFileLength;
        }

        private string[] SplitFileByMarkers(string file)
        {
            string[] fileParts = Regex.Split(file, MARKER_PATTERN).Where(s => s != string.Empty).ToArray();
            return fileParts;
        }

        private string GetNextSequenceAfterCurrentMarker(int markerIndex, string[] fileParts, int sequenceLength)
        {
            StringBuilder nextSequence = new StringBuilder();
            int i = markerIndex + 1;
            while (nextSequence.Length < sequenceLength)
            {
                string nextSubsequence = fileParts[i];

                if (nextSequence.Length + nextSubsequence.Length <= sequenceLength)
                {
                    nextSequence.Append(nextSubsequence);
                }
                else
                {
                    nextSequence.Append(nextSubsequence[..(sequenceLength - nextSequence.Length)]);
                }

                i++;
            }

            return nextSequence.ToString();
        }
    }
}
