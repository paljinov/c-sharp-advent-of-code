using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day9
{
    public class File
    {
        private const string MARKER_PATTERN = @"(\(\d+x\d+\))";

        public int GetDecompressedLength(string file)
        {
            string[] fileParts = ParseFile(file);

            StringBuilder decompressedFile = new StringBuilder();
            int skipLength = 0;

            for (int i = 0; i < fileParts.Length; i++)
            {
                string part = fileParts[i];

                if (skipLength > 0)
                {
                    if (skipLength - part.Length >= 0)
                    {
                        skipLength -= part.Length;
                        continue;
                    }
                    else if (skipLength > 0)
                    {
                        part = part[skipLength..];
                        skipLength = 0;
                    }
                }

                Match markerMatch = Regex.Match(part, MARKER_PATTERN);

                if (markerMatch.Success)
                {
                    string[] markerParts = part[1..^1].Split('x');
                    int sequenceLength = int.Parse(markerParts[0]);
                    int repeat = int.Parse(markerParts[1]);

                    skipLength = sequenceLength;

                    for (int j = 0; j < repeat; j++)
                    {
                        sequenceLength = skipLength;
                        for (int k = i + 1; k < fileParts.Length; k++)
                        {
                            if (sequenceLength - fileParts[k].Length >= 0)
                            {
                                sequenceLength -= fileParts[k].Length;
                                decompressedFile.Append(fileParts[k]);
                                continue;
                            }
                            else if (sequenceLength > 0)
                            {
                                decompressedFile.Append(fileParts[k][..sequenceLength]);
                                sequenceLength = 0;
                            }

                            if (skipLength == 0)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    decompressedFile.Append(part);
                }
            }

            return decompressedFile.ToString().Length;
        }

        private string[] ParseFile(string file)
        {
            string[] fileParts = Regex.Split(file, MARKER_PATTERN).Where(s => s != string.Empty).ToArray();
            return fileParts;
        }
    }
}
