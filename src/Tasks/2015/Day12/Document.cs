using System.Text.Json;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day12
{
    public class Document
    {
        public int CalculateSumOfAllNumbersInTheDocument(string input)
        {
            int sum = 0;

            Regex digitRegex = new Regex(@"(-?\d+)");
            MatchCollection digitMatches = digitRegex.Matches(input);

            foreach (Match digitMatch in digitMatches)
            {
                GroupCollection groups = digitMatch.Groups;
                sum += int.Parse(groups[1].Value);
            }

            return sum;
        }

        public int CalculateSumOfAllNumbersInTheDocumentWhenIgnoringObjectPropertiesWithValueRed(string input)
        {
            int sum = 0;

            JsonDocument jsonDocument = JsonDocument.Parse(input);
            JsonElement rootElement = jsonDocument.RootElement;

            IterateJsonElement(rootElement, ref sum);

            return sum;
        }

        /// <summary>
        /// Recursively iterates JSON element.
        /// </summary>
        /// <param name="jsonElement"></param>
        /// <param name="sum"></param>
        private void IterateJsonElement(JsonElement jsonElement, ref int sum)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Object:
                    if (!IsJsonObjectElementAndContainsRed(jsonElement))
                    {
                        foreach (JsonProperty property in jsonElement.EnumerateObject())
                        {
                            JsonElement element = property.Value;
                            IterateJsonElement(element, ref sum);
                        }
                    }
                    break;
                case JsonValueKind.Array:
                    if (!IsJsonObjectElementAndContainsRed(jsonElement))
                    {
                        foreach (JsonElement element in jsonElement.EnumerateArray())
                        {
                            IterateJsonElement(element, ref sum);
                        }
                    }
                    break;
                case JsonValueKind.Number:
                    sum += jsonElement.GetInt32();
                    break;
            }
        }

        private bool IsJsonObjectElementAndContainsRed(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty pro in element.EnumerateObject())
                {
                    JsonElement el = pro.Value;
                    if (el.ValueKind == JsonValueKind.String && el.GetString() == "red")
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
