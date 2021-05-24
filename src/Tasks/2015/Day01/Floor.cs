namespace App.Tasks.Year2015.Day1
{
    public class Floor
    {
        private const char UP = '(';

        public (int resultFloor, int stepsToEnterBasementFirstTime) FindResultFloorAndStepsToEnterBasementFirstTime(
            string parentheses
        )
        {
            int resultFloor = 0;
            int stepsToEnterBasementFirstTime = 0;

            for (int i = 0; i < parentheses.Length; i++)
            {
                if (parentheses[i] == UP)
                {
                    resultFloor += 1;
                }
                else
                {
                    resultFloor -= 1;
                }

                if (stepsToEnterBasementFirstTime == 0 && resultFloor == -1)
                {
                    stepsToEnterBasementFirstTime = i + 1;
                }
            }

            return (resultFloor, stepsToEnterBasementFirstTime);
        }
    }
}
