using System.Collections.Generic;

namespace App.Tasks.Year2020.Day6
{
    public class AnswersCounter
    {
        public int CountAnyoneYesAnswers(string[][] personsAnswers)
        {
            int anyoneYesAnswers = 0;

            for (int i = 0; i < personsAnswers.GetLength(0); i++)
            {
                anyoneYesAnswers += CountAnyoneYesAnswersForGroup(personsAnswers[i]);
            }

            return anyoneYesAnswers;
        }

        public int CountEveryoneYesAnswers(string[][] personsAnswers)
        {
            int everyoneYesAnswers = 0;

            for (int i = 0; i < personsAnswers.GetLength(0); i++)
            {
                everyoneYesAnswers += CountEveryoneYesAnswersForGroup(personsAnswers[i]);
            }

            return everyoneYesAnswers;
        }

        private int CountAnyoneYesAnswersForGroup(string[] groupAnswers)
        {
            List<char> anyoneYesAnswers = new List<char>();

            foreach (string personAnswers in groupAnswers)
            {
                foreach (char personAnswer in personAnswers)
                {
                    if (!anyoneYesAnswers.Contains(personAnswer))
                    {
                        anyoneYesAnswers.Add(personAnswer);
                    }
                }
            }

            return anyoneYesAnswers.Count;
        }

        private int CountEveryoneYesAnswersForGroup(string[] groupAnswers)
        {
            Dictionary<char, int> answerOccurrences = new Dictionary<char, int>();

            foreach (string personAnswers in groupAnswers)
            {
                foreach (char personAnswer in personAnswers)
                {
                    if (answerOccurrences.ContainsKey(personAnswer))
                    {
                        answerOccurrences[personAnswer]++;
                    }
                    else
                    {
                        answerOccurrences.Add(personAnswer, 1);
                    }
                }
            }

            int everyoneYesAnswers = 0;
            int totalPersonsInGroup = groupAnswers.Length;

            foreach (KeyValuePair<char, int> answer in answerOccurrences)
            {
                if (answer.Value == totalPersonsInGroup)
                {
                    everyoneYesAnswers++;
                }
            }

            return everyoneYesAnswers;
        }
    }
}
