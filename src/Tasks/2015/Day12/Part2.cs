/*
--- Part Two ---

Uh oh - the Accounting-Elves have realized that they double-counted everything
red.

Ignore any object (and all of its children) which has any property with the
value "red". Do this only for objects ({...}), not arrays ([...]).

- [1,2,3] still has a sum of 6.
- [1,{"c":"red","b":2},3] now has a sum of 4, because the middle object is
  ignored.
- {"d":"red","e":[1,2,3,4],"f":5} now has a sum of 0, because the entire
  structure is ignored.
- [1,"red",5] has a sum of 6, because "red" in an array has no effect.
*/

using System.Text.Json;

namespace App.Tasks.Year2015.Day12
{
    public class Part2 : ITask<int>
    {
        public int Solution(string input)
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
                    foreach (JsonProperty property in jsonElement.EnumerateObject())
                    {
                        JsonElement element = property.Value;
                        if (!IsJsonObjectElementAndContainsRed(element))
                        {
                            IterateJsonElement(element, ref sum);
                        }
                    }
                    break;
                case JsonValueKind.Array:
                    foreach (JsonElement element in jsonElement.EnumerateArray())
                    {
                        if (!IsJsonObjectElementAndContainsRed(element))
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
