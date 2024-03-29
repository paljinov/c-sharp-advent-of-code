using System;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2018.Day5
{
    public class Polymer
    {
        public int CountRemainingUnitsAfterFullyReactingThePolymer(string units)
        {
            StringBuilder unitsSb = new StringBuilder(units);
            ScanPolymer(unitsSb);

            return unitsSb.Length;
        }

        public int CalculateLengthOfShortestPolymerByRemovingAllUnitsOfExactlyOneType(string units)
        {
            int lengthOfShortestPolymerByRemovingAllUnitsOfExactlyOneType = int.MaxValue;

            char[] distinctLetters = units.ToLower().Distinct().ToArray();

            foreach (char letter in distinctLetters)
            {
                StringBuilder unitsSb = new StringBuilder(units
                    .Replace(letter.ToString().ToUpper(), string.Empty)
                    .Replace(letter.ToString(), string.Empty));

                ScanPolymer(unitsSb);

                lengthOfShortestPolymerByRemovingAllUnitsOfExactlyOneType =
                    Math.Min(lengthOfShortestPolymerByRemovingAllUnitsOfExactlyOneType, unitsSb.Length);
            }

            return lengthOfShortestPolymerByRemovingAllUnitsOfExactlyOneType;
        }

        private void ScanPolymer(StringBuilder units)
        {
            bool reaction = false;

            for (int i = 0; i < units.Length - 1; i++)
            {
                char a = units[i];
                char b = units[i + 1];

                if (char.ToUpperInvariant(a) == char.ToUpperInvariant(b))
                {
                    if (char.IsUpper(a) && !char.IsUpper(b) || char.IsLower(a) && !char.IsLower(b))
                    {
                        units.Remove(i, 2);
                        reaction = true;
                    }
                }
            }

            if (reaction)
            {
                ScanPolymer(units);
            }
        }
    }
}
