/*
--- Part Two ---

This important natural resource will need to last for at least thousands of
years. Are the Elves collecting this lumber sustainably?

What will the total resource value of the lumber collection area be after
1000000000 minutes?
*/

namespace App.Tasks.Year2018.Day18
{
    public class Part2 : ITask<int>
    {
        private const int TOTAL_MINUTES = 1000000000;

        private readonly AreaRepository areaRepository;

        private readonly Resources resources;

        public Part2()
        {
            areaRepository = new AreaRepository();
            resources = new Resources();
        }
        public int Solution(string input)
        {
            char[,] area = areaRepository.GetArea(input);
            int totalResourceValue = resources
                .CalculateTotalResourceValueOfTheLumberCollectionAreaAfterMinutes(area, TOTAL_MINUTES);

            return totalResourceValue;
        }
    }
}
