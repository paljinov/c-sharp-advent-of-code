using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day7
{
    public class Steps
    {
        public string FindOrderOfStepsInWhichInstructionsAreCompleted(HashSet<(char finished, char begin)> instructions)
        {
            string stepsOrder = string.Empty;

            List<char> steps = GetSortedSteps(instructions);

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

        public int CalculateSecondsNeededToCompleteAllSteps(
            HashSet<(char finished, char begin)> instructions,
            int totalWorkers,
            int baseStepDuration
        )
        {
            int secondsNeededToCompleteAllSteps = 0;

            List<char> steps = GetSortedSteps(instructions);
            Dictionary<char, int> stepsSeconds = new Dictionary<char, int>();

            while (steps.Count > 0 || stepsSeconds.Count > 0)
            {
                foreach (char step in steps)
                {
                    if (!stepsSeconds.ContainsKey(step) && stepsSeconds.Count < totalWorkers)
                    {
                        if (!instructions.Any(s => s.begin == step))
                        {
                            stepsSeconds[step] = CalculateStepSeconds(baseStepDuration, step);
                        }
                    }
                }

                foreach (KeyValuePair<char, int> stepSeconds in stepsSeconds)
                {
                    if (stepSeconds.Value > 0)
                    {
                        stepsSeconds[stepSeconds.Key] -= 1;
                        if (stepsSeconds[stepSeconds.Key] == 0)
                        {
                            // Remove used step instructions
                            steps.Remove(stepSeconds.Key);
                            instructions.RemoveWhere(s => s.finished == stepSeconds.Key);
                            stepsSeconds.Remove(stepSeconds.Key);
                        }
                    }
                }

                secondsNeededToCompleteAllSteps++;
            }

            return secondsNeededToCompleteAllSteps;
        }

        private List<char> GetSortedSteps(HashSet<(char finished, char begin)> instructions)
        {
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

            return steps;
        }

        private int CalculateStepSeconds(int baseStepDuration, char letter)
        {
            int asciiCodesBeforeFirstCapitalLetter = 64;
            int stepSeconds = baseStepDuration + (int)letter - asciiCodesBeforeFirstCapitalLetter;

            return stepSeconds;
        }
    }
}
