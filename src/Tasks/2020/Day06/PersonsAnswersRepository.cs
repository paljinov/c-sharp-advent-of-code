using System;

namespace App.Tasks.Year2020.Day6
{
    public class PersonsAnswersRepository
    {
        public string[][] GetPersonsAnswers(string input)
        {
            string[] personsAnswersString = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries
            );

            int groups = personsAnswersString.Length;
            string[][] personsAnswers = new string[groups][];

            for (int i = 0; i < personsAnswersString.Length; i++)
            {
                string[] personsAnswersForGroup = personsAnswersString[i].Split(Environment.NewLine);
                personsAnswers[i] = personsAnswersForGroup;
            }

            return personsAnswers;
        }
    }
}
