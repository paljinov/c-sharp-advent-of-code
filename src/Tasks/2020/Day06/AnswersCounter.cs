using System.Collections.Generic;

namespace App.Tasks.Year2020.Day6
{
    public class AnswersCounter
    {
        public int CountAnyoneYesAnswers(string[][] personsAnswers)
        {
            int anyoneYesAnswers = 0;

            for (int i = 0; i < personsAnswers.Length; i++)
            {
                anyoneYesAnswers += CountAnyoneYesAnswersForGroup(personsAnswers[i]);
            }

            return anyoneYesAnswers;
        }

        public int CountEveryoneYesAnswers(string[][] personsAnswers)
        {
            int everyoneYesAnswers = 0;

            for (int i = 0; i < personsAnswers.Length; i++)
            {
                everyoneYesAnswers += CountEveryoneYesAnswersForGroup(personsAnswers[i]);
            }

            return everyoneYesAnswers;
        }

        private int CountAnyoneYesAnswersForGroup(string[] groupAnswers)
        {
            // Different answers for a group
            Dictionary<char, int> differentAnswers = new Dictionary<char, int>();

            foreach (string personAnswers in groupAnswers)
            {
                foreach (char personAnswer in personAnswers)
                {
                    // If this is a new person answer
                    if (!differentAnswers.ContainsKey(personAnswer))
                    {
                        differentAnswers.Add(personAnswer, 1);
                    }
                }
            }

            return differentAnswers.Count;
        }

        private int CountEveryoneYesAnswersForGroup(string[] groupAnswers)
        {
            // Each answer occurrences for a group
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
            foreach (KeyValuePair<char, int> answer in answerOccurrences)
            {
                // If everyone answered "yes" to this question
                if (answer.Value == groupAnswers.Length)
                {
                    everyoneYesAnswers++;
                }
            }

            return everyoneYesAnswers;
        }
    }
}
