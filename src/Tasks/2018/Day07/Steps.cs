using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day7
{
    public class Steps
    {
        public string FindOrderOfStepsInWhichInstructionsAreCompleted(HashSet<(char finished, char begin)> instructions)
        {
            string stepsOrder = string.Empty;

            List<char> steps = new List<char>();
            foreach ((char finished, char begin) in instructions)
            {
                if (!steps.Contains(finished))
                {
                    steps.Add(finished);
                }

                if (!steps.Contains(begin))
                {
                    steps.Add(begin);
                }
            }
            steps.Sort();

            while (steps.Count > 0)
            {
                char nextStep = '\0';
                foreach (char step in steps)
                {
                    if (!instructions.Any(s => s.begin == step))
                    {
                        nextStep = step;
                        break;
                    }
                }

                stepsOrder += nextStep;

                // Remove used step instructions
                steps.Remove(nextStep);
                instructions.RemoveWhere(s => s.finished == nextStep);
            }

            return stepsOrder;
        }
    }
}
