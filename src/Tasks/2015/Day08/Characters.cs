using System.Globalization;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day8
{
    public class Characters
    {
        public int CalculateStringLiteralsMinusStringMemoryDiff(string[] stringsCode)
        {
            int stringLiteralsMinusStringMemoryDiff = 0;

            foreach (string stringCode in stringsCode)
            {
                string stringInMemory = GetStringInMemory(stringCode);
                stringLiteralsMinusStringMemoryDiff += stringCode.Length - stringInMemory.Length;
            }

            return stringLiteralsMinusStringMemoryDiff;
        }

        public int CalculateEncodedStringMinusStringLiteralsDiff(string[] decodedStrings)
        {
            int encodedStringMinusStringLiteralsDiff = 0;

            foreach (string decodedString in decodedStrings)
            {
                string encodedString = GetEncodedString(decodedString);
                encodedStringMinusStringLiteralsDiff += encodedString.Length - decodedString.Length;
            }

            return encodedStringMinusStringLiteralsDiff;
        }

        private string GetStringInMemory(string stringCode)
        {
            // Remove start and end quote
            string stringInMemory = stringCode.Trim(new char[] { '"' });
            // Remove backslash escape
            stringInMemory = stringInMemory.Replace(@"\\", @"\");
            // Remove quote escape
            stringInMemory = stringInMemory.Replace("\\\"", "\"");

            // Find and replace hexadecimal regex with character
            Regex hexadecimalCharacterRegex = new Regex(@"\\x([0-9a-f]{2})");
            MatchCollection hexadecimalCharacterMatches = hexadecimalCharacterRegex.Matches(stringInMemory);
            if (hexadecimalCharacterMatches.Count > 0)
            {
                foreach (Match hexadecimalCharacterMatch in hexadecimalCharacterMatches)
                {
                    GroupCollection groups = hexadecimalCharacterMatch.Groups;
                    char character = (char)int.Parse(groups[1].Value, NumberStyles.AllowHexSpecifier);

                    stringInMemory = stringInMemory.Replace(groups[0].Value, character.ToString());
                }
            }

            return stringInMemory;
        }

        private string GetEncodedString(string decodedString)
        {
            // Add backslash escape
            string encodedString = decodedString.Replace(@"\", @"\\");
            // Add quote escape
            encodedString = encodedString.Replace("\"", "\\\"");
            // Surround with quotes
            encodedString = "\"" + encodedString + "\"";

            return encodedString;
        }
    }
}
