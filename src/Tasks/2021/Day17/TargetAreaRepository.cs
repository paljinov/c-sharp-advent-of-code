using System.Text.RegularExpressions;

namespace App.Tasks.Year2021.Day17
{
    public class TargetAreaRepository
    {
        public TargetArea GetTargetArea(string input)
        {
            Regex targetAreaRegex = new Regex(@"^target area:\sx=(\d+)\.\.(\d+),\sy=(-?\d+)\.\.(-?\d+)$");

            Match targetAreaMatch = targetAreaRegex.Match(input);
            GroupCollection targetAreaGroups = targetAreaMatch.Groups;

            TargetArea targetArea = new TargetArea
            {
                X = (int.Parse(targetAreaGroups[1].Value), int.Parse(targetAreaGroups[2].Value)),
                Y = (int.Parse(targetAreaGroups[3].Value), int.Parse(targetAreaGroups[4].Value))
            };

            return targetArea;
        }
    }
}
