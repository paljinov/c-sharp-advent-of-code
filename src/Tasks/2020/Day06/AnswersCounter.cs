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
            Dictionary<char, int> yesAnswerOccurrences = CountYesAnswerOccurrencesForGroup(groupAnswers);

            return yesAnswerOccurrences.Count;
        }

        private int CountEveryoneYesAnswersForGroup(string[] groupAnswers)
        {
            int everyoneYesAnswers = 0;

            Dictionary<char, int> yesAnswerOccurrences = CountYesAnswerOccurrencesForGroup(groupAnswers);
            foreach (KeyValuePair<char, int> answer in yesAnswerOccurrences)
            {
                // If everyone answered "yes" to this question
                if (answer.Value == groupAnswers.Length)
                {
                    everyoneYesAnswers++;
                }
            }

            return everyoneYesAnswers;
        }

        private Dictionary<char, int> CountYesAnswerOccurrencesForGroup(string[] groupAnswers)
        {
            Dictionary<char, int> yesAnswerOccurrences = new Dictionary<char, int>();

            foreach (string personAnswers in groupAnswers)
            {
                foreach (char personAnswer in personAnswers)
                {
                    if (yesAnswerOccurrences.ContainsKey(personAnswer))
                    {
                        yesAnswerOccurrences[personAnswer]++;
                    }
                    else
                    {
                        yesAnswerOccurrences.Add(personAnswer, 1);
                    }
                }
            }

            return yesAnswerOccurrences;
        }
    }
}
